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
    
    public partial class tbl_ProposalUserGroup
    {
        public long ID { get; set; }
        public int Status { get; set; }
        public long ProposalID { get; set; }
        public long GroupID { get; set; }
        public long UserID { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<int> InsertUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }
    }
}
