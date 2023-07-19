using ServiceLayer.ServiceDomain;
using ServiceLayer.ServiceInterface;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace AuthenticationJWT
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IService, Service>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}