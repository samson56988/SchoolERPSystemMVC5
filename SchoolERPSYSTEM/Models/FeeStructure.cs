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
    
    public partial class FeeStructure
    {
        public int FeeID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<int> LevelID { get; set; }
        public Nullable<int> FeeTypeID { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual ClassLevel ClassLevel { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual FeeType FeeType { get; set; }
    }
}
