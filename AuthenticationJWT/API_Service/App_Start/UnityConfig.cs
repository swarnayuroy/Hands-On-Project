using API_Service.RepositoryLayer.RepoInterface;
using API_Service.RepositoryLayer.Repository;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace API_Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IRepository, DataRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}