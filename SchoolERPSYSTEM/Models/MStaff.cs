using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolERPSYSTEM.Models
{
    public class MStaff
    {
        [Key]
        //public int ID { get; set; }
        public int StaffID { get; set; }
        public int Designation { get; set; }
        public string DesignationName { get; set; }
        public int StaffType { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public decimal Phone1 { get; set; }
        public Nullable<decimal> Phone2 { get; set; }
        public string Email { get; set; }
        public System.DateTime DateOfAppointment { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string HighestQualification { get; set; }
        public int YearOfExperience { get; set; }
        public string PreviouseOrganization { get; set; }
        public string Image { get; set; }
        public string Resume { get; set; }
        public string Gender { get; set; }

        public string Name { get; set;}

        public string AccountStatus { get; set; }

        
        public HttpPostedFileBase ImageFile { get; set; }
    }
}