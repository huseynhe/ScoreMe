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
    
    public partial class tbl_Provider
    {
        public long ID { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public long ParentID { get; set; }
        public long UserId { get; set; }
        public Nullable<long> Type { get; set; }
        public string Description { get; set; }
        public Nullable<long> RegionId { get; set; }
        public Nullable<decimal> Longitudes { get; set; }
        public Nullable<decimal> Latitudes { get; set; }
        public string Address { get; set; }
        public string RelatedPersonName { get; set; }
        public string RelatedPersonProfession { get; set; }
        public string RelatedPersonPhone { get; set; }
        public string RP_HomePhone { get; set; }
        public string VOEN { get; set; }
        public string LogoLinkPath { get; set; }
        public string LogoLinkName { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<long> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdateUser { get; set; }
    }
}
