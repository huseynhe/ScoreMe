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
    
    public partial class tbl_AppConsumeDetail
    {
        public long ID { get; set; }
        public int Status { get; set; }
        public long AppConsumeModelID { get; set; }
        public long UserID { get; set; }
        public long AppType_EVID { get; set; }
        public string AppName { get; set; }
        public string AppDescription { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public decimal Consumed { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<long> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdateUser { get; set; }
    }
}
