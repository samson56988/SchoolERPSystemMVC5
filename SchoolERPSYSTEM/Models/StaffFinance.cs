using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolERPSYSTEM.Models
{
    public class StaffFinance
    {
        public int StaffID { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal HRA { get; set; }

        public decimal HouseA { get; set; }

        public decimal TransA { get; set; }

        public decimal Tax { get; set; }

        public decimal Vat { get; set; }

        public decimal latenessFee { get; set; }

        public decimal NHF { get; set; }

        public decimal Total { get; set; }
       
    }
}