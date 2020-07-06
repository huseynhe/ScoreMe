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
                string encryptedPassword = UserUtil.HashedPassword(item.Password);
                tbl_User validUser = cRUDOperation.ValidLogin(item.UserName, encryptedPassword);
                if (validUser != null)
                {
                    string encryptedNewPassword = UserUtil.HashedPassword(item.Newpassword);
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
                string encryptedPassword = UserUtil.HashedPassword(userPassword);
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
                    Password = UserUtil.HashedPassword(item.Password),
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

                            UserId = item.UserID,
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
                                UserID = userDB.ID,
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
                    Password = UserUtil.HashedPassword(item.Password),
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
                                UserID = userDB.ID,
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
    }

}
