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
    
    public partial class log_DataChange
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<int> OperationType { get; set; }
        public Nullable<System.DateTime> OperationDateTime { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public Nullable<int> OriginalId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public Nullable<int> IsActive { get; set; }
    }
}