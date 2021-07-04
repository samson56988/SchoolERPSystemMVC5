using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolERPSYSTEM.Models
{
    public class SubjectMarks
    {
        public int Session { get; set; }

        public string Term { get; set; }

        public int Subject { get; set; }

        public int Class { get; set; }

        public string StudentName { get; set; }

        public int StudentID { get; set; }

        public int firstCa { get; set; }

        public int SecondCa { get; set; }

        public int Exam { get; set; }

        public int Total { get; set; }

        public string Grade { get; set; }


       


    }
}