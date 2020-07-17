using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_side.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Addres { get; set; }
        public string Price { get; set; }
        public int Weigth { get; set; }
    }
}