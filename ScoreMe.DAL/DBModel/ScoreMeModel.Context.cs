﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_A62358_ScoreMeEntities : DbContext
    {
        public DB_A62358_ScoreMeEntities()
            : base("name=DB_A62358_ScoreMeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ProviderType> ProviderTypes { get; set; }
        public virtual DbSet<tbl_Customer> tbl_Customer { get; set; }
        public virtual DbSet<tbl_EnumCategory> tbl_EnumCategory { get; set; }
        public virtual DbSet<tbl_EnumValue> tbl_EnumValue { get; set; }
        public virtual DbSet<tbl_Group> tbl_Group { get; set; }
        public virtual DbSet<tbl_NetConsume> tbl_NetConsume { get; set; }
        public virtual DbSet<tbl_Package> tbl_Package { get; set; }
        public virtual DbSet<tbl_PackagePrice> tbl_PackagePrice { get; set; }
        public virtual DbSet<tbl_Permission> tbl_Permission { get; set; }
        public virtual DbSet<tbl_Proposal> tbl_Proposal { get; set; }
        public virtual DbSet<tbl_ProposalDetail> tbl_ProposalDetail { get; set; }
        public virtual DbSet<tbl_ProposalUserGroup> tbl_ProposalUserGroup { get; set; }
        public virtual DbSet<tbl_Provider> tbl_Provider { get; set; }
        public virtual DbSet<tbl_ProviderRole> tbl_ProviderRole { get; set; }
        public virtual DbSet<tbl_ProviderService> tbl_ProviderService { get; set; }
        public virtual DbSet<tbl_ProviderUserProposal> tbl_ProviderUserProposal { get; set; }
        public virtual DbSet<tbl_ProviderUserRating> tbl_ProviderUserRating { get; set; }
        public virtual DbSet<tbl_RatingType> tbl_RatingType { get; set; }
        public virtual DbSet<tbl_RatingValue> tbl_RatingValue { get; set; }
        public virtual DbSet<tbl_Region> tbl_Region { get; set; }
        public virtual DbSet<tbl_Role> tbl_Role { get; set; }
        public virtual DbSet<tbl_Score_x> tbl_Score_x { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
        public virtual DbSet<tbl_UserGroup> tbl_UserGroup { get; set; }
        public virtual DbSet<tbl_UserRole> tbl_UserRole { get; set; }
        public virtual DbSet<tbl_UserService> tbl_UserService { get; set; }
        public virtual DbSet<tbl_UserServiceRating> tbl_UserServiceRating { get; set; }
    }
}
