using AutoRental.Business.Entities;
using AutoRental.Data.Configurations;
using Core.Common.Contracts;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Common.Exceptions;
using System.Reflection;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration;
using NP.Core.Common.Data;

namespace AutoRental.Data
{
   public class AutoRentalContext : DbContextBase
   {
      //---------------------------------------------------------------------------------------
      #region Accessors

      public DbSet<Account> AccountSet { get; set; }
      public DbSet<Car> CarSet { get; set; }
      public DbSet<Rental> RentalSet { get; set; }
      public DbSet<Reservation> ReservationSet { get; set; }
      public DbSet<Sale> SaleSet { get; set; }
      #endregion

      //---------------------------------------------------------------------------------------
      #region Constructors

      public AutoRentalContext() : base("name=DefaultConnection", typeof(AccountConfiguration))
      {
         Configuration.LazyLoadingEnabled = false;
      }
      #endregion

      //---------------------------------------------------------------------------------------
      #region Methods.Override

      public override int SaveChanges()
      {
          /// first, remove orphaned entities
          var orphanedObjects = ChangeTracker.Entries().Where(
             e => (e.State == EntityState.Modified || e.State == EntityState.Added) &&
                e.Entity.GetType().GetInterfaces().Any(x => x.GetType() == typeof(IAccountOwnedEntity) &&
                e.Reference("AccountId").CurrentValue == null));

          foreach (var orphanedObject in orphanedObjects)
          {
              orphanedObject.State = EntityState.Deleted;
          }

          /// second, call base to do the rest
          return base.SaveChanges();
      }
      #endregion
   }
}