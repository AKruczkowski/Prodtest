using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductsNew;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;

namespace ProductsNew.App_Start
{
    public static class ServiceContainerConfig
    {

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();    
            builder.RegisterType<Service>().As<IService>();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(ProductsNew)))
            //    .Where(t => t.Namespace.Contains("Utilities"))
            //    .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I"+t.Name));

            return container;// builder.Build();
        }
    }
}