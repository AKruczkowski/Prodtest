using ProductsNew.Models;
using System.Collections.Generic;

namespace ProductsNew.Utilities
{
    public interface IProductsService
    {
        bool Delete(int id);
        Products Get(int id);
        List<Products> GetAll();
        void Post(Products product);
        Products Put(int id, Products value);
    }
}