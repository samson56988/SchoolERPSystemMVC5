﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolERPSYSTEM.Models
{
    public class Attendance
    {
        public string AdmissionNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Class { get; set; }

        public int SessionName { get; set; }

        public int id { get; set; }

        public decimal amount { get; set; }

        public string pdf { get; set; }

        public string teller { get; set; }

        public string Session{ get; set; }

        public System.DateTime Attendancedate { get; set; }

        public string AttendanceValue { get; set; }
    }
}