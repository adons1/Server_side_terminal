using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_side.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public int OrderStatus { get; set; }
    }
}