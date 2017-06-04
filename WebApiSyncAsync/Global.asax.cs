using System.Web;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using WebApiSyncAsync.DataContext;
using WebApiSyncAsync.Repositories;

namespace WebApiSyncAsync
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
	        var container = new Container();
	        container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
	        // Register 
	        container.Register(typeof(IRepository<,>), typeof(ProductsRepository));
			container.Register<IWebApiSyncAsyncContextFactory, WebApiSyncAsyncContextFactory>();
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

	        container.Verify();

	        GlobalConfiguration.Configuration.DependencyResolver =
		        new SimpleInjectorWebApiDependencyResolver(container);

			GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
