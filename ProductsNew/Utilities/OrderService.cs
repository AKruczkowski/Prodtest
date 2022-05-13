using ProductsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
using System.Data.Entity;

namespace ProductsNew.Utilities
{
    public class OrderService : IOrderService
    {
        ProductsContext productsContext = new ProductsContext();
        private IService _iservice;

        public OrderService(IService iservice)
        {
            _iservice = iservice;
        }

        public List<Orders> GetAll()
        {
            var orders = productsContext.Orders.ToList();
            return orders;
        }

        public Orders GetOrder(int id)
        {
            var result = productsContext.Orders.FirstOrDefault(e => e.Order_ID == id);

            return result;
        }

        public List<OrdernDetails> GetAllDetails()
        {
            var orders = productsContext.OrdernDetails.ToList();

            return orders;
        }

        public List <OrdernDetails> GetOrderDetails(int id)
        {
             var result = productsContext.OrdernDetails.Where(e => e.Order_ID == id).ToList();//(e => e.Order_ID == id).ToString();

            return result;
        }

        public void AddOrder(Orders order)
        {
            order.OrderDate = DateTime.UtcNow;
            productsContext.Orders.Add(order);
            productsContext.SaveChanges();
            var det = order.OrdernDetails.FirstOrDefault();
            var find = order.OrdernDetails.FirstOrDefault(e => e.Order_ID == det.Order_ID);
            var findPrice = productsContext.Products.FirstOrDefault(e => e.Product_ID == det.Product_ID);
            find.Price = (findPrice.Price * det.Quantity) ?? 0;
            order.TotalPrice = (find.Price + findPrice.ShippingPrice) ?? 0;
            productsContext.SaveChanges();

        }

        public Orders EditOrder(int id, Orders order)
        {
            var result = productsContext.Orders.Where(p => p.Order_ID == id).Include(x => x.OrdernDetails).FirstOrDefault();

            var orders = productsContext.OrdernDetails.Where(e => e.Order_ID == id).ToList();
            var neworders = order.OrdernDetails.ToList();
            if (result != null)
            {
                if (order.Address != null)
                { result.Address = order.Address; }
                else { }
                result.OrderDate = DateTime.UtcNow;

                for (int i = 0; i < orders.Count; i++)
                {
                    var toBeChanged = result.OrdernDetails.ElementAtOrDefault(i);
                    var changing = order.OrdernDetails.ElementAtOrDefault(i);


                    if (toBeChanged.Product_ID == changing.Product_ID)
                        if (toBeChanged.Quantity != changing.Quantity)
                        {
                            var findPrice = productsContext.Products.FirstOrDefault(e => e.Product_ID == toBeChanged.Product_ID);
                            toBeChanged.Quantity = changing.Quantity;
                            productsContext.SaveChanges();
                            toBeChanged.Price = (findPrice.Price * toBeChanged.Quantity) ?? 0;
                            result.TotalPrice = (toBeChanged.Price + findPrice.ShippingPrice) ?? 0;
                            productsContext.SaveChanges();

                        }

                    productsContext.SaveChanges();
                }
            }
            return result;
        }

        public Orders EditOrderAdress(int id, Orders order)
        {
            var result = productsContext.Orders.FirstOrDefault(e => e.Order_ID == id);

            if (result != null)
            {
                if (order.Address != null)
                {
                    result.Address = order.Address;
                }
                else { }
                result.OrderDate = DateTime.UtcNow;
                productsContext.SaveChanges();
            }
            return result;
        }

        public bool DeleteOrders(int id)
        {
            var result = productsContext.Orders.Where(p => p.Order_ID == id).Include(x => x.OrdernDetails).FirstOrDefault();
            var details = productsContext.OrdernDetails.Where(p => p.Order_ID == id);

            if (result != null)
            {
                foreach (var det in details)
                {
                    productsContext.OrdernDetails.Remove(det);
                    //productsContext.SaveChanges();
                }
                productsContext.Orders.Remove(result);
                productsContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddOrderDetail(int id, OrdernDetails orderdet)
        {
            var result = productsContext.Orders.FirstOrDefault(e => e.Order_ID == id);
            var prod = productsContext.Products.FirstOrDefault(e => e.Product_ID == orderdet.Product_ID);
            orderdet.Order_ID = result.Order_ID;
            orderdet.Price = (orderdet.Quantity * prod.Price) ?? 0;
            result.OrdernDetails.Add(orderdet);
        }

        public OrdernDetails EditOrderDetail(int id, OrdernDetails order)
        {
            var result = productsContext.OrdernDetails.FirstOrDefault(e => e.OrderDetail_ID == id);

            if (result != null)
            {
                var findPrice = productsContext.Products.FirstOrDefault(e => e.Product_ID == result.Product_ID);

                result.Quantity = order.Quantity; ;
                result.Price = (findPrice.Price * order.Quantity) ?? 0;
                productsContext.SaveChanges();

            }
            return result;
        }

        public bool DeleteOrderDetail(int id)
        {

            var result = productsContext.OrdernDetails.FirstOrDefault(e => e.OrderDetail_ID == id);

            if (result != null)
            {
                productsContext.OrdernDetails.Remove(result);
                productsContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}