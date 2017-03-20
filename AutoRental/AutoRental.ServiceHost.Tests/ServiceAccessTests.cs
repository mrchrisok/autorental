using AutoRental.Business.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;

namespace AutoRental.ServiceHost.Tests
{
   [TestClass]
   public class ServiceAccessTests
   {
      #region Methods

      [TestMethod]
      public void test_inventory_manager_as_service()
      {
         ChannelFactory<IInventoryService> channelFactory = new ChannelFactory<IInventoryService>("");

         IInventoryService proxy = channelFactory.CreateChannel(); // CreateChannel() creates a pseudo client proxy

         (proxy as ICommunicationObject).Open();

         channelFactory.Close();
      }

      [TestMethod]
      public void test_rental_manager_as_service()
      {
         ChannelFactory<IRentalService> channelFactory = new ChannelFactory<IRentalService>("");

         IRentalService proxy = channelFactory.CreateChannel();

         (proxy as ICommunicationObject).Open();

         channelFactory.Close();
      }

      [TestMethod]
      public void test_account_manager_as_service()
      {
         ChannelFactory<IAccountService> channelFactory = new ChannelFactory<IAccountService>("");

         IAccountService proxy = channelFactory.CreateChannel();

         (proxy as ICommunicationObject).Open();

         channelFactory.Close();
      }
      #endregion
   }
}

