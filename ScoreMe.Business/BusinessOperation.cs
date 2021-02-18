using ScoreMe.DAL.Model;
using ScoreMe.Business.Util;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Enum;
using ScoreMe.DAL.ErrorManagment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreMe.DAL.Repositories;

namespace ScoreMe.Business
{
    public class BusinessOperation
    {
        #region User
        public BaseOutput ChangePasswordByUserName(UserInfo item, Int64 LoginUserID, out tbl_User itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                if (item.Newpassword != item.ConfirmPassword)
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.PasswordAndConfirmPasswordCode, CustomError.PasswordAndConfirmPasswordDesc, "");

                }


                tbl_User user = cRUDOperation.GetUserByUserName(item.UserName);
                if (user == null)
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.UserNameNotFoundCode, CustomError.UserNameNotFoundDesc, "");

                }
                string encryptedPassword = UserUtil.MD5HashedPassword(item.Password);
                tbl_User validUser = cRUDOperation.ValidLogin(item.UserName, encryptedPassword);
                if (validUser != null)
                {
                    string encryptedNewPassword = UserUtil.MD5HashedPassword(item.Newpassword);
                    tbl_User _User = cRUDOperation.ChangePassword(validUser.ID, LoginUserID, encryptedNewPassword);
                    if (_User != null)
                    {
                        itemOut = _User;
                        return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                    }
                    else
                    {
                        itemOut = null;
                        return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, "");

                    }

                }
                else
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.PasswordIncorrectCode, CustomError.PasswordIncorrectDesc, "");

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public BaseOutput ResetPasswordByUserName(UserInfo item, Int64 LoginUserID, out tbl_User itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                if (item.Newpassword != item.ConfirmPassword)
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.PasswordAndConfirmPasswordCode, CustomError.PasswordAndConfirmPasswordDesc, "");

                }


                tbl_User user = cRUDOperation.GetUserByUserName(item.UserName);
                if (user == null)
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.UserNameNotFoundCode, CustomError.UserNameNotFoundDesc, "");

                }
                else
                {
                    string encryptedNewPassword = UserUtil.MD5HashedPassword(item.Newpassword);
                    tbl_User _User = cRUDOperation.ChangePassword(user.ID, LoginUserID, encryptedNewPassword);
                    if (_User != null)
                    {
                        itemOut = _User;
                        return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                    }
                    else
                    {
                        itemOut = null;
                        return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, "");

                    }
                }
             
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
        #region Account
        public BaseOutput ValidLogin(string userName, string userPassword, out tbl_User itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;

            try
            {
                tbl_User user = cRUDOperation.GetUserByUserName(userName);
                if (user == null)
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.UserNameNotFoundCode, CustomError.UserNameNotFoundDesc, "");

                }
                string encryptedPassword = UserUtil.MD5HashedPassword(userPassword);
                tbl_User validUser = cRUDOperation.ValidLogin(userName, encryptedPassword);
                if (validUser != null)
                {
                    itemOut = validUser;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
                }
                else
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.PasswordIncorrectCode, CustomError.PasswordIncorrectDesc, "");

                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Provider
        public BaseOutput AddProviderWithUser(Provider item, out Provider itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_EnumValue enumValue = cRUDOperation.GetEnumValueByName("Provider");

                tbl_User user = new tbl_User()
                {
                    UserName = item.UserName,
                    Password = UserUtil.MD5HashedPassword(item.Password),
                    UserType_EVID = enumValue.ID,


                };
                tbl_User _User = cRUDOperation.GetUserByUserName(user.UserName);
                if (_User == null)
                {
                    tbl_Provider providerVoen = cRUDOperation.GetProviderByVOEN(item.VOEN);
                    if (providerVoen != null)
                    {
                        itemOut = null;
                        return baseOutput = new BaseOutput(true, CustomError.UniqueVOENErrorCode, CustomError.UniqueVOENErrorDesc, "");

                    }
                    tbl_User userDB = cRUDOperation.AddUser(user);
                    if (userDB != null)
                    {
                        tbl_Provider provider = new tbl_Provider()
                        {

                            UserId = userDB.ID,
                            Name = item.Name,
                            Type = item.Type,
                            Description = item.Description,
                            RegionId = item.RegionId,
                            Address = item.Address,
                            RelatedPersonName = item.RelatedPersonName,
                            RelatedPersonPhone = item.RelatedPersonPhone,
                            RelatedPersonProfession = item.RelatedPersonProfession,
                            RP_HomePhone = item.RP_HomePhone,
                            VOEN = item.VOEN,
                            ParentID = item.ParentID

                        };
                        tbl_Provider providerDB = cRUDOperation.AddProvider(provider);

                        if (providerDB != null)
                        {
                            itemOut = new Provider()
                            {
                                UserID = providerDB.UserId,
                                UserName = userDB.UserName,
                                ProviderID = providerDB.ID,
                                ParentID = providerDB.ParentID,
                                Name = providerDB.Name,
                                Type = providerDB.Type == null ? 0 : (Int64)providerDB.Type,
                                Description = providerDB.Description,
                                RegionId = providerDB.RegionId == null ? 0 : (Int64)providerDB.RegionId,
                                RelatedPersonName = providerDB.RelatedPersonName,
                                RelatedPersonPhone = providerDB.RelatedPersonPhone,
                                RelatedPersonProfession = providerDB.RelatedPersonProfession,
                                RP_HomePhone = providerDB.RP_HomePhone,
                                VOEN = providerDB.VOEN


                            };
                            return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
                        }
                        else
                        {
                            itemOut = null;
                            return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, "");
                        }


                    }
                    else
                    {
                        itemOut = null;
                        return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, "");
                    }
                }
                else
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput GetProviderByID(Int64 id, out Provider itemOut)
        {
            ProviderRepository providerRepository = new ProviderRepository();
            BaseOutput baseOutput;
            try
            {
                Provider provider = providerRepository.GetProviderByID(id);

                if (provider != null)
                {
                    itemOut = provider;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }



            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
        #region Customer
        public BaseOutput AddCustomerWithUser(Customer item, out Customer itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_EnumValue enumValue = cRUDOperation.GetEnumValueByName("Customer");

                tbl_User user = new tbl_User()
                {
                    UserName = item.UserName,
                    Password = UserUtil.MD5HashedPassword(item.Password),
                    UserType_EVID = enumValue.ID,


                };
                tbl_User _User = cRUDOperation.GetUserByUserName(user.UserName);
                if (_User == null)
                {
                    tbl_User userDB = cRUDOperation.AddUser(user);
                    if (userDB != null)
                    {
                        tbl_Customer customer = new tbl_Customer()
                        {
                            UserId = userDB.ID,
                            Name = item.Name,
                            Surname = item.Surname,
                            FatherName = item.FatherName,
                            IdentityCode = item.IdentityCode,
                            PhoneNumber = item.PhoneNumber,
                            Email = item.Email,
                            RegionId = item.RegionId,
                            Address = item.Address

                        };
                        tbl_Customer customerDB = cRUDOperation.AddCustomer(customer);

                        if (customerDB != null)
                        {
                            itemOut = new Customer()
                            {
                                UserID = customerDB.UserId,
                                UserName = userDB.UserName,
                                CustomerID = customerDB.ID,
                                Name = customerDB.Name,
                                Surname = customerDB.Surname,
                                FatherName = customerDB.FatherName,
                                IdentityCode = customerDB.IdentityCode,
                                PhoneNumber = customerDB.PhoneNumber,
                                Email = customerDB.Email,
                                RegionId = customerDB.RegionId == null ? 0 : (Int64)customerDB.RegionId,
                                Address = customerDB.Address

                            };
                            return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
                        }
                        else
                        {
                            itemOut = null;
                            return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, "");
                        }


                    }
                    else
                    {
                        itemOut = null;
                        return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, "");
                    }
                }
                else
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion

        #region Proposal
        public BaseOutput AddProposalWithDetail(Proposal item, out Proposal itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                tbl_Proposal proposal = new tbl_Proposal()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Note = item.Note,
                    ProviderID = item.ProviderID,
                    IsPublic = item.IsPublic,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate

                };

                tbl_Proposal _Proposal = cRUDOperation.AddProposal(proposal);

                if (_Proposal != null)
                {
                    itemOut = new Proposal()
                    {
                        ID = _Proposal.ID,
                        Name = _Proposal.Name,
                        Description = _Proposal.Description,
                        Note = _Proposal.Note,
                        ProviderID = _Proposal.ProviderID,
                        IsPublic = _Proposal.IsPublic,
                        StartDate = _Proposal.StartDate,
                        EndDate = _Proposal.EndDate
                    };
                    foreach (var pDetail in item.ProposalDetails)
                    {
                        tbl_ProposalDetail proposalDetail = new tbl_ProposalDetail()
                        {
                            ProposalID = _Proposal.ID,
                            ProposolKey = pDetail.ProposolKey,
                            ProposolValue = pDetail.ProposolValue,
                        };

                        tbl_ProposalDetail _ProposalDetail = cRUDOperation.AddProposalDetail(proposalDetail);

                    }

                    if (!_Proposal.IsPublic)
                    {
                        foreach (ProposalUserGroup userGroup in item.ProposalUserGroups)
                        {
                            tbl_ProposalUserGroup proposalUserGroup = new tbl_ProposalUserGroup()
                            {
                                ProposalID = _Proposal.ID,
                                GroupID = userGroup.GroupID,


                            };

                            tbl_ProposalUserGroup _ProposalUserGroup = cRUDOperation.AddProposalUserGroup(proposalUserGroup);

                        }
                    }

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddProposalWithDetailNew(Proposal item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_Proposal proposal = new tbl_Proposal()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Note = item.Note,
                    ProviderID = item.ProviderID,
                    IsPublic = item.IsPublic,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate

                };

                List<tbl_ProposalDetail> tbl_ProposalDetails = new List<tbl_ProposalDetail>();
                List<tbl_ProposalUserGroup> tbl_ProposalUserGroups = new List<tbl_ProposalUserGroup>();
                foreach (var pDetail in item.ProposalDetails)
                {
                    tbl_ProposalDetail proposalDetail = new tbl_ProposalDetail()
                    {
                        ProposolKey = pDetail.ProposolKey,
                        ProposolValue = pDetail.ProposolValue,
                    };
                    tbl_ProposalDetails.Add(proposalDetail);
                }

                if (!item.IsPublic)
                {
                    foreach (ProposalUserGroup userGroup in item.ProposalUserGroups)
                    {
                        tbl_ProposalUserGroup proposalUserGroup = new tbl_ProposalUserGroup()
                        {
                            GroupID = userGroup.GroupID,
                        };
                        tbl_ProposalUserGroups.Add(proposalUserGroup);

                    }
                }
                tbl_Proposal _Proposal = cRUDOperation.AddProposalNew(proposal, tbl_ProposalDetails, tbl_ProposalUserGroups);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalByID(Int64 id, out Proposal proposal)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            proposal = null;
            try
            {

                tbl_Proposal _ProposalObj = cRUDOperation.GetProposalById(id);


                if (_ProposalObj != null)
                {
                    tbl_Provider _Provider = cRUDOperation.GetProviderById(_ProposalObj.ProviderID);
                    proposal = new Proposal()
                    {
                        ID = _ProposalObj.ID,
                        Name = _ProposalObj.Name,
                        ProviderName = _Provider == null ? String.Empty : _Provider.Name,
                        Description = _ProposalObj.Description,
                        Note = _ProposalObj.Note,
                        ProviderID = _ProposalObj.ProviderID,
                        UserID = _Provider == null ? 0 : _Provider.UserId,
                        IsPublic = _ProposalObj.IsPublic,
                        StartDate = _ProposalObj.StartDate,
                        EndDate = _ProposalObj.EndDate,

                    };

                    List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                    List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposal.ID);

                    foreach (var item in tbl_ProposalDetails)
                    {
                        ProposalDetail proposalDetail = new ProposalDetail()
                        {
                            ID = item.ID,
                            ProposalID = item.ProposalID,
                            ProposolKey = item.ProposolKey,
                            ProposolValue = item.ProposolValue,
                        };
                        proposalDetails.Add(proposalDetail);

                    }
                    proposal.ProposalDetails = proposalDetails;

                    if (!proposal.IsPublic)
                    {
                        List<ProposalUserGroup> proposalUserGroups = new List<ProposalUserGroup>();
                        List<tbl_ProposalUserGroup> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(proposal.ID);

                        foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                        {
                            ProposalUserGroup proposalUserGroup = new ProposalUserGroup()
                            {
                                ID = userGroup.ID,
                                ProposalID = userGroup.ProposalID,
                                GroupID = userGroup.GroupID,


                            };

                            proposalUserGroups.Add(proposalUserGroup);

                        }
                        proposal.ProposalUserGroups = proposalUserGroups;
                    }
                    proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposals(out List<Proposal> proposals)
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            proposals = null;
            try
            {

                List<tbl_Proposal> tbl_Proposals = cRUDOperation.GetProposals();
                proposals = new List<Proposal>();

                if (tbl_Proposals.Count > 0)
                {
                    foreach (var proposalItem in tbl_Proposals)
                    {
                        tbl_Provider _Provider = cRUDOperation.GetProviderById(proposalItem.ProviderID);
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ID,
                            Name = proposalItem.Name,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = _Provider == null ? 0 : _Provider.UserId,
                            ProviderName = _Provider == null ? String.Empty : _Provider.Name,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,

                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ID);

                        foreach (var detailItem in tbl_ProposalDetails)
                        {
                            ProposalDetail proposalDetail = new ProposalDetail()
                            {
                                ID = detailItem.ID,
                                ProposalID = detailItem.ProposalID,
                                ProposolKey = detailItem.ProposolKey,
                                ProposolValue = detailItem.ProposolValue,
                            };
                            proposalDetails.Add(proposalDetail);

                        }
                        proposal.ProposalDetails = proposalDetails;

                        if (!proposal.IsPublic)
                        {
                            List<ProposalUserGroup> proposalUserGroups = new List<ProposalUserGroup>();
                            List<tbl_ProposalUserGroup> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(proposal.ID);

                            foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                            {
                                ProposalUserGroup proposalUserGroup = new ProposalUserGroup()
                                {
                                    ID = userGroup.ID,
                                    ProposalID = userGroup.ProposalID,
                                    GroupID = userGroup.GroupID,


                                };

                                proposalUserGroups.Add(proposalUserGroup);

                            }
                            proposal.ProposalUserGroups = proposalUserGroups;
                        }
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);

                    }


                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalsByProviderID(Int64 providerid, out List<Proposal> proposals)
        {
            ProposalRepository repository = new ProposalRepository();
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            proposals = null;
            try
            {

                List<tbl_Proposal> tbl_Proposals = cRUDOperation.GetProposalsByProviderID(providerid);
                tbl_Provider _Provider = cRUDOperation.GetProviderById(providerid);

                proposals = new List<Proposal>();

                if (tbl_Proposals.Count > 0)
                {

                    foreach (var proposalItem in tbl_Proposals)
                    {
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ID,
                            Name = proposalItem.Name,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = _Provider == null ? 0 : _Provider.UserId,
                            ProviderName = _Provider == null ? String.Empty : _Provider.Name,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,

                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ID);

                        foreach (var detailItem in tbl_ProposalDetails)
                        {
                            ProposalDetail proposalDetail = new ProposalDetail()
                            {
                                ID = detailItem.ID,
                                ProposalID = detailItem.ProposalID,
                                ProposolKey = detailItem.ProposolKey,
                                ProposolValue = detailItem.ProposolValue,
                            };
                            proposalDetails.Add(proposalDetail);

                        }
                        proposal.ProposalDetails = proposalDetails;

                        if (!proposal.IsPublic)
                        {
                            List<ProposalUserGroup> proposalUserGroups = new List<ProposalUserGroup>();
                            List<tbl_ProposalUserGroup> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(proposal.ID);

                            foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                            {
                                ProposalUserGroup proposalUserGroup = new ProposalUserGroup()
                                {
                                    ID = userGroup.ID,
                                    ProposalID = userGroup.ProposalID,
                                    GroupID = userGroup.GroupID,


                                };

                                proposalUserGroups.Add(proposalUserGroup);

                            }
                            proposal.ProposalUserGroups = proposalUserGroups;
                        }
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);


                    }


                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalsWithStateByProviderID(Int64 providerid, out List<Proposal> proposals)
        {
            ProposalRepository repository = new ProposalRepository();
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            proposals = null;
            try
            {

                List<tbl_Proposal> tbl_Proposals = cRUDOperation.GetProposalsByProviderID(providerid);
                tbl_Provider _Provider = cRUDOperation.GetProviderById(providerid);

                proposals = new List<Proposal>();

                if (tbl_Proposals.Count > 0)
                {
                    foreach (var proposalItem in tbl_Proposals)
                    {
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ID,
                            Name = proposalItem.Name,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = _Provider == null ? 0 : _Provider.UserId,
                            ProviderName = _Provider == null ? String.Empty : _Provider.Name,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,

                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ID);

                        foreach (var detailItem in tbl_ProposalDetails)
                        {
                            ProposalDetail proposalDetail = new ProposalDetail()
                            {
                                ID = detailItem.ID,
                                ProposalID = detailItem.ProposalID,
                                ProposolKey = detailItem.ProposolKey,
                                ProposolValue = detailItem.ProposolValue,
                            };
                            proposalDetails.Add(proposalDetail);

                        }
                        proposal.ProposalDetails = proposalDetails;

                        if (!proposal.IsPublic)
                        {
                            List<ProposalUserGroup> proposalUserGroups = new List<ProposalUserGroup>();
                            List<tbl_ProposalUserGroup> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(proposal.ID);

                            foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                            {
                                ProposalUserGroup proposalUserGroup = new ProposalUserGroup()
                                {
                                    ID = userGroup.ID,
                                    ProposalID = userGroup.ProposalID,
                                    GroupID = userGroup.GroupID,


                                };

                                proposalUserGroups.Add(proposalUserGroup);

                            }
                            proposal.ProposalUserGroups = proposalUserGroups;
                        }
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        List<ProposalUserState> ProposalUserStates = repository.GetProposalUserStateByProposalID(proposal.ID);
                        proposal.ProposalUserStateList = ProposalUserStates;
                        proposals.Add(proposal);
                    }


                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalsByUserName(string username, out List<Proposal> proposals)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            ProposalRepository repository = new ProposalRepository();
            BaseOutput baseOutput;
            proposals = null;
            try
            {
                tbl_User userDB = cRUDOperation.GetUserByUserName(username);
                List<tbl_Proposal> tbl_Proposals = cRUDOperation.GetProposalsByUserID(userDB.ID);
                proposals = new List<Proposal>();

                if (tbl_Proposals.Count > 0)
                {
                    foreach (var proposalItem in tbl_Proposals)
                    {
                        tbl_Provider _Provider = cRUDOperation.GetProviderById(proposalItem.ProviderID);
                        int dislikecount = 0;
                        int likecount = cRUDOperation.GetUserProposalLikeDislikeCount(proposalItem.ID, userDB.ID, out dislikecount);
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ID,
                            Name = proposalItem.Name,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = _Provider == null ? 0 : _Provider.UserId,
                            ProviderName = _Provider == null ? String.Empty : _Provider.Name,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            IsLike = likecount > 0 ? true : false,
                            IsDislike = dislikecount > 0 ? true : false,
                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ID);

                        foreach (var detailItem in tbl_ProposalDetails)
                        {
                            ProposalDetail proposalDetail = new ProposalDetail()
                            {
                                ID = detailItem.ID,
                                ProposalID = detailItem.ProposalID,
                                ProposolKey = detailItem.ProposolKey,
                                ProposolValue = detailItem.ProposolValue,
                            };
                            proposalDetails.Add(proposalDetail);

                        }
                        proposal.ProposalDetails = proposalDetails;

                        if (!proposal.IsPublic)
                        {
                            List<ProposalUserGroup> proposalUserGroups = new List<ProposalUserGroup>();
                            List<tbl_ProposalUserGroup> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(proposal.ID);

                            foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                            {
                                ProposalUserGroup proposalUserGroup = new ProposalUserGroup()
                                {
                                    ID = userGroup.ID,
                                    ProposalID = userGroup.ProposalID,
                                    GroupID = userGroup.GroupID,


                                };

                                proposalUserGroups.Add(proposalUserGroup);

                            }
                            proposal.ProposalUserGroups = proposalUserGroups;
                        }

                        ProposalUserState proposalUserState = repository.GetProposalUserStateByUserID(userDB.ID, proposal.ID);
                        proposal.ProposalUserState = proposalUserState;
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);



                    }


                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalWithDetailsByIsPublic(string username, out List<Proposal> proposals)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            ProposalRepository repository = new ProposalRepository();
            BaseOutput baseOutput;
            proposals = null;
            try
            {
                tbl_User userDB = cRUDOperation.GetUserByUserName(username);
                List<tbl_Proposal> tbl_Proposals = cRUDOperation.GetProposalsByIsPublic();
                proposals = new List<Proposal>();

                if (tbl_Proposals.Count > 0)
                {
                    foreach (var proposalItem in tbl_Proposals)
                    {
                        tbl_Provider _Provider = cRUDOperation.GetProviderById(proposalItem.ProviderID);
                        int dislikecount = 0;
                        int likecount = cRUDOperation.GetUserProposalLikeDislikeCount(proposalItem.ID, userDB.ID, out dislikecount);
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ID,
                            Name = proposalItem.Name,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = _Provider == null ? 0 : _Provider.UserId,
                            ProviderName = _Provider == null ? String.Empty : _Provider.Name,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            IsLike = likecount > 0 ? true : false,
                            IsDislike = dislikecount > 0 ? true : false,
                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ID);

                        foreach (var detailItem in tbl_ProposalDetails)
                        {
                            ProposalDetail proposalDetail = new ProposalDetail()
                            {
                                ID = detailItem.ID,
                                ProposalID = detailItem.ProposalID,
                                ProposolKey = detailItem.ProposolKey,
                                ProposolValue = detailItem.ProposolValue,
                            };
                            proposalDetails.Add(proposalDetail);

                        }
                        proposal.ProposalDetails = proposalDetails;

                        ProposalUserState proposalUserState = repository.GetProposalUserStateByUserID(userDB.ID, proposal.ID);
                        proposal.ProposalUserState = proposalUserState;
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);



                    }


                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateProposalWithDetail(Proposal item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_Proposal proposal = new tbl_Proposal()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Description = item.Description,
                    Note = item.Note,
                    ProviderID = item.ProviderID,
                    IsPublic = item.IsPublic,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate

                };

                tbl_Proposal _Proposal = cRUDOperation.UpdateProposal(proposal);

                if (_Proposal != null)
                {
                    foreach (var pDetail in item.ProposalDetails)
                    {
                        tbl_ProposalDetail proposalDetail = new tbl_ProposalDetail()
                        {
                            ID = pDetail.ID,
                            ProposalID = _Proposal.ID,
                            ProposolKey = pDetail.ProposolKey,
                            ProposolValue = pDetail.ProposolValue,
                        };
                        if (proposalDetail.ID != 0)
                        {
                            tbl_ProposalDetail _ProposalDetail = cRUDOperation.UpdateProposalDetail(proposalDetail);
                        }
                        else
                        {
                            tbl_ProposalDetail _ProposalDetail = cRUDOperation.AddProposalDetail(proposalDetail);
                        }

                    }

                    if (!_Proposal.IsPublic)
                    {
                        foreach (ProposalUserGroup userGroup in item.ProposalUserGroups)
                        {
                            tbl_ProposalUserGroup proposalUserGroup = new tbl_ProposalUserGroup()
                            {
                                ID = userGroup.ID,
                                ProposalID = _Proposal.ID,
                                GroupID = userGroup.GroupID,


                            };
                            if (proposalUserGroup.ID != 0)
                            {
                                tbl_ProposalUserGroup _ProposalUserGroup = cRUDOperation.UpdateProposalUserGroup(proposalUserGroup);
                            }
                            else
                            {
                                tbl_ProposalUserGroup _ProposalUserGroup = cRUDOperation.AddProposalUserGroup(proposalUserGroup);
                            }


                        }
                    }

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteProposalWithDetail(Int64 id)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {

                tbl_Proposal _Proposal = cRUDOperation.GetProposalById(id);

                if (_Proposal != null)
                {
                    List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(_Proposal.ID);

                    foreach (var item in tbl_ProposalDetails)
                    {

                        tbl_ProposalDetail tbl_ProposalDetail = cRUDOperation.DeleteProposalDetail(item.ID, 0);

                    }

                    List<tbl_ProposalUserGroup> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(_Proposal.ID);

                    foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                    {


                        tbl_ProposalUserGroup tbl_ProposalUserGroup = cRUDOperation.DeleteProposalUserGroup(userGroup.ID, 0);

                    }

                    tbl_Proposal tbl_Proposal = cRUDOperation.DeleteProposal(id, 0);
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        private List<Int64> GetProposalDocuments(Int64 proposalID)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            List<tbl_ProposalDocument> ProposalDocumentList = cRUDOperation.GetProposalDocumentsByProposalID(proposalID);
            List<Int64> proposalDocumentIds = new List<long>();
            foreach (var item in ProposalDocumentList)
            {
                proposalDocumentIds.Add(item.ID);
            }
            return proposalDocumentIds;
        }
        #endregion

        #region SMSModel
        public BaseOutput GetSMSModels(out List<SMSModel> sMSModels)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            sMSModels = null;
            try
            {

                List<tbl_SMSModel> tbl_SMSModels = cRUDOperation.GetSMSModels();
                sMSModels = new List<SMSModel>();

                if (tbl_SMSModels.Count > 0)
                {
                    foreach (var tbl_SMSModel in tbl_SMSModels)
                    {
                        SMSModel sMSModel = new SMSModel()
                        {
                            ID = tbl_SMSModel.ID,
                            TotalMessageCount = tbl_SMSModel.TotalMessageCount,
                            ShortMessageCount = tbl_SMSModel.ShortMessageCount,

                            OutMessageCount = tbl_SMSModel.OutMessageCount,
                            InMessageCount = tbl_SMSModel.InMessageCount,

                            OutMessageForeignCount = tbl_SMSModel.OutMessageForeignCount,
                            InMessageForeigCount = tbl_SMSModel.InMessageForeigCount,

                            OutMessageRoamingCount = tbl_SMSModel.OutMessageRoamingCount,
                            InMessageRoamingCount = tbl_SMSModel.InMessageRoamingCount,

                            BeginDate = tbl_SMSModel.BeginDate,
                            EndDate = tbl_SMSModel.EndDate,
                        };

                        List<tbl_SMSDetail> tbl_SMSDetails = cRUDOperation.GetSMSDetailsByModelID(sMSModel.ID);


                        sMSModel.SMSDetails = tbl_SMSDetails;
                        sMSModels.Add(sMSModel);

                    }


                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetSMSModelsByID(Int64 id, out SMSModel sMSModel)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            sMSModel = null;
            try
            {

                tbl_SMSModel tbl_SMSModel = cRUDOperation.GetSMSModelByID(id);
                if (tbl_SMSModel != null)
                {
                    sMSModel = new SMSModel()
                    {
                        ID = tbl_SMSModel.ID,
                        TotalMessageCount = tbl_SMSModel.TotalMessageCount,
                        ShortMessageCount = tbl_SMSModel.ShortMessageCount,

                        OutMessageCount = tbl_SMSModel.OutMessageCount,
                        InMessageCount = tbl_SMSModel.InMessageCount,

                        OutMessageForeignCount = tbl_SMSModel.OutMessageForeignCount,
                        InMessageForeigCount = tbl_SMSModel.InMessageForeigCount,

                        OutMessageRoamingCount = tbl_SMSModel.OutMessageRoamingCount,
                        InMessageRoamingCount = tbl_SMSModel.InMessageRoamingCount,

                        BeginDate = tbl_SMSModel.BeginDate,
                        EndDate = tbl_SMSModel.EndDate,
                    };

                    List<tbl_SMSDetail> tbl_SMSDetails = cRUDOperation.GetSMSDetailsByModelID(sMSModel.ID);
                    sMSModel.SMSDetails = tbl_SMSDetails;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetLastSMSModelByUserName(string userName, out SMSModel sMSModel)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            sMSModel = null;
            try
            {

                tbl_SMSModel tbl_SMSModel = cRUDOperation.GetLastSMSModelByUserName(userName);
                if (tbl_SMSModel != null)
                {
                    sMSModel = new SMSModel()
                    {
                        ID = tbl_SMSModel.ID,
                        TotalMessageCount = tbl_SMSModel.TotalMessageCount,
                        ShortMessageCount = tbl_SMSModel.ShortMessageCount,

                        OutMessageCount = tbl_SMSModel.OutMessageCount,
                        InMessageCount = tbl_SMSModel.InMessageCount,

                        OutMessageForeignCount = tbl_SMSModel.OutMessageForeignCount,
                        InMessageForeigCount = tbl_SMSModel.InMessageForeigCount,

                        OutMessageRoamingCount = tbl_SMSModel.OutMessageRoamingCount,
                        InMessageRoamingCount = tbl_SMSModel.InMessageRoamingCount,

                        BeginDate = tbl_SMSModel.BeginDate,
                        EndDate = tbl_SMSModel.EndDate,
                    };
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddSMSModel(SMSModel item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_SMSModel sMSModel = new tbl_SMSModel()
                {
                    UserID = item.UserID,
                    TotalMessageCount = item.TotalMessageCount,
                    ShortMessageCount = item.ShortMessageCount,

                    OutMessageCount = item.OutMessageCount,
                    InMessageCount = item.InMessageCount,

                    OutMessageForeignCount = item.OutMessageForeignCount,
                    InMessageForeigCount = item.InMessageForeigCount,

                    OutMessageRoamingCount = item.OutMessageRoamingCount,
                    InMessageRoamingCount = item.InMessageRoamingCount,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                };

                List<tbl_SMSDetail> tbl_SMSDetails = new List<tbl_SMSDetail>();

                tbl_SMSDetails = item.SMSDetails;
                tbl_SMSModel _SMSModel = cRUDOperation.AddSMSModel(sMSModel, tbl_SMSDetails);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateSMSModel(SMSModel item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_SMSModel tbl_SMSModel = new tbl_SMSModel()
                {
                    ID = item.ID,
                    UserID = item.UserID,
                    TotalMessageCount = item.TotalMessageCount,
                    ShortMessageCount = item.ShortMessageCount,

                    OutMessageCount = item.OutMessageCount,
                    InMessageCount = item.InMessageCount,

                    OutMessageForeignCount = item.OutMessageForeignCount,
                    InMessageForeigCount = item.InMessageForeigCount,

                    OutMessageRoamingCount = item.OutMessageRoamingCount,
                    InMessageRoamingCount = item.InMessageRoamingCount,

                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,

                };

                tbl_SMSModel _SMSModel = cRUDOperation.UpdateSMSModel(tbl_SMSModel);

                if (_SMSModel != null)
                {
                    foreach (var smsDetail in item.SMSDetails)
                    {
                        tbl_SMSDetail tbl_SMSDetail = cRUDOperation.UpdateSMSDetail(smsDetail);
                    }

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteSMSModel(Int64 id)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {

                tbl_SMSModel _SMSModel = cRUDOperation.GetSMSModelByID(id);

                if (_SMSModel != null)
                {
                    List<tbl_SMSDetail> tbl_SMSDetails = cRUDOperation.GetSMSDetailsByModelID(_SMSModel.ID);

                    foreach (var item in tbl_SMSDetails)
                    {

                        tbl_SMSDetail tbl_SMSDetail = cRUDOperation.DeleteSMSDetail(item.ID, 0);

                    }



                    tbl_SMSModel tbl_SMSModel = cRUDOperation.DeleteSMSModel(id, 0);
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion

        #region CALLModel
        public BaseOutput GetCALLModels(out List<CALLModel> callModels)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            callModels = null;
            try
            {

                List<tbl_CALLModel> tbl_CALLModels = cRUDOperation.GetCALLModels();
                callModels = new List<CALLModel>();

                if (tbl_CALLModels.Count > 0)
                {
                    foreach (var item in tbl_CALLModels)
                    {
                        CALLModel callModel = new CALLModel()
                        {
                            ID = item.ID,
                            UserID = item.UserID,
                            TotalCallCount = item.TotalCallCount,
                            OutCallCount = item.OutCallCount,
                            OutCallSecond = item.OutCallSecond,
                            InCallCount = item.InCallCount,                     
                            InCallSecond = item.InCallSecond,
                            MissedCallCount = item.MissedCallCount,
                            OutCallForeignCount = item.OutCallForeignCount,
                            OutCallForeignSecond = item.OutCallForeignSecond,
                            InCallForeignCount = item.InCallForeignCount,
                            InCallForeignSecond = item.InCallForeignSecond,
                            OutCallRoamingCount = item.OutCallRoamingCount,
                            OutCallRoamingSecond = item.OutCallRoamingSecond,
                            InCallRoamingCount = item.InCallRoamingCount,
                            InCallRoamingSecond = item.InCallRoamingSecond,
                            BeginDate = item.BeginDate,
                            EndDate = item.EndDate
                        };

                        List<tbl_CALLDetail> tbl_CALLDetails = cRUDOperation.GetCALLDetailsByModelID(callModel.ID);


                        callModel.CALLDetails = tbl_CALLDetails;
                        callModels.Add(callModel);

                    }


                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetCALLModelsByID(Int64 id, out CALLModel callModel)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            callModel = null;
            try
            {

                tbl_CALLModel item = cRUDOperation.GetCALLModelByID(id);
                if (item != null)
                {
                    callModel = new CALLModel()
                    {
                        ID = item.ID,
                        UserID = item.UserID,
                        TotalCallCount = item.TotalCallCount,
                        OutCallCount = item.OutCallCount,
                        OutCallSecond = item.OutCallSecond,
                        InCallCount = item.InCallCount,              
                        InCallSecond = item.InCallSecond,
                        MissedCallCount = item.MissedCallCount,
                        OutCallForeignCount = item.OutCallForeignCount,
                        OutCallForeignSecond = item.OutCallForeignSecond,
                        InCallForeignCount = item.InCallForeignCount,
                        InCallForeignSecond = item.InCallForeignSecond,
                        OutCallRoamingCount = item.OutCallRoamingCount,
                        OutCallRoamingSecond = item.OutCallRoamingSecond,
                        InCallRoamingCount = item.InCallRoamingCount,
                        InCallRoamingSecond = item.InCallRoamingSecond,
                        BeginDate = item.BeginDate,
                        EndDate = item.EndDate
                    };

                    List<tbl_CALLDetail> tblCALLDetails = cRUDOperation.GetCALLDetailsByModelID(callModel.ID);
                    callModel.CALLDetails = tblCALLDetails;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetLastCALLModelByUserName(string userName, out CALLModel callModel)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            callModel = null;
            try
            {

                tbl_CALLModel item = cRUDOperation.GetLastCALLModelByUserName(userName);
                if (item != null)
                {
                    callModel = new CALLModel()
                    {
                        ID = item.ID,
                        UserID=item.UserID,
                        TotalCallCount = item.TotalCallCount,
                        OutCallCount = item.OutCallCount,
                        OutCallSecond = item.OutCallSecond,
                        InCallCount = item.InCallCount,               
                        InCallSecond = item.InCallSecond,
                        MissedCallCount = item.MissedCallCount,
                        OutCallForeignCount = item.OutCallForeignCount,
                        OutCallForeignSecond = item.OutCallForeignSecond,
                        InCallForeignCount = item.InCallForeignCount,
                        InCallForeignSecond = item.InCallForeignSecond,
                        OutCallRoamingCount = item.OutCallRoamingCount,
                        OutCallRoamingSecond = item.OutCallRoamingSecond,
                        InCallRoamingCount = item.InCallRoamingCount,
                        InCallRoamingSecond = item.InCallRoamingSecond,
                        BeginDate = item.BeginDate,
                        EndDate = item.EndDate
                    };

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddCALLModel(CALLModel item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_CALLModel callModel = new tbl_CALLModel()
                {
                    UserID = item.UserID,
                    TotalCallCount = item.TotalCallCount,
                    OutCallCount = item.OutCallCount,
                    OutCallSecond = item.OutCallSecond,
                    InCallCount = item.InCallCount,             
                    InCallSecond = item.InCallSecond,
                    MissedCallCount = item.MissedCallCount,
                    OutCallForeignCount = item.OutCallForeignCount,
                    OutCallForeignSecond = item.OutCallForeignSecond,
                    InCallForeignCount = item.InCallForeignCount,
                    InCallForeignSecond = item.InCallForeignSecond,
                    OutCallRoamingCount = item.OutCallRoamingCount,
                    OutCallRoamingSecond = item.OutCallRoamingSecond,
                    InCallRoamingCount = item.InCallRoamingCount,
                    InCallRoamingSecond = item.InCallRoamingSecond,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                };

                List<tbl_CALLDetail> tblCALLDetails = new List<tbl_CALLDetail>();

                tblCALLDetails = item.CALLDetails;
                tbl_CALLModel _CALLModel = cRUDOperation.AddCALLModel(callModel, tblCALLDetails);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateCALLModel(CALLModel item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_CALLModel tblCALLModel = new tbl_CALLModel()
                {
                    ID = item.ID,
                    UserID = item.UserID,
                    TotalCallCount = item.TotalCallCount,
                    OutCallCount = item.OutCallCount,
                    OutCallSecond = item.OutCallSecond,
                    InCallCount = item.InCallCount,               
                    InCallSecond = item.InCallSecond,
                    MissedCallCount = item.MissedCallCount,
                    OutCallForeignCount = item.OutCallForeignCount,
                    OutCallForeignSecond = item.OutCallForeignSecond,
                    InCallForeignCount = item.InCallForeignCount,
                    InCallForeignSecond = item.InCallForeignSecond,
                    OutCallRoamingCount = item.OutCallRoamingCount,
                    OutCallRoamingSecond = item.OutCallRoamingSecond,
                    InCallRoamingCount = item.InCallRoamingCount,
                    InCallRoamingSecond = item.InCallRoamingSecond,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                };

                tbl_CALLModel _CALLModel = cRUDOperation.UpdateCALLModel(tblCALLModel);

                if (_CALLModel != null)
                {
                    foreach (var callDetail in item.CALLDetails)
                    {
                        tbl_CALLDetail tblCALLDetail = cRUDOperation.UpdateCALLDetail(callDetail);
                    }

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteCALLModel(Int64 id)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {

                tbl_SMSModel _SMSModel = cRUDOperation.GetSMSModelByID(id);

                if (_SMSModel != null)
                {
                    List<tbl_SMSDetail> tbl_SMSDetails = cRUDOperation.GetSMSDetailsByModelID(_SMSModel.ID);

                    foreach (var item in tbl_SMSDetails)
                    {

                        tbl_SMSDetail tbl_SMSDetail = cRUDOperation.DeleteSMSDetail(item.ID, 0);

                    }



                    tbl_SMSModel tbl_SMSModel = cRUDOperation.DeleteSMSModel(id, 0);
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.UniqueUserNameErrorCode, CustomError.UniqueUserNameErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
    }

}
