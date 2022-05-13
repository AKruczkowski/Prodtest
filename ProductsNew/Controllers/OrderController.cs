using ProductsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;
using ProductsNew.Utilities;


namespace ProductsNew.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderController : ApiController
    {
        ProductsContext productsContext = new ProductsContext();
        private IService _iservice;
        private IOrderService _orderService;
        public OrderController(IService iservice, IOrderService orderService)
        {
            _iservice = iservice;
            _orderService = orderService;
        }


        public HttpResponseMessage GetOrders()
        {
            // var orders = productsContext.Orders.ToList();
            var orders = _orderService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        // [HttpGet]
        public HttpResponseMessage GetOrders(int id)
        {
            //var result = productsContext.Orders.FirstOrDefault(e => e.Order_ID == id);
            var result = _orderService.GetOrder(id);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product with ID " + id.ToString() + "not found.");
            }
        }

        public HttpResponseMessage GetOrderDetails()
        {
           // var orders = productsContext.OrdernDetails.ToList();
            var orders = _orderService.GetAllDetails();
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        //[HttpGet]
        public HttpResponseMessage GetOrderDetails(int id)
        {
          //  var result = productsContext.OrdernDetails.Where(e => e.Order_ID == id).ToList();//(e => e.Order_ID == id).ToString();
           var result = _orderService.GetOrderDetails(id);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product with ID " + id.ToString() + "not found.");
            }
        }


        [HttpPost]
        public HttpResponseMessage AddOrder([FromBody] Orders order)
        {

            try
            {
                //order.OrderDate = DateTime.UtcNow;
                //productsContext.Orders.Add(order);

                //productsContext.SaveChanges();

                //var det = order.OrdernDetails.FirstOrDefault();
                //var find = order.OrdernDetails.FirstOrDefault(e => e.Order_ID == det.Order_ID);
                //var findPrice = productsContext.Products.FirstOrDefault(e => e.Product_ID == det.Product_ID);
                //find.Price = (findPrice.Price * det.Quantity) ?? 0;            
                //productsContext.SaveChanges();
                //// order.OrdernDetails.FirstOrDefault() = det;
                _orderService.AddOrder(order);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { order.Order_ID }));

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage EditOrder(int id, [FromBody] Orders order)
        {
            try
            {
                var result = _orderService.EditOrder(id, order);
                if(result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with Id " + id.ToString() + " not found.");

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage EditOrderNew(int id, [FromBody] Orders order)
        {
            try
            {
                var result = _orderService.EditOrderAdress(id, order);

                if (result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id " + id.ToString() + " not found.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }




        [HttpPost]
        public HttpResponseMessage DeleteOrders(int id)
        {
            try
            {
                if (!_orderService.DeleteOrders(id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with ID " + id.ToString() + "not found.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddOrderDetail(int id, [FromBody] OrdernDetails orderdet)
        {
            try
            {
                //var result = productsContext.Orders.FirstOrDefault(e => e.Order_ID == id);
                //var prod = productsContext.Products.FirstOrDefault(e => e.Product_ID == orderdet.Product_ID);
                //orderdet.Order_ID = result.Order_ID;
                //orderdet.Price = (orderdet.Quantity * prod.Price)??0;
                //result.OrdernDetails.Add(orderdet);
                //productsContext.SaveChanges();
                _orderService.AddOrderDetail(id, orderdet);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, orderdet);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { orderdet.Order_ID }));

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage EditOrderDetail(int id, [FromBody] OrdernDetails order)
        {
            try
            {
                //var result = productsContext.OrdernDetails.FirstOrDefault(e => e.OrderDetail_ID == id);
                var result = _orderService.EditOrderDetail(id, order);
                if (result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id " + id.ToString() + " not found.");
                }
                else
                {
                    //var findPrice = productsContext.Products.FirstOrDefault(e => e.Product_ID == result.Product_ID);
                   
                    //result.Quantity = order.Quantity;;
                    //result.Price = (findPrice.Price * order.Quantity)??0;
                    //productsContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        // ///////////////////////////////

        [HttpPost]
        public HttpResponseMessage DeleteOrderDetail(int id)
        {
            try
            {
               // var result = productsContext.OrdernDetails.FirstOrDefault(e => e.OrderDetail_ID == id);

                if (!_orderService.DeleteOrderDetail(id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Partial order with ID " + id.ToString() + "not found.");
                }
                else
                {
                    //productsContext.OrdernDetails.Remove(result);
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
