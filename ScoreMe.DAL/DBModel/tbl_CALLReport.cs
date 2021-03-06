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
    
    public partial class tbl_CALLReport
    {
        public long ID { get; set; }
        public int Status { get; set; }
        public long UserID { get; set; }
        public long CALLModelID { get; set; }
        public long CALLDetailID { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> OutCallCountSame { get; set; }
        public Nullable<decimal> OutCallSecondSame { get; set; }
        public Nullable<decimal> OutCallMinuteSame { get; set; }
        public Nullable<int> OutCallCountOther { get; set; }
        public Nullable<decimal> OutCallSecondOther { get; set; }
        public Nullable<decimal> OutCallMinuteOther { get; set; }
        public Nullable<int> InCallCount { get; set; }
        public Nullable<decimal> InCallSecond { get; set; }
        public Nullable<decimal> InCallMinute { get; set; }
        public Nullable<int> OutMissedCallCount { get; set; }
        public Nullable<int> InMissedCallCount { get; set; }
        public Nullable<int> OutCallForeignCount { get; set; }
        public Nullable<decimal> OutCallForeignSecond { get; set; }
        public Nullable<decimal> OutCallForeignMinute { get; set; }
        public Nullable<int> InCallForeignCount { get; set; }
        public Nullable<decimal> InCallForeignSecond { get; set; }
        public Nullable<decimal> InCallForeignMinute { get; set; }
        public Nullable<int> OutCallRoamingCount { get; set; }
        public Nullable<decimal> OutCallRoamingSecond { get; set; }
        public Nullable<decimal> OutCallRoamingMinute { get; set; }
        public Nullable<int> InCallRoamingCount { get; set; }
        public Nullable<decimal> InCallRoamingSecond { get; set; }
        public Nullable<decimal> InCallRoamingMinute { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<long> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdateUser { get; set; }
    }
}
