using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Enum;
using ScoreMe.DAL.ErrorManagment;
using ScoreMe.DAL.Model;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.Business
{
    public class ProposalBusinessOperation
    {
        CRUDOperation operation = new CRUDOperation();
        #region Proposal


        public BaseOutput GetProposalByID(Int64 id, out Proposal proposal)
        {
            ProposalRepository repository = new ProposalRepository();
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            proposal = null;
            try
            {

                Search search = new Search()
                {
                    ProposalID = id,
                };

                ProposalDTO proposalItem = repository.SW_GePropsals(search).FirstOrDefault();

                if (proposalItem != null)
                {
                    proposal = new Proposal()
                    {
                        ID = proposalItem.ProposalID,
                        Name = proposalItem.ProposalName,
                        Description = proposalItem.Description,
                        Note = proposalItem.Note,
                        ProviderID = proposalItem.ProviderID,
                        UserID = proposalItem.OwnerUserID,
                        ProviderName = proposalItem.ProviderName,
                        IsPublic = proposalItem.IsPublic,
                        StartDate = proposalItem.StartDate,
                        EndDate = proposalItem.EndDate,
                        ProviderType = proposalItem.ProviderType,
                        ProviderTypeCode = proposalItem.ProviderTypeCode

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
                        List<Int64> groupIDS = repository.GetProposalUserGroupIDsByProposalID(proposal.ID);
                        if (groupIDS.Count > 0)
                        {
                           
                            proposal.ProposalUserGroupIds = groupIDS;
                        }
                    }
                    proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);


                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposals(out List<Proposal> proposals)
        {
            ProposalRepository repository = new ProposalRepository();
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            proposals = null;
            try
            {
                Search search = new Search();

                IList<ProposalDTO> proposalDTOs = repository.SW_GePropsals(search);
                proposals = new List<Proposal>();

                if (proposalDTOs.Count > 0)
                {
                    foreach (var proposalItem in proposalDTOs)
                    {

                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ProposalID,
                            Name = proposalItem.ProposalName,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = proposalItem.OwnerUserID,
                            ProviderName = proposalItem.ProviderName,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            ProviderType = proposalItem.ProviderType,
                            ProviderTypeCode = proposalItem.ProviderTypeCode

                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposal.ID);
                    

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
                            //List<ProposalUserGroup> proposalUserGroups = new List<ProposalUserGroup>();
                            //List<Int64> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(proposal.ID);

                            //foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                            //{
                            //    ProposalUserGroup proposalUserGroup = new ProposalUserGroup()
                            //    {
                            //        ID = userGroup.ID,
                            //        ProposalID = userGroup.ProposalID,
                            //        GroupID = userGroup.GroupID,
                            //        UserID = userGroup.UserID,

                            //    };

                            //    proposalUserGroups.Add(proposalUserGroup);

                            //}
                            //proposal.ProposalUserGroups = proposalUserGroups;

                            List<Int64> groupIDS = repository.GetProposalUserGroupIDsByProposalID(proposal.ID);
                            if (groupIDS.Count > 0)
                            {
                                ProposalUserGroupModel proposalUserGroupModel = new ProposalUserGroupModel
                                {
                                    ProposalID = proposal.ID,
                                    GroupIDs = groupIDS
                                };
                                proposal.ProposalUserGroupIds = groupIDS;
                            }
                        }
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);

                    }

                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalWithDetailsByUserName(string username,out List<Proposal> proposals)
        {
            ProposalRepository repository = new ProposalRepository();
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            proposals = null;
            try
            {
                //tbl_User userObj = cRUDOperation.GetUserByUserName(username);
                Search search = new Search();
                search.UserName = username;

                IList<ProposalDTO> proposalDTOs = repository.SW_GetProposalWithDetailsByUserName(search);
                proposals = new List<Proposal>();

                if (proposalDTOs.Count > 0)
                {
                    foreach (var proposalItem in proposalDTOs)
                    {

                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ProposalID,
                            Name = proposalItem.ProposalName,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = proposalItem.OwnerUserID,
                            ProviderName = proposalItem.ProviderName,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            ProviderType = proposalItem.ProviderType,
                            ProviderTypeCode = proposalItem.ProviderTypeCode

                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposal.ID);


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
                            //List<ProposalUserGroup> proposalUserGroups = new List<ProposalUserGroup>();
                            //List<Int64> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(proposal.ID);

                            //foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                            //{
                            //    ProposalUserGroup proposalUserGroup = new ProposalUserGroup()
                            //    {
                            //        ID = userGroup.ID,
                            //        ProposalID = userGroup.ProposalID,
                            //        GroupID = userGroup.GroupID,
                            //        UserID = userGroup.UserID,

                            //    };

                            //    proposalUserGroups.Add(proposalUserGroup);

                            //}
                            //proposal.ProposalUserGroups = proposalUserGroups;

                            List<Int64> groupIDS = repository.GetProposalUserGroupIDsByProposalID(proposal.ID);
                            if (groupIDS.Count > 0)
                            {
                                ProposalUserGroupModel proposalUserGroupModel = new ProposalUserGroupModel
                                {
                                    ProposalID = proposal.ID,
                                    GroupIDs = groupIDS
                                };
                                proposal.ProposalUserGroupIds = groupIDS;
                            }
                        }
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);

                    }

                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

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
                Search search = new Search()
                {
                    ProviderID = providerid,
                };

                IList<ProposalDTO> proposalDTOs = repository.SW_GePropsals(search);

                proposals = new List<Proposal>();

                if (proposalDTOs.Count > 0)
                {

                    foreach (var proposalItem in proposalDTOs)
                    {
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ProposalID,
                            Name = proposalItem.ProposalName,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = proposalItem.OwnerUserID,
                            ProviderName = proposalItem.ProviderName,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            ProviderType = proposalItem.ProviderType,
                            ProviderTypeCode = proposalItem.ProviderTypeCode

                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ProposalID);

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
                            List<Int64> groupIDS = repository.GetProposalUserGroupIDsByProposalID(proposal.ID);
                            if (groupIDS.Count > 0)
                            {
                                proposal.ProposalUserGroupIds = groupIDS;
                            }
                        }
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);


                    }

                }

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

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
                Search search = new Search()
                {
                    ProviderID = providerid,
                };

                IList<ProposalDTO> proposalDTOs = repository.SW_GePropsals(search);

                proposals = new List<Proposal>();

                if (proposalDTOs.Count > 0)
                {
                    foreach (var proposalItem in proposalDTOs)
                    {
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ProposalID,
                            Name = proposalItem.ProposalName,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = proposalItem.OwnerUserID,
                            ProviderName = proposalItem.ProviderName,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            ProviderType = proposalItem.ProviderType,
                            ProviderTypeCode = proposalItem.ProviderTypeCode

                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposal.ID);

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
                            List<Int64> groupIDS = repository.GetProposalUserGroupIDsByProposalID(proposal.ID);
                            if (groupIDS.Count > 0)
                            {
                                
                                proposal.ProposalUserGroupIds = groupIDS;
                            }
                        }
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        List<ProposalUserState> ProposalUserStates = repository.GetProposalUserStateByProposalID(proposal.ID);
                        proposal.ProposalUserStateList = ProposalUserStates;
                        proposals.Add(proposal);
                    }

                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalWithDetailsByProviderUserName(string username, out List<Proposal> proposals)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            ProposalRepository repository = new ProposalRepository();
            BaseOutput baseOutput;
            proposals = null;
            try
            {
                Search search = new Search()
                {
                    UserName = username,
                };

                IList<ProposalDTO> proposalDTOs = repository.SW_GetProposalWithDetailsByProviderUserName(search);
                proposals = new List<Proposal>();

                if (proposalDTOs.Count > 0)
                {
                    foreach (var proposalItem in proposalDTOs)
                    {
                        tbl_ProposalLikeDislike proposalLikeDislikeDB = cRUDOperation.GetProposalLikeDislikeByPropsalIdAndUserIDNotNull(proposalItem.ProposalID, proposalItem.UserID);
                        tbl_ProposalFavorite proposalFavoriteDB = cRUDOperation.GetProposalFavoriteByPropsalIdAndUserIDNotNull(proposalItem.ProposalID, proposalItem.UserID);
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ProposalID,
                            Name = proposalItem.ProposalName,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = proposalItem.OwnerUserID,
                            ProviderName = proposalItem.ProviderName,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            ProviderType = proposalItem.ProviderType,
                            ProviderTypeCode = proposalItem.ProviderTypeCode,
                            IsLike = proposalLikeDislikeDB.IsLike == 1 ? true : false,
                            IsDislike = proposalLikeDislikeDB.IsDislike == 1 ? true : false,
                            IsFavorite = proposalFavoriteDB.IsFavorite == 1 ? true : false,
                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ProposalID);

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
                            List<Int64> groupIDS = repository.GetProposalUserGroupIDsByProposalID(proposal.ID);
                            if (groupIDS.Count > 0)
                            {
                                proposal.ProposalUserGroupIds = groupIDS;
                            }
                        }

                        ProposalUserState proposalUserState = repository.GetProposalUserStateByUserID(proposalItem.UserID, proposalItem.ProposalID);
                        proposal.ProposalUserState = proposalUserState;
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);



                    }


                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetFavoriteProposalsByUserName(string username, out List<Proposal> proposals)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            ProposalRepository repository = new ProposalRepository();
            BaseOutput baseOutput;
            proposals = null;
            try
            {
                Search search = new Search()
                {
                    UserName = username,
                };

                IList<ProposalDTO> proposalDTOs = repository.SW_GetFavoriteProposalsByUserName(search);
                proposals = new List<Proposal>();

                if (proposalDTOs.Count > 0)
                {
                    foreach (var proposalItem in proposalDTOs)
                    {
                        tbl_ProposalLikeDislike proposalLikeDislikeDB = cRUDOperation.GetProposalLikeDislikeByPropsalIdAndUserIDNotNull(proposalItem.ProposalID, proposalItem.UserID);

                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ProposalID,
                            Name = proposalItem.ProposalName,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = proposalItem.OwnerUserID,
                            ProviderName = proposalItem.ProviderName,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            ProviderType = proposalItem.ProviderType,
                            ProviderTypeCode = proposalItem.ProviderTypeCode,
                            IsLike = proposalLikeDislikeDB.IsLike == 1 ? true : false,
                            IsDislike = proposalLikeDislikeDB.IsDislike == 1 ? true : false,
                            IsFavorite = true,
                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ProposalID);

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
                            List<Int64> groupIDS = repository.GetProposalUserGroupIDsByProposalID(proposal.ID);
                            if (groupIDS.Count > 0)
                            {
                                proposal.ProposalUserGroupIds = groupIDS;
                            }
                        }

                        ProposalUserState proposalUserState = repository.GetProposalUserStateByUserID(proposalItem.UserID, proposalItem.ProposalID);
                        proposal.ProposalUserState = proposalUserState;
                        proposal.ProposalDocumentIds = GetProposalDocuments(proposal.ID);
                        proposals.Add(proposal);



                    }

                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


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
                Search search = new Search();


                IList<ProposalDTO> proposalDTOs = repository.SW_GetProposalsByIsPublic(search);
                proposals = new List<Proposal>();

                if (proposalDTOs.Count > 0)
                {
                    foreach (var proposalItem in proposalDTOs)
                    {

                        tbl_ProposalLikeDislike proposalLikeDislikeDB = cRUDOperation.GetProposalLikeDislikeByPropsalIdAndUserIDNotNull(proposalItem.ProposalID, userDB.ID);
                        tbl_ProposalFavorite proposalFavoriteDB = cRUDOperation.GetProposalFavoriteByPropsalIdAndUserIDNotNull(proposalItem.ProposalID, userDB.ID);
                        Proposal proposal = new Proposal()
                        {
                            ID = proposalItem.ProposalID,
                            Name = proposalItem.ProposalName,
                            Description = proposalItem.Description,
                            Note = proposalItem.Note,
                            ProviderID = proposalItem.ProviderID,
                            UserID = proposalItem.OwnerUserID,
                            ProviderName = proposalItem.ProviderName,
                            IsPublic = proposalItem.IsPublic,
                            StartDate = proposalItem.StartDate,
                            EndDate = proposalItem.EndDate,
                            ProviderType = proposalItem.ProviderType,
                            ProviderTypeCode = proposalItem.ProviderTypeCode,
                            IsLike = proposalLikeDislikeDB.IsLike == 1 ? true : false,
                            IsDislike = proposalLikeDislikeDB.IsDislike == 1 ? true : false,
                            IsFavorite = proposalFavoriteDB.IsFavorite == 1 ? true : false,
                        };

                        List<ProposalDetail> proposalDetails = new List<ProposalDetail>();
                        List<tbl_ProposalDetail> tbl_ProposalDetails = cRUDOperation.GetProposalDetailsByProposalID(proposalItem.ProposalID);

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

                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
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
                        if (item.ProposalUserGroupIds!=null)
                        {
                            foreach (Int64 userGroup in item.ProposalUserGroupIds)
                            {
                                tbl_Group group = cRUDOperation.GetGroupByID(userGroup);
                                GroupBusinessOperation groupBusinessOperation = new GroupBusinessOperation();
                                List<UserDTO> userList = new List<UserDTO>();
                                Int64 pointGroupID = 0;
                                Int64 priceGroupID = 0;
                                if (group != null)
                                {
                                    try
                                    {
                                        if (group.GroupType == 2)
                                        {
                                            pointGroupID = group.ID;
                                        }
                                        else if (group.GroupType == 3)
                                        {
                                            priceGroupID = group.ID;
                                        }
                                        if (pointGroupID > 0 || priceGroupID > 0)
                                        {
                                            groupBusinessOperation.GetDynamicGroupUsersByGroupID(pointGroupID, priceGroupID, out userList);
                                            foreach (var userItem in userList)
                                            {
                                                tbl_ProposalUserGroup proposalUserGroup = new tbl_ProposalUserGroup()
                                                {
                                                    ProposalID = _Proposal.ID,
                                                    GroupID = group.ID,
                                                    UserID = userItem.UserID,


                                                };

                                                tbl_ProposalUserGroup _ProposalUserGroup = cRUDOperation.AddProposalUserGroupControl(proposalUserGroup);
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {

                                    }


                                }



                            }
                        }
                      
                    }

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(false, CustomError.NotExistRecordErrorCode, CustomError.NotExistRecordErrorDesc, "");

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
            ProposalRepository proposalRepository = new ProposalRepository();
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
                    //butun deteail list silinir
                    int nrCount = proposalRepository.SW_DeleteProposalDetail(_Proposal.ID);

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

                    #region Update ProposalUserGroup
                    if (_Proposal.IsPublic)
                    {
                        int nrUSCount = proposalRepository.SW_DeleteProposalUserGroup(_Proposal.ID);
                    }
                    //else
                    //{
                    //    int nrUSCount = proposalRepository.SW_DeleteProposalUserGroup(_Proposal.ID);

                    //    foreach (ProposalUserGroup userGroup in item.ProposalUserGroups)
                    //    {
                    //        tbl_ProposalUserGroup proposalUserGroup = new tbl_ProposalUserGroup()
                    //        {
                    //            ID = userGroup.ID,
                    //            ProposalID = _Proposal.ID,
                    //            GroupID = userGroup.GroupID,


                    //        };
                    //        if (proposalUserGroup.ID != 0)
                    //        {
                    //            tbl_ProposalUserGroup _ProposalUserGroup = cRUDOperation.UpdateProposalUserGroup(proposalUserGroup);
                    //        }
                    //        else
                    //        {
                    //            tbl_ProposalUserGroup _ProposalUserGroup = cRUDOperation.AddProposalUserGroup(proposalUserGroup);
                    //        }


                    //    }
                    //}
                    #endregion



                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


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

                        tbl_ProposalDetail dbitem = cRUDOperation.DeleteProposalDetail(item.ID, 0);

                    }

                    List<tbl_ProposalDocument> tbl_ProposalDocuments = cRUDOperation.GetProposalDocumentsByProposalID(_Proposal.ID);

                    foreach (var item in tbl_ProposalDocuments)
                    {

                        tbl_ProposalDocument dbitem = cRUDOperation.DeleteProposalDocument(item.ID, 0);

                    }

                    List<tbl_ProposalLikeDislike> tbl_ProposalLikeDislikes = cRUDOperation.GetProposalLikeDislikeByPropsalId(_Proposal.ID);

                    foreach (var item in tbl_ProposalLikeDislikes)
                    {

                        tbl_ProposalLikeDislike dbitem = cRUDOperation.DeleteProposalLikeDislike(item.ID, 0);

                    }

                    List<tbl_ProposalFavorite> tbl_ProposalFavorites = cRUDOperation.GetProposalFavoriteByPropsalId(_Proposal.ID);

                    foreach (var item in tbl_ProposalFavorites)
                    {

                        tbl_ProposalFavorite dbitem = cRUDOperation.DeleteProposalFavorite(item.ID, 0);

                    }

                    List<tbl_ProposalUserState> tbl_ProposalUserStates = cRUDOperation.GetProposalUserStatesByProposalID(_Proposal.ID);

                    foreach (var item in tbl_ProposalUserStates)
                    {

                        tbl_ProposalUserState dbitem = cRUDOperation.DeleteProposalUserState(item.ID, 0);

                    }
                    List<tbl_ProposalUserGroup> tblproposalUserGroups = cRUDOperation.GetProposalUserGroupsByProposalID(_Proposal.ID);

                    foreach (tbl_ProposalUserGroup userGroup in tblproposalUserGroups)
                    {


                        tbl_ProposalUserGroup dbitem = cRUDOperation.DeleteProposalUserGroup(userGroup.ID, 0);

                    }

                    tbl_Proposal tbl_Proposal = cRUDOperation.DeleteProposal(id, 0);
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

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

        #region ProposalUserState
        public BaseOutput GetProposalUserStates(out List<tbl_ProposalUserState> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var listItem = operation.GetProposalUserStates();

                itemsOut = listItem;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalUserStateByID(Int64 id, out tbl_ProposalUserState itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var item = operation.GetProposalUserStateByID(id); ;

                itemOut = item;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddProposalUserState(tbl_ProposalUserState item, out tbl_ProposalUserState itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                if (item != null)
                {
                    tbl_ProposalUserState itemDB = operation.AddProposalUserState(item);
                    itemOut = itemDB;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateProposalUserState(tbl_ProposalUserState item, out tbl_ProposalUserState itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                if (item != null)
                {
                    tbl_ProposalUserState itemDB = operation.UpdateProposalUserState(item);
                    itemOut = itemDB;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteProposalUserState(Int64 id, out tbl_ProposalUserState itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_ProposalUserState itemDB = operation.DeleteProposalUserState(id, 0);
                itemOut = itemDB;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalUserStatesByProposalID(Int64 proposalID, out List<tbl_ProposalUserState> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var proposaluserstates = operation.GetProposalUserStatesByProposalID(proposalID);
                itemsOut = proposaluserstates;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalUserStatesByUserID(Int64 userID, out List<tbl_ProposalUserState> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var proposaluserstates = operation.GetProposalUserStatesByUserID(userID);
                itemsOut = proposaluserstates;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalUserStatesByProviderStateType(Int64 providerStateType, out List<tbl_ProposalUserState> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var proposaluserstates = operation.GetProposalUserStatesByProviderStateType(providerStateType);
                itemsOut = proposaluserstates;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalUserStatesByUserStateType(Int64 userStateType, out List<tbl_ProposalUserState> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var proposaluserstates = operation.GetProposalUserStatesByUserStateType(userStateType);
                itemsOut = proposaluserstates;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion

        #region tbl_ProposalUserGroup
        public BaseOutput GetProposalUserGroups(out List<tbl_ProposalUserGroup> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var listItem = operation.GetProposalUserGroups();

                itemsOut = listItem;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalsByGroupID(Int64 groupid, out List<tbl_Proposal> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var listitem = operation.GetProposalsByGroupID(groupid); ;

                itemsOut = listitem;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetGroupsByPropsalID(Int64 propsalid, out List<tbl_Group> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var listitem = operation.GetGroupsByPropsalID(propsalid); ;

                itemsOut = listitem;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddProposalUserGroup(tbl_ProposalUserGroup item, out tbl_ProposalUserGroup itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                if (item != null)
                {
                    tbl_ProposalUserGroup itemDB = operation.AddProposalUserGroup(item);
                    itemOut = itemDB;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateProposalUserGroup(tbl_ProposalUserGroup item, out tbl_ProposalUserGroup itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                if (item != null)
                {

                    tbl_ProposalUserGroup itemDB = operation.UpdateProposalUserGroup(item);
                    itemOut = itemDB;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateProposalUserGroupList(ProposalUserGroupModel item, out ProposalUserGroupModel itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            List<Int64> groupIDs = new List<long>();
            try
            {

                if (item != null)
                {

                    ProposalRepository proposalRepository = new ProposalRepository();
                    CRUDOperation cRUDOperation = new CRUDOperation();
                    int nrUSCount = proposalRepository.SW_DeleteProposalUserGroup(item.ProposalID);

                    foreach (Int64 groupID in item.GroupIDs)
                    {

                        tbl_Group group = cRUDOperation.GetGroupByID(groupID);

                        GroupBusinessOperation groupBusinessOperation = new GroupBusinessOperation();
                        List<UserDTO> userList = new List<UserDTO>();
                        Int64 pointGroupID = 0;
                        Int64 priceGroupID = 0;

                        if (group != null)
                        {
                            try
                            {
                                if (group.GroupType == 2)
                                {
                                    pointGroupID = group.ID;
                                }
                                else if (group.GroupType == 3)
                                {
                                    priceGroupID = group.ID;
                                }

                                if (pointGroupID > 0 || priceGroupID > 0)
                                {
                                    groupBusinessOperation.GetDynamicGroupUsersByGroupID(pointGroupID, priceGroupID, out userList);
                                    foreach (var userItem in userList)
                                    {
                                        tbl_ProposalUserGroup proposalUserGroup = new tbl_ProposalUserGroup()
                                        {
                                            ProposalID = item.ProposalID,
                                            GroupID = group.ID,
                                            UserID = userItem.UserID,


                                        };

                                        tbl_ProposalUserGroup _ProposalUserGroup = cRUDOperation.AddProposalUserGroup(proposalUserGroup);
                                        if (_ProposalUserGroup != null)
                                        {
                                            if (!groupIDs.Contains(_ProposalUserGroup.GroupID))
                                            {
                                                groupIDs.Add(_ProposalUserGroup.GroupID);
                                            }

                                        }
                                    }
                                }

                            }
                            catch (Exception)
                            {

                            }


                        }


                    }

                    if (groupIDs.Count == 0)
                    {
                        return baseOutput = new BaseOutput(true, CustomError.GroupLimitErrorCode, CustomError.GroupLimitErrorDesc, "");
                    }
                    else
                    {
                        itemOut = new ProposalUserGroupModel()
                        {
                            ProposalID = item.ProposalID,
                            GroupIDs = groupIDs
                        };
                        return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                    }


                }
                else
                {
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteProposalUserGroup(Int64 id, out tbl_ProposalUserGroup itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_ProposalUserGroup itemDB = operation.DeleteProposalUserGroup(id, 0);
                itemOut = itemDB;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion

        #region tbl_ProposalFavorite
        public BaseOutput GetProposalFavoriteCountByPropsalId(Int64 proposalID, out Int64 value)
        {
            BaseOutput baseOutput;
            value = 0;
            try
            {
                value = operation.GetProposalFavoriteCountByPropsalId(proposalID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalFavoriteCountByUserId(Int64 userID, out Int64 value)
        {
            BaseOutput baseOutput;
            value = 0;
            try
            {
                value = operation.GetProposalFavoriteCountByUserID(userID);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalFavoriteByPropsalIdAndUserID(Int64 proposalID, Int64 userID, out tbl_ProposalFavorite itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var item = operation.GetProposalFavoriteByPropsalIdAndUserID(proposalID, userID);

                if (item != null)
                {
                    itemOut = item;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }



            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddProposalFavorite(tbl_ProposalFavorite item, out tbl_ProposalFavorite itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                tbl_ProposalFavorite dbItem = cRUDOperation.GetProposalFavoriteByPropsalIdAndUserID(item.ProposalID, item.UserID);

                if (dbItem == null)
                {
                    tbl_ProposalFavorite additem = cRUDOperation.AddProposalFavorite(item);
                    itemOut = additem;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "Uğurla əlavə edilmişdir.");
                }
                else
                {
                    dbItem.IsFavorite = item.IsFavorite;
                    tbl_ProposalFavorite additem = cRUDOperation.UpdateProposalFavorite(dbItem);
                    itemOut = additem;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "Uğurla dəyişiklik edilmişdir.");
                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteProposalFavorite(Int64 id, out tbl_ProposalFavorite itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_ProposalFavorite itemDB = operation.DeleteProposalFavorite(id, 0);
                itemOut = itemDB;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
        #region tbl_ProposalLikeDislike
        public BaseOutput GetProposalLikeCountByProposalID(Int64 proposalID, out int value)
        {
            BaseOutput baseOutput;
            value = 0;
            try
            {
                int diclikeCount = 0;
                value = operation.GetProposalLikeDislikeCountByProposalId(proposalID, out diclikeCount);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalDislikeCountByProposalID(Int64 proposalID, out int value)
        {
            BaseOutput baseOutput;
            value = 0;
            try
            {
                var items = operation.GetProposalLikeDislikeCountByProposalId(proposalID, out value);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalLikeDislikeByPropsalIdAndUserID(Int64 proposalID, Int64 userID, out tbl_ProposalLikeDislike itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var item = operation.GetProposalLikeDislikeByPropsalIdAndUserID(proposalID, userID);

                if (item != null)
                {
                    itemOut = item;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }



            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddProposalLikeDislike(tbl_ProposalLikeDislike item, out tbl_ProposalLikeDislike itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                tbl_ProposalLikeDislike dbItem = cRUDOperation.GetProposalLikeDislikeByPropsalIdAndUserID(item.ProposalID, item.UserID);

                if (dbItem == null)
                {
                    tbl_ProposalLikeDislike additem = cRUDOperation.AddProposalLikeDislike(item);
                    itemOut = additem;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "Uğurla əlavə edilmişdir.");
                }
                else
                {
                    if (item.IsLike.HasValue)
                    {
                        dbItem.IsLike = item.IsLike;
                    }
                    if (item.IsDislike.HasValue)
                    {
                        dbItem.IsDislike = item.IsDislike;
                    }

                    tbl_ProposalLikeDislike additem = cRUDOperation.UpdateProposalLikeDislike(dbItem);
                    itemOut = additem;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "Uğurla dəyişiklik edilmişdir.");
                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteProposalLikeDislike(Int64 id, out tbl_ProposalLikeDislike itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_ProposalLikeDislike itemDB = operation.DeleteProposalLikeDislike(id, 0);
                itemOut = itemDB;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion

        #region tbl_ProposalCommission
        public BaseOutput GetProposalCommissions(out List<tbl_ProposalCommission> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var listItem = operation.GetProposalCommissions();
                itemsOut = listItem;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalCommissionByID(Int64 id, out tbl_ProposalCommission itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var listitem = operation.GetProposalCommissionById(id);

                itemOut = listitem;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalCommissionByProposalID(Int64 proposalID, out tbl_ProposalCommission itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var listitem = operation.GetProposalCommissionByProposalId(proposalID);

                itemOut = listitem;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProposalCommissionsByProviderID(Int64 providerID, out List<tbl_ProposalCommission> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var listitem = operation.GetProposalCommissionByProviderId(providerID);

                itemsOut = listitem;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddProposalCommission(tbl_ProposalCommission item, out tbl_ProposalCommission itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                if (item != null)
                {
                    tbl_ProposalCommission itemDB = operation.AddProposalCommission(item);
                    itemOut = itemDB;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateProposalCommission(tbl_ProposalCommission item, out tbl_ProposalCommission itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                if (item != null)
                {
                    tbl_ProposalCommission itemDB = operation.UpdateProposalCommission(item);
                    itemOut = itemDB;
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }

        public BaseOutput DeleteProposalCommission(Int64 id, out tbl_ProposalCommission itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_ProposalCommission itemDB = operation.DeleteProposalCommission(id, 0);
                itemOut = itemDB;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
    }
}
