using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ProductsNew.Models;
using ProductsNew.Controllers;
using ProductsNew.Utilities;
using ProductsNew.App_Start;
using System.Web.Http.Routing;
using Moq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ProductsNew;
using System.Linq;

//using ProductsNew.App_Start;


namespace TestProducts
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void GetProducts()
        {   
            var context = new Mock<ProductsContext>();
            IService servInt;
            Service serv = new Service();
            var products = GetTestProducts();
            //var queryable = products.AsQueryable();
            //context.Object.Products.Attach(products[0]);
            //context.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(queryable.Provider);
            //context.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(queryable.Expression);
            //context.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            //context.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

           // context.Setup(x => x.Products.Add(It.IsAny<Products>())).Callback<Products>((s) => products.Add(s));

            //context.SetupAllProperties();

            var Prodservice = new ProductsService(serv);//,context.Object);

           // Products product = new Products() { Product_ID = 5, Name = "Test1", Price = 10, Width = 10, Height = 10, Length = 10, Date = DateTime.Now, ShippingPrice = 20 };
           // Prodservice.Post(product);

            var listofProd = Prodservice.GetAll();
            Assert.IsNotNull(listofProd);
            Assert.AreEqual(13, listofProd.Count);
        }

        [TestMethod]
        public void PostProduct_AddOne()
        {
            var context = new Mock<ProductsContext>();
            IService servInt;
            Service serv = new Service();
            var products = GetTestProducts();
            //context.Object.Products.Attach(products[0]);
            context.Setup(x => x.Products);

            context.SetupAllProperties();

            var Prodservice = new ProductsService(serv);//,context.Object);

             Products product = new Products() { Product_ID = 5, Name = "Test1", Price = 10, Width = 10, Height = 10, Length = 10, Date = DateTime.Now, ShippingPrice = 20 };
             Prodservice.Post(product);

            var listofProd = Prodservice.Get(5);
            Assert.IsNotNull(listofProd);
           Assert.AreEqual(product.Name,listofProd.Name);
            //Assert.AreEqual(13, listofProd.Count);
        }
        [TestMethod]
        public void PostProduct_ShouldntAdd()
        {
            var context = new Mock<ProductsContext>();
            IService servInt;
            Service serv = new Service();
            var products = GetTestProducts();
            //context.Object.Products.Attach(products[0]);
            context.Setup(x => x.Products);

            context.SetupAllProperties();

            var Prodservice = new ProductsService(serv);//,context.Object);

            Products product = new Products();// { Product_ID = 5, Name = "Test1", Price = 10, Width = 10, Height = 10, Length = 10, Date = DateTime.Now, ShippingPrice = 20 };
            Prodservice.Post(product);

            var listofProd = Prodservice.Get(5);
            Assert.IsNotNull(listofProd);
            Assert.AreEqual(product.Name, listofProd.Name);
            //Assert.AreEqual(13, listofProd.Count);
        }







        private List<Products> GetTestProducts()
        {
            var testProd = new List<Products>();
            testProd.Add(new Products { Product_ID = 10, Name = "Test1", Price = 10, Width = 10, Height = 10, Length = 10, Date = DateTime.Now, ShippingPrice = 20 });
            testProd.Add(new Products { Product_ID = 11, Name = "Test2", Price = 20, Width = 30, Height = 20, Length = 30, Date = DateTime.Now, ShippingPrice = 40 });
            testProd.Add(new Products { Product_ID = 12, Name = "Test3", Price = 3.99M, Width = 50, Height = 5, Length = 10, Date = DateTime.Now, ShippingPrice = 10, Category = "Box" });
            return testProd;
        }


        [TestMethod]
        public void DelProduct_ShouldReturnTrue()
        {

            var context = new Mock<ProductsContext>();
            IService servInt;
            Service serv = new Service();
            var products = GetTestProducts();
            //context.Object.Products.Attach(products[0]);
            context.Setup(x => x.Products);

            context.SetupAllProperties();
            var Prodservice = new ProductsService(serv);//,context.Object);
            var response = Prodservice.Delete(5);

            Assert.IsTrue(response);

        }
        [TestMethod]
        public void DelProduct_ShouldReturnFalse()
        {

            var context = new Mock<ProductsContext>();
            IService servInt;
            Service serv = new Service();
            var products = GetTestProducts();
            //context.Object.Products.Attach(products[0]);
            context.Setup(x => x.Products);

            context.SetupAllProperties();
            var Prodservice = new ProductsService(serv);//,context.Object);
            var response = Prodservice.Delete(999);

            Assert.IsFalse(response);

        }
        [TestMethod]
        public void EditProduct_EqualNamesAftChange()
        {

            var context = new Mock<ProductsContext>();
            IService servInt;
            Service serv = new Service();
            var products = GetTestProducts();
            //context.Object.Products.Attach(products[0]);
            context.Setup(x => x.Products);

            context.SetupAllProperties();
            var Prodservice = new ProductsService(serv);//,context.Object);

            Products product = new Products() { Product_ID = 5, Name = "Test3", Price = 10, Width = 10, Height = 10, Length = 10, Date = DateTime.Now, ShippingPrice = 20 };
            Prodservice.Put(product.Product_ID,product);

            var listofProd = Prodservice.Get(5);
            Assert.IsNotNull(listofProd);
            Assert.AreEqual(product.Name, listofProd.Name);
        }

        [TestMethod]
        public void EditProduct_CouldntFindObjToEdit()
        {

            var context = new Mock<ProductsContext>();
            IService servInt;
            Service serv = new Service();
            var products = GetTestProducts();
            //context.Object.Products.Attach(products[0]);
            context.Setup(x => x.Products);

            context.SetupAllProperties();
            var Prodservice = new ProductsService(serv);//,context.Object);

            Products product = new Products() {Name = "Test3", Price = 10, Width = 10, Height = 10, Length = 10, Date = DateTime.Now, ShippingPrice = 20 };
            Prodservice.Put(product.Product_ID, product);

            var listofProd = Prodservice.Get(5);
            Assert.IsNull(listofProd);
            //Assert.AreEqual(product.Name, listofProd.Name);
        }

        [TestMethod]
        public void EditProduct_DimensionsChanged()
        {

            var context = new Mock<ProductsContext>();
            IService servInt;
            Service serv = new Service();
            var products = GetTestProducts();
            //context.Object.Products.Attach(products[0]);
            context.Setup(x => x.Products);

            context.SetupAllProperties();
            var Prodservice = new ProductsService(serv);//,context.Object);

            Products product = new Products() { Product_ID = 5, Width = 10, Height = 10, Length = 10};
            Prodservice.Put(product.Product_ID, product);

            var listofProd = Prodservice.Get(5);
            Assert.IsNotNull(listofProd);
            Assert.AreEqual(product.Width, listofProd.Width);
        }
        //[TestMethod]
        //public void GetAll()
        //{
        //    Service service = new Service();
        //    ProductsService _service = new ProductsService(service);
        //    var controller = new ProductsController(service, _service);

        //   // System.Net.WebProxy proxy = new System.Net.WebProxy();
        //    //proxy = null;
        //    controller.Request = new HttpRequestMessage
        //    {
        //        RequestUri = new Uri("http://localhost:50586/api/Products/GetProducts")

        //    };
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Configuration.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{action}/{id}",
        //        defaults: new { id = RouteParameter.Optional });

        //    controller.RequestContext.RouteData = new HttpRouteData(
        //        route: new HttpRoute(),
        //        values: new HttpRouteValueDictionary { { "controller", "products" } });


        //    var response = controller.Get();
        //   // var count = response.
        //    Assert.AreEqual(response.StatusCode, 200);

        //}
    }



    //private List<Products> GetTestProducts()
    //{
    //    var testProd = new List<Products>();
    //    testProd.Add(new Products { Product_ID = 3, Name = "Test1", Price = 10, Width = 10, Height = 10, Length = 10, Date = DateTime.Now, ShippingPrice = 20 }) ;
    //    testProd.Add(new Products { Product_ID = 4, Name = "Test2", Price = 20, Width = 30, Height = 20, Length = 30, Date = DateTime.Now, ShippingPrice = 40 });
    //    testProd.Add(new Products { Product_ID = 6, Name = "Test3", Price = 3.99M, Width = 50, Height = 5, Length = 10, Date = DateTime.Now, ShippingPrice = 10, Category="Box" }); 


    //    return testProd;
    //}

}

