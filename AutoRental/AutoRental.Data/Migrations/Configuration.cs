namespace AutoRental.Data.Migrations
{
    using AutoRental.Business.Entities;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    using AutoRental.Common;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoRental.Data.AutoRentalContext>
    {
        #region Constructors

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        #endregion
        //---------------------------------------------------------------------------------------------------

        #region Methods

        /// <summary>
        /// Populates the database with seed data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(AutoRental.Data.AutoRentalContext context)
        {
            context.CarSet.AddOrUpdate(x => x.Description,
               new Car("Ford Mustang", "Green", 2010, 100.0M, DateTime.Now),
               new Car("Chevy Camaro", "Blue", 2011, 120.0M, DateTime.Now),
               new Car("Honda Accord", "Red", 2012, 140M, DateTime.Now),
               new Car("Toyota Prius", "Silver", 2013, 160M, DateTime.Now),
               new Car("Lexus ES350", "Black", 2014, 180M, DateTime.Now),
               new Car("Cadillac Escalade", "White", 2014, 180M, DateTime.Now)
            );

            context.ReservationSet.AddOrUpdate(x => x.AccountId,
                  new Reservation { AccountId = 1, CarId = 2, RentalDate = new DateTime(2016, 1, 1), ReturnDate = new DateTime(2016, 7, 4), DateCreated = DateTime.Now }
            );

            SeedMembership();
        }

        /// <summary>
        /// Populates the database with seed membership data
        /// </summary>
        private void SeedMembership()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Account", "AccountId", "LoginEmail", autoCreateTables: true);
            }

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists(Security.Admin))
            {
                roles.CreateRole(Security.Admin);
            }

            if (!roles.RoleExists(Security.User))
            {
                roles.CreateRole(Security.User);
            }

            if (membership.GetUser("mrchrisok@hotmail.com", false) == null)
            {
                WebSecurity.CreateUserAndAccount("mrchrisok@hotmail.com", "Mocha4578",
                   new
                   {
                       FirstName = "Chris",
                       LastName = "Okonkwo",
                       Address = "123 American Way",
                       City = "Mckinney",
                       State = "TX",
                       ZipCode = "75070",
                       CreditCard = "1111222233334444",
                       ExpDate = "032015",
                       DateCreated = DateTime.Now,
                       DateModified = DateTime.Now
                   });
            }

            if (!roles.GetRolesForUser("mrchrisok@hotmail.com").Contains(Security.Admin))
            {
                roles.AddUsersToRoles(new[] { "mrchrisok@hotmail.com" }, new[] { Security.Admin });
            }

            if (!roles.GetRolesForUser("mrchrisok@hotmail.com").Contains(Security.User))
            {
                roles.AddUsersToRoles(new[] { "mrchrisok@hotmail.com" }, new[] { Security.User });
            }
        }
        #endregion
    }
}