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
        public BaseOutput AddProposalWithDetail(Proposal item)
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

                tbl_Proposal _Proposal = cRUDOperation.AddProposal(proposal);

                if (_Proposal != null)
                {
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
                    proposal = new Proposal()
                    {
                        ID = _ProposalObj.ID,
                        Name = _ProposalObj.Name,
                        Description = _ProposalObj.Description,
                        Note = _ProposalObj.Note,
                        ProviderID = _ProposalObj.ProviderID,
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
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ID,
                            Name = proposalItem.Name,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
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

                        tbl_ProposalDetail _ProposalDetail = cRUDOperation.UpdateProposalDetail(proposalDetail);

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

                            tbl_ProposalUserGroup _ProposalUserGroup = cRUDOperation.UpdateProposalUserGroup(proposalUserGroup);

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
        #endregion
    }

}
