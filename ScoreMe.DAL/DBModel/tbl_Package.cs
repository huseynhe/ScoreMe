//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScoreMe.DAL.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Package
    {
        public long ID { get; set; }
        public int Status { get; set; }
        public long Mobile_EVID { get; set; }
        public string PackageName { get; set; }
        public decimal PackageSize { get; set; }
        public int Validity { get; set; }
        public string ValidityDesc { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<long> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdateUser { get; set; }
    }
}
