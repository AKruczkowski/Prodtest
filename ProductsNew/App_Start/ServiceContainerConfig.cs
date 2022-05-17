using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductsNew;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Web.Mvc;
using ProductsNew.Utilities;
using ProductsNew.Models;

namespace ProductsNew.App_Start
{
    public static class ServiceContainerConfig
    {

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // builder.RegisterModule(new AutofacModule());
            var config = GlobalConfiguration.Configuration;
            builder.RegisterType<ProductsContext>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ProductsService>().As<IProductsService>();
            builder.RegisterType<ProductsNew.Utilities.Service>().As<IService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;

        }
    }
}