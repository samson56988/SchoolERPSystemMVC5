//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolERPSYSTEM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCity
    {
        public int Cityid { get; set; }
        public string Cityname { get; set; }
        public Nullable<int> sid { get; set; }
    
        public virtual tblState tblState { get; set; }
    }
}