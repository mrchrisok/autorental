using AutoRental.Business.Bootstrapper;
using AutoRental.Business.Entities;
using AutoRental.Business.Managers;
using AutoRental.Common;
using NP.Core.Common.Core;
using System;
using System.Security.Principal;
using System.Threading;
using System.Timers;
using System.Transactions;
using SM = System.ServiceModel;

namespace AutoRental.ServiceHost
{
   class Program
   {
      static void Main(string[] args)
      {
         //GenericPrincipal principal = new GenericPrincipal(
         //      new GenericIdentity("Osita"), new string[] { Security.User, Security.Admin });
         //Thread.CurrentPrincipal = principal;
         
         Console.WriteLine("Starting up database...");

         AutoRentalContextBootstrapper.Init(); // initializes the AutoRentalContext
         ObjectBase.Container = MEFLoader.Init();  // DI: repositories and AutoRentalEngine

         Console.WriteLine("Database started...");
         Console.WriteLine("");

         Console.WriteLine("Starting up services...");

         SM.ServiceHost hostInventoryManager = new SM.ServiceHost(typeof(InventoryManager));
         SM.ServiceHost hostRentalManager = new SM.ServiceHost(typeof(RentalManager));
         SM.ServiceHost hostAccountManager = new SM.ServiceHost(typeof(AccountManager));

         StartService(hostInventoryManager, "InventoryManager");
         StartService(hostRentalManager, "RentalManager");
         StartService(hostAccountManager, "AccountManager");

         // config & start a timer for timed tasks. handler is OnTimerElapsed() below
         System.Timers.Timer timer = new System.Timers.Timer(120000);
         timer.Elapsed += OnTimerElapsed;
         timer.Start();

         Console.WriteLine("Reservation monitor started.");

         Console.WriteLine("");
         Console.WriteLine("Press [Enter] to exit.\n");
         Console.ReadLine(); // allows the timer to continue running until user presses Enter

         /// after user presses Enter ...
         timer.Stop();
         
         Console.WriteLine("Reservation monitor stopped.");

         StopService(hostInventoryManager, "InventoryManager");
         StopService(hostRentalManager, "RentalManager");
         StopService(hostAccountManager, "AccountManager");
      }

      /// <summary>
      /// Executes unattended service operations
      /// </summary>
      /// <param name="sender">Sender is timer.Elasped</param>
      /// <param name="e">Null</param>
      static void OnTimerElapsed(object sender, ElapsedEventArgs e)
      {
         Console.WriteLine("Timer interval elapsed");
         Console.WriteLine("Looking for expired reservations at {0}. ...", DateTime.Now.ToString());

         RentalManager rentalManager = new RentalManager();
         Reservation[] reservations = rentalManager.GetDeadReservations();
         
         if (reservations != null)
         {
            Console.WriteLine("Found {0} expired reservations.", reservations.Length);

            foreach (Reservation reservation in reservations)
            {
               using (TransactionScope scope = new TransactionScope())
               {
                  try
                  {
                     rentalManager.CancelReservation(reservation.ReservationId);
                     Console.WriteLine("Canceling reservation '{0}'.", reservation.ReservationId);

                     scope.Complete();
                  }
                  catch (Exception ex)
                  {
                     Console.WriteLine("There was a {0} exception when attempting to cancel reservation '{1}'.", ex.Source, reservation.ReservationId);
                  }
               }
            }
         }
         else
         {
            Console.WriteLine("No expired reservations found.");
         }
      }

      /// <summary>
      /// Starts the AutoRental tcp services
      /// </summary>
      /// <param name="host"></param>
      /// <param name="serviceDescription"></param>
      static void StartService(SM.ServiceHost host, string serviceDescription)
      {
         host.Open();
         Console.WriteLine("Service {0} started.", serviceDescription);

         foreach (var endpoint in host.Description.Endpoints)
         {
            Console.WriteLine(string.Format(" -- Address: {0}", endpoint.Address.Uri.ToString()));
            Console.WriteLine(string.Format(" -- Binding: {0}", endpoint.Binding.Name));
            Console.WriteLine(string.Format(" -- Contract: {0}", endpoint.Contract.ConfigurationName));
         }

         Console.WriteLine();
      }

      /// <summary>
      /// Stops the AutoRental tcp services
      /// </summary>
      /// <param name="host"></param>
      /// <param name="serviceDescription"></param>
      static void StopService(SM.ServiceHost host, string serviceDescription)
      {
         host.Close();
         Console.WriteLine("Service {0} stopped.", serviceDescription);
      }
   }
}
