using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolERPSYSTEM.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string Role1 { get; set; }
        public Nullable<int> StaffID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Rolename { get; set; }
    }
}