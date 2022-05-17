using ProductsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Cors;

namespace ProductsNew.Utilities
{
    public class ProductsService : IProductsService
    {

        private ProductsContext productsContext;// = new ProductsContext();
        private IService _iservice;

        public ProductsService(IService service, ProductsContext context)
        {
            _iservice = service;
           productsContext = context;
        }

        public List<Products> GetAll()
        {
            var products = productsContext.Products.ToList();

            return products;
        }
        public Products Get(int id)
        {
            var result = productsContext.Products.FirstOrDefault(e => e.Product_ID == id);

            return result;
        }

        public void Post(Products product)
        {
            productsContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Products ON");
            product.ShippingPrice = _iservice.EstimatePrice(_iservice.EstimateVolume(product.Height ?? 0, product.Length ?? 0, product.Width ?? 0));
            productsContext.Products.Add(product);
            productsContext.SaveChanges();
            productsContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Products OFF");
        }
        public Products Put(int id, Products value)
        {
            var result = productsContext.Products.FirstOrDefault(e => e.Product_ID == id);
            if (result != null)
            {
                result.Name = value.Name;
                result.Description = value.Description;
                result.Price = value.Price;
                result.Height = value.Height;
                result.Width = value.Width;
                result.Date = value.Date;
                result.Length = value.Length;
                result.Date = DateTime.Now;
                result.Category = value.Category;
                result.ShippingPrice = _iservice.EstimatePrice(_iservice.EstimateVolume(result.Height ?? 0, result.Length ?? 0, result.Width ?? 0));
                productsContext.SaveChanges();
            }
            return result;
        }

        public bool Delete(int id)
        {
            var result = productsContext.Products.FirstOrDefault(e => e.Product_ID == id);
            if (result == null)
            {
                return false;
            }
            else
            {
                productsContext.Products.Remove(result);
                productsContext.SaveChanges();
                return true;
            }
        }
    }
}