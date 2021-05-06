using ScoreMe.Business.Util;
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
    public class ProviderBusinessOperation
    {
        #region Provider

        public BaseOutput GetProviders(out List<tbl_Provider> itemsOut)
        {
            CRUDOperation operation = new CRUDOperation();
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var providers = operation.GetProviders();
                itemsOut = providers;

                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProviderByID(Int64 id, out tbl_Provider itemOut)
        {
            BaseOutput baseOutput;
            CRUDOperation operation = new CRUDOperation();
            itemOut = null;
            try
            {
                var provider = operation.GetProviderById(id); 
                itemOut = provider;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
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
                                ID = providerDB.ID,
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
                    return baseOutput = new BaseOutput(true, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, "");

                }



            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteProvider(Int64 providerID,out tbl_Provider itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                tbl_Provider _propvider = cRUDOperation.GetProviderById(providerID);

                if (_propvider != null)
                {
                    List<tbl_Proposal> _Proposals = cRUDOperation.GetProposalsByProviderID(_propvider.ID);

                    if (_Proposals.Count > 0)
                    {
                    
                        return baseOutput = new BaseOutput(false, CustomError.ProposalRecordExistErrorCode, CustomError.ProposalRecordExistErrorDesc, "Bu provayder`a bağlı xidmətlər mövcuddur!.");

                    }
                    else
                    {
                        tbl_User _user = cRUDOperation.DeleteUser(_propvider.UserId, _propvider.UserId);
                        if (_user == null)
                        {
                            return baseOutput = new BaseOutput(false, CustomError.NotExistRecordErrorCode, CustomError.NotExistRecordErrorDesc, "");
                        }
                        tbl_Provider _ProviderDB = cRUDOperation.DeleteProvider(_propvider.ID, _user.ID);
                        itemOut = _ProviderDB;
                        return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                    }


                }
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

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
                itemOut = provider;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProviderByUserName(string username, out Provider itemOut)
        {
            ProviderRepository providerRepository = new ProviderRepository();
            BaseOutput baseOutput;
            try
            {
                CRUDOperation operation = new CRUDOperation();
                var tblprovider = operation.GetProviderByUserName(username);
                Provider provider = new Provider()
                {
                    ID = tblprovider.ID,
                    UserID = tblprovider.UserId,
                    UserName = username,
                    Name = tblprovider.Name,
                    ParentID = tblprovider.ParentID,
                    Type = tblprovider.Type == null ? 0 : (Int64)tblprovider.Type,
                    Description = tblprovider.Description,
                    RegionId = tblprovider.RegionId == null ? 0 : (Int64)tblprovider.RegionId,
                    Address = tblprovider.Address,
                    RelatedPersonName = tblprovider.RelatedPersonName,
                    RelatedPersonProfession = tblprovider.RelatedPersonProfession,
                    RelatedPersonPhone = tblprovider.RelatedPersonPhone,
                    RP_HomePhone = tblprovider.RP_HomePhone,
                    VOEN = tblprovider.VOEN,
                    LogoLinkPath = tblprovider.LogoLinkPath,
                    LogoLinkName = tblprovider.LogoLinkName,

                };

                itemOut = provider;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProviderReportsByDatePeriod(Search search, out ProviderReportDTO itemOut)
        {
            ProviderRepository providerRepository = new ProviderRepository();
            BaseOutput baseOutput;
            try
            {
                ProviderRepository repository = new ProviderRepository();
                var providerReportDTO = repository.SW_GetProviderReportsByDatePeriod(search);
                itemOut = providerReportDTO;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetProviderReportsByYearAndMonths(Search search, out ProviderReportDTO itemOut)
        {
            ProviderRepository providerRepository = new ProviderRepository();
            BaseOutput baseOutput;
            try
            {
                ProviderRepository repository = new ProviderRepository();
                var providerReportDTO = repository.SW_GetProviderReportsByYearAndMonths(search);
                itemOut = providerReportDTO;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
    }
}
