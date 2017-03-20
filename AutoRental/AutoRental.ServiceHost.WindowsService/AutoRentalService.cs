using AutoRental.Business.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using SM = System.ServiceModel;
using System.IO;
using System.Transactions;
using T = System.Timers;
using AutoRental.Business.Entities;
using AutoRental.Business.Bootstrapper;
using NP.Core.Common.Core;
using System.Security.Principal;
using AutoRental.Common;
using System.Threading;

namespace AutoRental.ServiceHost.WindowsService
{
   public partial class AutoRentalService : ServiceBase
   {
      T.Timer _timer = new T.Timer(60000); // via config?

      public AutoRentalService()
      {
         InitializeComponent();
      }

      // create module level servicehost(s)
      SM.ServiceHost hostInventoryManager = new SM.ServiceHost(typeof(InventoryManager));
      SM.ServiceHost hostRentalManager = new SM.ServiceHost(typeof(RentalManager));
      SM.ServiceHost hostAccountManager = new SM.ServiceHost(typeof(AccountManager));

      protected override void OnStart(string[] args)
      {
         //GenericPrincipal principal = new GenericPrincipal(
         //   new GenericIdentity("AutoRental.ServiceHost.Console"), new string[] { Security.User, Security.Admin });
         //Thread.CurrentPrincipal = principal;

         WriteLog("Starting up database...");
         AutoRentalContextBootstrapper.Init(); // initializes the AutoRentalContext
         ObjectBase.Container = MEFLoader.Init();  // DI: repositories and AutoRentalEngine
         WriteLog("Database started...\n\n");

         // open the host(s)
         WriteLog("Starting up services...\n");

         StartService(hostInventoryManager, "InventoryManager");
         StartService(hostRentalManager, "RentalManager");
         StartService(hostAccountManager, "AccountManager");

         _timer.Elapsed += OnTimerElapsed;
         _timer.Start();
      }

      protected override void OnStop()
      {
         // close the service host(s)
         _timer.Stop();
         WriteLog(string.Format("Unattended operations stopped at {0}. ...\n", DateTime.Now.ToString()));

         StopService(hostInventoryManager, "InventoryManager");
         StopService(hostRentalManager, "RentalManager");
         StopService(hostAccountManager, "AccountManager");
      }

      /// <summary>
      /// Starts the AutoRental tcp services
      /// </summary>
      /// <param name="host"></param>
      /// <param name="serviceDescription"></param>
      static void StartService(SM.ServiceHost host, string serviceDescription)
      {
         host.Open();

         string logEntry = string.Empty;
         logEntry += string.Format("Service {0} started.\n", serviceDescription);

         foreach (var endpoint in host.Description.Endpoints)
         {
            logEntry += string.Format(" -- Address: {0}\n", endpoint.Address.Uri.ToString());
            logEntry += string.Format(" -- Binding: {0}\n", endpoint.Binding.Name);
            logEntry += string.Format(" -- Contract: {0}\n", endpoint.Contract.ConfigurationName);
         }

         WriteLog(logEntry + "\n");
      }

      /// <summary>
      /// Stops the AutoRental tcp services
      /// </summary>
      /// <param name="host"></param>
      /// <param name="serviceDescription"></param>
      static void StopService(SM.ServiceHost host, string serviceDescription)
      {
         host.Close();
         WriteLog(string.Format("Service {0} stopped.\n", serviceDescription));
      }

      /// <summary>
      /// Executes unattended service operations
      /// </summary>
      /// <param name="sender">Sender is timer.Elasped</param>
      /// <param name="e">Null</param>
      static void OnTimerElapsed(object sender, T.ElapsedEventArgs e)
      {
         string logEntry = string.Empty;

         logEntry += "Timer interval elapsed\n";
         logEntry += string.Format("Initiating unattended operations at {0}. ...\n", DateTime.Now.ToString());

         RentalManager rentalManager = new RentalManager();
         Reservation[] reservations = rentalManager.GetDeadReservations();
         logEntry += string.Format("Looking for expired reservations at {0}. ...\n", DateTime.Now.ToString());

         if (reservations != null)
         {
            logEntry += string.Format("Found {0} expired reservations.\n", reservations.Length);

            foreach (Reservation reservation in reservations)
            {
               using (TransactionScope scope = new TransactionScope())
               {
                  try
                  {
                     rentalManager.CancelReservation(reservation.ReservationId);
                     logEntry += string.Format("Canceling reservation '{0}'.\n", reservation.ReservationId);

                     scope.Complete();
                  }
                  catch (Exception ex)
                  {
                     logEntry += string.Format("There was a {0} exception when attempting to cancel reservation '{1}'.\n", ex.Source, reservation.ReservationId);
                     WriteLog(logEntry);
                  }
               }
            }
         }
         else
         {
            logEntry += "No expired reservations found.\n";
         }

         WriteLog(logEntry);
      }

      /// <summary>
      /// Writes entries to the service log
      /// </summary>
      /// <param name="logEntry"></param>
      static void WriteLog(string logEntry)
      {
         using (StreamWriter writer = File.AppendText("c:\\Temp\\AutoRentalService.log"))
         {
            writer.WriteLineAsync(logEntry);
         }
      }
   }
}
