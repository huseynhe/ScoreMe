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
    
    public partial class tbl_SMSReportShort
    {
        public long ID { get; set; }
        public int Status { get; set; }
        public long UserID { get; set; }
        public long SMSModelID { get; set; }
        public long SMSDetailID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string SenderName { get; set; }
        public Nullable<int> IsExpense { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public Nullable<System.DateTime> OperationDate { get; set; }
        public string MerchantName { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string BalanceCurrency { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<long> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdateUser { get; set; }
    }
}
