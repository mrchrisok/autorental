using AutoRental.Client.Bootstrapper;
using AutoRental.Web.Mvc.Core;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AutoRental.Web.Mvc
{
   // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
   // visit http://go.microsoft.com/?LinkId=9394801

   public class MvcApplication : System.Web.HttpApplication
   {
      protected void Application_Start()
      {
         AreaRegistration.RegisterAllAreas();
         WebApiConfig.Register(GlobalConfiguration.Configuration);
         //GlobalConfiguration.Configure(WebApiConfig.Register);
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);
         BootstrapBundleConfig.RegisterBundles();
         AuthConfig.RegisterAuth();

         AggregateCatalog catalog = new AggregateCatalog(); // create new Mef catalog
         catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly())); // discover types
         CompositionContainer container = MEFLoader.Init(catalog.Catalogs); // add types to Mef container

         //oco: replace default mvc resolver with custom mef resolver .. instantiates controllers, etc.
         DependencyResolver.SetResolver(new MefDependencyResolver(container));

         //oco: replace default web api resolver with custom mef resolver .. instantiates api controllers
         GlobalConfiguration.Configuration.DependencyResolver = new MefAPIDependencyResolver(container);

         // consider calling SecurityAdapter.Initialize() here
            
      }
   }
}