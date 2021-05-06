using ScoreMe.Business.Util;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Enum;
using ScoreMe.DAL.ErrorManagment;
using ScoreMe.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.Business
{
    public class UserBusinessOperation
    {
        CRUDOperation operation = new CRUDOperation();
        public BaseOutput GetUsers(out List<tbl_User> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var users = operation.GetUsers();
                itemsOut = users;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetUserByID(Int64 id, out tbl_User itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var user = operation.GetUserById(id); 
                itemOut = user;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetUserByUserName(string username, out tbl_User itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var user = operation.GetUserByUserName(username); 
                itemOut = user;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddUser(tbl_User item, out tbl_User itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_User user = operation.AddUser(item);
                itemOut = user;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateUser(tbl_User item, out tbl_User itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_User user = operation.UpdateUser(item);
                itemOut = user;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteUser(Int64 id, out tbl_User itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_User user = operation.DeleteUser(id, id);
                itemOut = user;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput ChangeUserActivateStatus(Int64 id, int activateStatus, out tbl_User itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_User user = operation.ActivateUser(id, id, activateStatus);
                itemOut = user;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput ChangePassword(Int64 id, string newpassword, out tbl_User itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_User user = operation.ChangePassword(id, id, newpassword);
                itemOut = user;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
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
    }
}
