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
using ScoreMe.UTILITY;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.DTO;

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
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

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
                    return baseOutput = new BaseOutput(false, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");
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

                tbl_CALLModel _CALLModel = cRUDOperation.GetCALLModelByID(id);

                if (_CALLModel != null)
                {
                    List<tbl_CALLDetail> tbl_CALLDetails = cRUDOperation.GetCALLDetailsByModelID(_CALLModel.ID);

                    foreach (var item in tbl_CALLDetails)
                    {

                        tbl_CALLDetail tbl_CALLDetail = cRUDOperation.DeleteCALLDetail(item.ID, 0);

                    }



                    tbl_CALLModel tbl_CALLModel = cRUDOperation.DeleteCALLModel(id, 0);
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

       
        #region NetConsumeModel
        public BaseOutput GetNetConsumeModels(out List<NetConsumeModel> netConsumeModels)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            netConsumeModels = null;
            try
            {

                List<tbl_NetConsumeModel> tblNetConsumeModels = cRUDOperation.GetNetConsumeModels();
                netConsumeModels = new List<NetConsumeModel>();

                if (tblNetConsumeModels.Count > 0)
                {
                    foreach (var item in tblNetConsumeModels)
                    {
                        NetConsumeModel consumeModel = new NetConsumeModel()
                        {
                            ID = item.ID,
                            UserID = item.UserID,
                            BeginDate = item.BeginDate,
                            EndDate = item.EndDate
                        };

                        List<tbl_NetConsumeDetail> tbl_netConsumeDetails = cRUDOperation.GetNetConsumeDetailsByModelID(consumeModel.ID);

                        consumeModel.NetConsumeDetails = tbl_netConsumeDetails;
                        netConsumeModels.Add(consumeModel);

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
        public BaseOutput GetNetConsumeModelByID(Int64 id, out NetConsumeModel netConsumeModel)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            netConsumeModel = null;
            try
            {

                tbl_NetConsumeModel item = cRUDOperation.GetNetConsumeModelByID(id);
                if (item != null)
                {
                    netConsumeModel = new NetConsumeModel()
                    {
                        ID = item.ID,
                        UserID = item.UserID,
                        BeginDate = item.BeginDate,
                        EndDate = item.EndDate
                    };

                    List<tbl_NetConsumeDetail> tbl_NetConsumeDetails = cRUDOperation.GetNetConsumeDetailsByModelID(netConsumeModel.ID);
                    netConsumeModel.NetConsumeDetails = tbl_NetConsumeDetails;
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
        public BaseOutput GetLastNetConsumeModelByUserName(string userName, out NetConsumeModel netConsumeModel)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            netConsumeModel = null;
            try
            {

                tbl_NetConsumeModel item = cRUDOperation.GetLastNetConsumeModelByUserName(userName);
                if (item != null)
                {
                    netConsumeModel = new NetConsumeModel()
                    {
                        ID = item.ID,
                        UserID = item.UserID,
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
        public BaseOutput AddNetConsumeModel(NetConsumeModel item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;


            try
            {
                tbl_NetConsumeModel netConsumeModel = new tbl_NetConsumeModel()
                {
                    UserID = item.UserID,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                };

                List<tbl_NetConsumeDetail> tblNetConsumeDetails = new List<tbl_NetConsumeDetail>();

                tblNetConsumeDetails = item.NetConsumeDetails;
                tbl_NetConsumeModel _netConsumeModel = cRUDOperation.AddNetConsumeModel(netConsumeModel, tblNetConsumeDetails);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateNetConsumeModel(NetConsumeModel item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                tbl_NetConsumeModel tblNetConsumeModel = new tbl_NetConsumeModel()
                {
                    ID = item.ID,
                    UserID = item.UserID,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                };

                tbl_NetConsumeModel _NetConsumeModel = cRUDOperation.UpdateNetConsumeModel(tblNetConsumeModel);

                if (_NetConsumeModel != null)
                {
                    foreach (var consumeDetail in item.NetConsumeDetails)
                    {
                        tbl_NetConsumeDetail tblNetConsumeDetail = cRUDOperation.UpdateNetConsumeDetail(consumeDetail);
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
        public BaseOutput DeleteNetConsumeModel(Int64 id)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {

                tbl_NetConsumeModel _NetConsumeModel = cRUDOperation.GetNetConsumeModelByID(id);

                if (_NetConsumeModel != null)
                {
                    List<tbl_NetConsumeDetail> netConsumeDetails = cRUDOperation.GetNetConsumeDetailsByModelID(_NetConsumeModel.ID);

                    foreach (var item in netConsumeDetails)
                    {

                        tbl_NetConsumeDetail tbl_NetConsumeDeatilDB = cRUDOperation.DeleteNetConsumeDetail(item.ID, 0);

                    }



                    tbl_NetConsumeModel netConsumeModel = cRUDOperation.DeleteNetConsumeModel(id, 0);
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

        #region AppConsumeModel
        public BaseOutput GetAppConsumeModels(out List<AppConsumeModel> appConsumeModels)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            appConsumeModels = null;
            try
            {

                List<tbl_AppConsumeModel> tblAppConsumeModels = cRUDOperation.GetAppConsumeModels();
                appConsumeModels = new List<AppConsumeModel>();

                if (tblAppConsumeModels.Count > 0)
                {
                    foreach (var item in tblAppConsumeModels)
                    {
                        AppConsumeModel consumeModel = new AppConsumeModel()
                        {
                            ID = item.ID,
                            UserID = item.UserID,
                            BeginDate = item.BeginDate,
                            EndDate = item.EndDate
                        };

                        List<tbl_AppConsumeDetail> tbl_appConsumeDetails = cRUDOperation.GetAppConsumeDetailsByModelID(consumeModel.ID);

                        consumeModel.AppConsumeDetails = tbl_appConsumeDetails;
                        appConsumeModels.Add(consumeModel);

                    }


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
        public BaseOutput GetAppConsumeModelByID(Int64 id, out AppConsumeModel appConsumeModel)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            appConsumeModel = null;
            try
            {

                tbl_AppConsumeModel item = cRUDOperation.GetAppConsumeModelByID(id);
                if (item != null)
                {
                    appConsumeModel = new AppConsumeModel()
                    {
                        ID = item.ID,
                        UserID = item.UserID,
                        BeginDate = item.BeginDate,
                        EndDate = item.EndDate
                    };

                    List<tbl_AppConsumeDetail> tbl_AppConsumeDetails = cRUDOperation.GetAppConsumeDetailsByModelID(appConsumeModel.ID);
                    appConsumeModel.AppConsumeDetails = tbl_AppConsumeDetails;
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
        public BaseOutput GetLastAppConsumeModelByUserName(string userName, out AppConsumeModel appConsumeModel)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            appConsumeModel = null;
            try
            {

                tbl_AppConsumeModel item = cRUDOperation.GetLastAppConsumeModelByUserName(userName);
                if (item != null)
                {
                    appConsumeModel = new AppConsumeModel()
                    {
                        ID = item.ID,
                        UserID = item.UserID,
                        BeginDate = item.BeginDate,
                        EndDate = item.EndDate
                    };

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
        public BaseOutput AddAppConsumeModel(AppConsumeModel item, out AppConsumeModel itemOut)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                tbl_AppConsumeModel appConsumeModel = new tbl_AppConsumeModel()
                {
                    UserID = item.UserID,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                };

                List<tbl_AppConsumeDetail> tblAppConsumeDetails = new List<tbl_AppConsumeDetail>();

                tblAppConsumeDetails = item.AppConsumeDetails;
                tbl_AppConsumeModel _appConsumeModel = cRUDOperation.AddAppConsumeModel(appConsumeModel, tblAppConsumeDetails);
                item.ID = _appConsumeModel.ID;
                itemOut = item;
                itemOut.AppConsumeDetails = null;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateAppConsumeModel(AppConsumeModel item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
    
            try
            {
                tbl_AppConsumeModel tblAppConsumeModel = new tbl_AppConsumeModel()
                {
                    ID = item.ID,
                    UserID = item.UserID,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                };

                tbl_AppConsumeModel _AppetConsumeModel = cRUDOperation.UpdateAppConsumeModel(tblAppConsumeModel);

                if (_AppetConsumeModel != null)
                {
                    foreach (var consumeDetail in item.AppConsumeDetails)
                    {
                        tbl_AppConsumeDetail tblAppConsumeDetail = cRUDOperation.UpdateAppConsumeDetail(consumeDetail);
                    }

                    
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
        public BaseOutput DeleteAppConsumeModel(Int64 id)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {

                tbl_AppConsumeModel _AppConsumeModel = cRUDOperation.GetAppConsumeModelByID(id);

                if (_AppConsumeModel != null)
                {
                    List<tbl_AppConsumeDetail> appConsumeDetails = cRUDOperation.GetAppConsumeDetailsByModelID(_AppConsumeModel.ID);

                    foreach (var item in appConsumeDetails)
                    {

                        tbl_AppConsumeDetail appConsumeDetail = cRUDOperation.DeleteAppConsumeDetail(item.ID, 0);

                    }

                    tbl_AppConsumeModel appConsumeModel = cRUDOperation.DeleteAppConsumeModel(id, 0);
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
        #endregion


        #region AppConsumeModel
        public BaseOutput GetUserPhoneInformationsByUserName(string userName, out List<tbl_UserPhoneInforamtion> userPhoneInforamtions)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            userPhoneInforamtions = null;
            try
            {


                if (!string.IsNullOrEmpty(userName))
                {
                    userPhoneInforamtions = cRUDOperation.GetUserPhoneInformationsByUserName(userName);

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.EmptyUserNameErrorCode, CustomError.EmptyUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetLastUserPhoneInformationByUserName(string userName, out tbl_UserPhoneInforamtion userPhoneInforamtion)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            userPhoneInforamtion = null;
            try
            {

                if (!string.IsNullOrEmpty(userName))
                {

                    userPhoneInforamtion = cRUDOperation.GetLastUserPhoneInformationByUserName(userName);
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.EmptyUserNameErrorCode, CustomError.EmptyUserNameErrorDesc, "");

                }

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetUserPhoneInformationByID(Int64 id, out tbl_UserPhoneInforamtion userPhoneInforamtion)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            userPhoneInforamtion = null;
            try
            {

                userPhoneInforamtion = cRUDOperation.GetUserPhoneInformationById(id);
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddUserPhoneInformation(tbl_UserPhoneInforamtion item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;


            try
            {
                bool flag = cRUDOperation.ControlUserPhoneInformation(item);
                if (!flag)
                {
                    tbl_UserPhoneInforamtion _UserPhoneInforamtion = cRUDOperation.AddUserPhoneInformation(item);
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");
                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.ExistRecordErrorCode, CustomError.ExistRecordErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateUserPhoneInformation(tbl_UserPhoneInforamtion item)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {

                tbl_UserPhoneInforamtion _UserPhoneInforamtion = cRUDOperation.UpdateUserPhoneInformation(item);

                if (_UserPhoneInforamtion != null)
                {
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.NotExistRecordErrorCode, CustomError.NotExistRecordErrorDesc, "");

                }



            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteUserPhoneInformation(Int64 id)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {

                tbl_UserPhoneInforamtion _UserPhoneInforamtion = cRUDOperation.DeleteUserPhoneInformation(id, 0);

                if (_UserPhoneInforamtion != null)
                {
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }
                else
                {
                    return baseOutput = new BaseOutput(true, CustomError.NotExistRecordErrorCode, CustomError.NotExistRecordErrorDesc, "");

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


