using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektASP.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        [MaxLength(6)]
        public string Code { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int HouseNumber { get; set; }
    }
}