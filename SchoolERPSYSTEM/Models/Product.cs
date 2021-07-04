using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchoolERPSYSTEM.Models
{
    public class Product
    {

        public int ID { get; set; }

        [DisplayName("Product Name")]
        public string Productname { get; set; }

        public double price { get; set; }

        public int quantity { get; set; }
    }
}