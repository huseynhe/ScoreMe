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
    
    public partial class tbl_CALLDetail
    {
        public long ID { get; set; }
        public int Status { get; set; }
        public long CALLModelID { get; set; }
        public Nullable<int> InOutType { get; set; }
        public Nullable<int> IsRoaming { get; set; }
        public Nullable<int> IsForeign { get; set; }
        public string PhonePrefix { get; set; }
        public Nullable<decimal> Duration { get; set; }
        public Nullable<System.DateTime> RecievedDate { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<long> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdateUser { get; set; }
    }
}
