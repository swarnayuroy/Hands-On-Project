using API_Service.RepositoryLayer.RepoInterface;
using API_Service.RepositoryLayer.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

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
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}