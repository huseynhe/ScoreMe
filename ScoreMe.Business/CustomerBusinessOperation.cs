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
    public class CustomerBusinessOperation
    {
        CRUDOperation operation = new CRUDOperation();

        public BaseOutput GetCustomers(out List<tbl_Customer> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var customers = operation.GetCustomers();

                if (customers != null)
                {
                    itemsOut = customers;
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
        public BaseOutput GetCustomerByID(Int64 id, out tbl_Customer itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var customer = operation.GetCustomerById(id); ;

                if (customer != null)
                {
                    itemOut = customer;
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
        public BaseOutput GetCustomerByUserID(Int64 userid, out tbl_Customer itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var customer = operation.GetCustomerByUserId(userid); ;

                if (customer != null)
                {
                    itemOut = customer;
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
        public BaseOutput GetCustomerByUserName(string username, out tbl_Customer itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var customer = operation.GetCustomerByUserName(username); ;

                if (customer != null)
                {
                    itemOut = customer;
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
        public BaseOutput AddCustomer(tbl_Customer item, out tbl_Customer itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                if (item != null)
                {
                    tbl_Customer customer = operation.AddCustomer(item);
                    itemOut = customer;
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
        public BaseOutput UpdateCustomer(tbl_Customer item, out tbl_Customer itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                if (item != null)
                {
                    tbl_Customer customer = operation.UpdateCustomer(item);
                    itemOut = customer;
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
        public BaseOutput DeleteCustomer(Int64 id, out tbl_Customer itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_Customer customer = operation.DeleteCustomer(id, 0);
                itemOut = customer;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
    }
}
