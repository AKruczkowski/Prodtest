using ProductsNew.Models;
using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
//using Autofac;
//using ProductsNew.App_Start;
using ProductsNew.Utilities;
//using System.Globalization;

namespace ProductsNew.Controllers
{
   [EnableCors(origins: "*", headers:"*",methods:"*")]
   [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {

        private IService _iservice;
        private IProductsService productsService;

        public ProductsController(IService service, IProductsService productsService)
        {
            _iservice = service;
            this.productsService = productsService;

        }

        // GET: api/Products
        [Route("GetProducts")]
        public HttpResponseMessage Get()
        {
            // var products = productsContext.Products.ToList();
            var products = productsService.GetAll();
           return Request.CreateResponse(HttpStatusCode.OK, products);
        }


        // GET: api/Products/5
        [Route("GetProduct")]
        public HttpResponseMessage Get(int id)
        {
           // var result = productsContext.Products.FirstOrDefault(e => e.Product_ID == id);
           var result = productsService.Get(id);

            if(result != null)
            {return Request.CreateResponse(HttpStatusCode.OK, result);  }
            else
            { return Request.CreateResponse(HttpStatusCode.NotFound, "Product with ID " + id.ToString() + "not found.");}
        }

        // POST: api/Products
        [Route("AddProduct")]
        public HttpResponseMessage Post([FromBody] Products product)
        {
            try
            {
                productsService.Post(product); 
                var message = Request.CreateResponse(HttpStatusCode.Created, product);
                message.Headers.Location = new Uri(Request.RequestUri + "/"+ product.Product_ID.ToString());


                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/Products/5
        [Route("EditProduct")]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Products value)
        {
            try
            {
                var result = productsService.Put(id, value);

                if (result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id " + id.ToString() + " not found.");
                }
                else
                { return Request.CreateResponse(HttpStatusCode.OK, result); }   
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

        }

        // DELETE: api/Products/5
        [Route("RemoveProduct")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
               // var result = productsContext.Products.FirstOrDefault(e => e.Product_ID == id);

                if(!productsService.Delete(id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with ID " + id.ToString() + "not found.");
                }
                else
                {
                    //productsContext.Products.Remove(result);
                    //productsContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);  
            }
        }
    }
}
