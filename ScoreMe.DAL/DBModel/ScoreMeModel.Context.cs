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
        public virtual DbSet<tbl_AccessRight> tbl_AccessRight { get; set; }
        public virtual DbSet<tbl_AppConsumeDetail> tbl_AppConsumeDetail { get; set; }
        public virtual DbSet<tbl_AppConsumeModel> tbl_AppConsumeModel { get; set; }
        public virtual DbSet<tbl_AppGroupInformation> tbl_AppGroupInformation { get; set; }
        public virtual DbSet<tbl_ApplicationInformation> tbl_ApplicationInformation { get; set; }
        public virtual DbSet<tbl_CALLDetail> tbl_CALLDetail { get; set; }
        public virtual DbSet<tbl_CALLModel> tbl_CALLModel { get; set; }
        public virtual DbSet<tbl_CALLReport> tbl_CALLReport { get; set; }
        public virtual DbSet<tbl_Customer> tbl_Customer { get; set; }
        public virtual DbSet<tbl_Employee> tbl_Employee { get; set; }
        public virtual DbSet<tbl_EnumCategory> tbl_EnumCategory { get; set; }
        public virtual DbSet<tbl_EnumValue> tbl_EnumValue { get; set; }
        public virtual DbSet<tbl_Group> tbl_Group { get; set; }
        public virtual DbSet<tbl_NetConsumeDetail> tbl_NetConsumeDetail { get; set; }
        public virtual DbSet<tbl_NetConsumeModel> tbl_NetConsumeModel { get; set; }
        public virtual DbSet<tbl_OperatorInformation> tbl_OperatorInformation { get; set; }
        public virtual DbSet<tbl_OTP> tbl_OTP { get; set; }
        public virtual DbSet<tbl_Package> tbl_Package { get; set; }
        public virtual DbSet<tbl_PackagePrice> tbl_PackagePrice { get; set; }
        public virtual DbSet<tbl_Permission> tbl_Permission { get; set; }
        public virtual DbSet<tbl_Proposal> tbl_Proposal { get; set; }
        public virtual DbSet<tbl_ProposalCommission> tbl_ProposalCommission { get; set; }
        public virtual DbSet<tbl_ProposalDetail> tbl_ProposalDetail { get; set; }
        public virtual DbSet<tbl_ProposalDocument> tbl_ProposalDocument { get; set; }
        public virtual DbSet<tbl_ProposalFavorite> tbl_ProposalFavorite { get; set; }
        public virtual DbSet<tbl_ProposalLikeDislike> tbl_ProposalLikeDislike { get; set; }
        public virtual DbSet<tbl_ProposalUserGroup> tbl_ProposalUserGroup { get; set; }
        public virtual DbSet<tbl_ProposalUserSave> tbl_ProposalUserSave { get; set; }
        public virtual DbSet<tbl_ProposalUserState> tbl_ProposalUserState { get; set; }
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
        public virtual DbSet<tbl_SMSDetail> tbl_SMSDetail { get; set; }
        public virtual DbSet<tbl_SMSModel> tbl_SMSModel { get; set; }
        public virtual DbSet<tbl_SMSReport> tbl_SMSReport { get; set; }
        public virtual DbSet<tbl_SMSReportShort> tbl_SMSReportShort { get; set; }
        public virtual DbSet<tbl_SMSSenderInfo> tbl_SMSSenderInfo { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
        public virtual DbSet<tbl_UserDocument> tbl_UserDocument { get; set; }
        public virtual DbSet<tbl_UserGroup> tbl_UserGroup { get; set; }
        public virtual DbSet<tbl_UserPhoneInforamtion> tbl_UserPhoneInforamtion { get; set; }
        public virtual DbSet<tbl_UserRole> tbl_UserRole { get; set; }
        public virtual DbSet<tbl_UserService> tbl_UserService { get; set; }
        public virtual DbSet<tbl_UserServiceRating> tbl_UserServiceRating { get; set; }
        public virtual DbSet<log_DataChange> log_DataChange { get; set; }
        public virtual DbSet<log_Login> log_Login { get; set; }
    }
}
