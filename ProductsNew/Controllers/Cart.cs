using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProductsNew.Controllers
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public int Product_ID { get; set; }

    }
}