using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASP.Models
{
    public class OrdersProducts
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}