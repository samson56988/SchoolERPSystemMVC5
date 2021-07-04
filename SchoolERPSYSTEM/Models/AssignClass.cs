using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolERPSYSTEM.Models
{
    public class AssignClass
    {
        public int StaffID { get; set;}

        public string Staffname { get; set; }

        public int ClassLevelID { get; set; }

        public string ClassLevel { get; set; }

        public string Prefix { get; set; }
    }
}