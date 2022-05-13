using ProductsNew.Models;
using System.Collections.Generic;

namespace ProductsNew.Utilities
{
    public interface IOrderService
    {
        void AddOrder(Orders order);
        void AddOrderDetail(int id, OrdernDetails orderdet);
        bool DeleteOrderDetail(int id);
        bool DeleteOrders(int id);
        Orders EditOrder(int id, Orders order);
        Orders EditOrderAdress(int id, Orders order);
        OrdernDetails EditOrderDetail(int id, OrdernDetails order);
        List<Orders> GetAll();
        List<OrdernDetails> GetAllDetails();
        List<OrdernDetails> GetOrderDetails(int id);
        Orders GetOrder(int id);
    }
}