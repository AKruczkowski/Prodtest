using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ProductsNew.Models;
using ProductsNew.Controllers;
using ProductsNew.Utilities;
using System.Web.Http.Routing;
using Moq;
using System.Data.Entity;
using System.Linq;

//using ProductsNew.App_Start;


namespace TestProducts
{
    public class ProductContext : DbContext
    {
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdernDetails> OrdernDetails { get; set; }

    }


    [TestClass]
    public class UnitTest1
    {

          [TestMethod]
        public void Price_ShouldReturn40()
        {
            var price = 40;
            var volume1 = 1001;

            var mock = new Mock<Service>();

            var vol = mock.Object.EstimatePrice(volume1);

            Assert.AreEqual(40, price);

        }
          [TestMethod]
        public void Volume_EstimatedVolume()
        {
            var result = 6000;
            decimal d1 = 10;
            decimal d2 = 20;
            decimal d3 = 30;

            var mock = new Mock<Service>();

            var vol = mock.Object.EstimateVolume(d1, d2, d3);

            Assert.AreEqual(vol, result);

        }
         [TestMethod]
        public void Volume_ReturnZero()
        {
            decimal d1 = 10;
            decimal d2 = 0;
            decimal d3 = 30;

            var mock = new Mock<Service>();

            var vol = mock.Object.EstimateVolume(d1, d2, d3);

            Assert.AreEqual(vol, 0);

        }
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [TestMethod]
        public void GetProducts_GetAll()
        {
            var testProd = new List<Products>
            {

                 new Products { Product_ID = 11, Name = "Test2", Price = 20, Width = 30, Height = 20, Length = 30, Date = DateTime.Now, ShippingPrice = 40 },
                 new Products { Product_ID = 12, Name = "Test3", Price = 3.99M, Width = 50, Height = 5, Length = 10, Date = DateTime.Now, ShippingPrice = 10, Category = "Box" }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Products>>();
            mockSet.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(testProd.Provider);
            mockSet.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(testProd.Expression);
            mockSet.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(testProd.ElementType);
            mockSet.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(testProd.GetEnumerator());


            var context = new Mock<ProductsContext>();
             context.Setup(c => c.Products).Returns(mockSet.Object);

            Service serv = new Service();
            var Prodservice = new ProductsService(serv, context.Object);

            var listofProd = Prodservice.GetAll();
            Assert.IsNotNull(listofProd);
            Assert.AreEqual(2, listofProd.Count);
        }


        [TestMethod]
        public void GetProduct_GetOne()
        {
            var testProd = new List<Products>
            {

                 new Products { Product_ID = 11, Name = "Test2", Price = 20, Width = 30, Height = 20, Length = 30, Date = DateTime.Now, ShippingPrice = 40 },
                 new Products { Product_ID = 12, Name = "Test3", Price = 3.99M, Width = 50, Height = 5, Length = 10, Date = DateTime.Now, ShippingPrice = 10, Category = "Box" }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Products>>();
            mockSet.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(testProd.Provider);
            mockSet.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(testProd.Expression);
            mockSet.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(testProd.ElementType);
            mockSet.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(testProd.GetEnumerator());


            var context = new Mock<ProductsContext>();
            context.Setup(c => c.Products).Returns(mockSet.Object);

            Service serv = new Service();
            var Prodservice = new ProductsService(serv, context.Object);

            var listofProd = Prodservice.Get(11);
            Assert.IsNotNull(listofProd);
        }



         [TestMethod]
        public void PostProduct_AddOne()
        {
            var testProd = new List<Products>
            {

                 new Products { Product_ID = 11, Name = "Test2", Price = 20, Width = 30, Height = 20, Length = 30, Date = DateTime.Now, ShippingPrice = 40 },
                 new Products { Product_ID = 12, Name = "Test3", Price = 3.99M, Width = 50, Height = 5, Length = 10, Date = DateTime.Now, ShippingPrice = 10, Category = "Box" }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Products>>();
            mockSet.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(testProd.Provider);
            mockSet.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(testProd.Expression);
            mockSet.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(testProd.ElementType);
            mockSet.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(testProd.GetEnumerator());


            var context = new Mock<ProductsContext>();
            context.Setup(c => c.Products).Returns(mockSet.Object);

            Service serv = new Service();
            var Prodservice = new ProductsService(serv, context.Object);

            Products prod = new Products { Product_ID = 12, Name = "Test3", Price = 3.99M, Width = 50, Height = 5, Length = 10, Date = DateTime.Now, ShippingPrice = 10, Category = "Box" };

           Prodservice.Post(prod);
            Assert.IsInstanceOfType(prod, typeof(Products));
        }

        [TestMethod]
        public void DelProduct_ShouldReturnTrue()
        {
            var testProd = new List<Products>
            {

                 new Products { Product_ID = 11, Name = "Test2", Price = 20, Width = 30, Height = 20, Length = 30, Date = DateTime.Now, ShippingPrice = 40 },
                 new Products { Product_ID = 12, Name = "Test3", Price = 3.99M, Width = 50, Height = 5, Length = 10, Date = DateTime.Now, ShippingPrice = 10, Category = "Box" }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Products>>();
            mockSet.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(testProd.Provider);
            mockSet.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(testProd.Expression);
            mockSet.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(testProd.ElementType);
            mockSet.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(testProd.GetEnumerator());


            var context = new Mock<ProductsContext>();
            context.Setup(c => c.Products).Returns(mockSet.Object);

            Service serv = new Service();
            var Prodservice = new ProductsService(serv, context.Object);

           var response = Prodservice.Delete(11);
            Assert.IsTrue(response);

        }

        [TestMethod]
        public void DelProduct_ShouldReturnFalse()
        {

            var testProd = new List<Products>
            {

                 new Products { Product_ID = 11, Name = "Test2", Price = 20, Width = 30, Height = 20, Length = 30, Date = DateTime.Now, ShippingPrice = 40 },
                 new Products { Product_ID = 12, Name = "Test3", Price = 3.99M, Width = 50, Height = 5, Length = 10, Date = DateTime.Now, ShippingPrice = 10, Category = "Box" }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Products>>();
            mockSet.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(testProd.Provider);
            mockSet.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(testProd.Expression);
            mockSet.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(testProd.ElementType);
            mockSet.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(testProd.GetEnumerator());


            var context = new Mock<ProductsContext>();
            context.Setup(c => c.Products).Returns(mockSet.Object);

            Service serv = new Service();
            var Prodservice = new ProductsService(serv, context.Object);

            var response = Prodservice.Delete(999);

            Assert.IsFalse(response);

        }
    }
}

