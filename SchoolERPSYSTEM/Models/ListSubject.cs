using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolERPSYSTEM.Models
{
    public class ListSubject
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Subject { get; set; }

        public string Levelname { get; set; }

        public int FirstAss { get; set; }

        public int SecondAss { get; set; }

        public int Exams { get; set; }

        public int Total { get; set; }

        public string Grade { get; set; }

        public string session { get; set; }

        public string Term { get; set; }
    }
}