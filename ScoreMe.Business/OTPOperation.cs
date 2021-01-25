using OtpNet;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Enum;
using ScoreMe.DAL.ErrorManagment;
using ScoreMe.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.Business
{
    public class OTPOperation
    {

        public BaseOutput GenarateOTP(string userName, out string itemOut)
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.EmptyUserNameErrorCode, CustomError.EmptyUserNameErrorDesc, "");

                }


                tbl_User user = cRUDOperation.GetUserByUserName(userName);

                if (user == null)
                {
                    itemOut = null;
                    return baseOutput = new BaseOutput(true, CustomError.UserNameNotFoundCode, CustomError.UserNameNotFoundDesc, "");

                }
                else
                {
                    tbl_Customer customer = cRUDOperation.GetCustomerByUserId(user.ID);

                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(userName);
                    var window = new VerificationWindow(previous: 1, future: 1);
                    var totp = new Totp(bytes, step: 300);
                    var result = totp.ComputeTotp(DateTime.UtcNow);
                    poctgoyerciniSRV.smsservice srv = new poctgoyerciniSRV.smsservice();
                    List<string> lists = new List<string>();
                    string[] numbers = new string[1];
                    numbers[0] = customer.PhoneNumber;
                    string[] resultArray = new string[1];
                    resultArray = srv.SmsInsert_1_N(WebServiceUtil.SMSUserName, WebServiceUtil.SMSPassword, DateTime.Now, null, numbers, result);

                    if (!string.IsNullOrEmpty(resultArray[0]))
                    {
                        tbl_OTP _OTP = new tbl_OTP()
                        {
                            UserID = user.ID,
                            CreateTime = DateTime.Now,
                            OTPCode = result,
                            ISsuccess = 0,

                        };

                        tbl_OTP oTP = cRUDOperation.AddOTP(_OTP);
                        itemOut = _OTP.OTPCode;
                        return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                    }
                    else
                    {
                        itemOut = null;
                        return baseOutput = new BaseOutput(true, CustomError.OTPCodeNotSendSMSServiceCode, CustomError.OTPCodeNotSendSMSServiceDesc, "");

                    }



                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public BaseOutput VerifyOTP(string userName, string otptext, out bool verify)
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            BaseOutput baseOutput;
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    verify = false;
                    return baseOutput = new BaseOutput(true, CustomError.EmptyUserNameErrorCode, CustomError.EmptyUserNameErrorDesc, "");

                }
                else if (string.IsNullOrEmpty(otptext))
                {
                    verify = false;
                    return baseOutput = new BaseOutput(true, CustomError.EmptyOTPCodeErrorCode, CustomError.EmptyOTPCodeErrorDesc, "");

                }

                else
                {
                    tbl_User user = cRUDOperation.GetUserByUserName(userName);

                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(userName);
                    var totp = new Totp(bytes, step: 300);
                    var input = otptext;
                    long timeStepMatched;
                    verify = totp.VerifyTotp(input, out timeStepMatched, window: null);

                    tbl_OTP OTPObj = cRUDOperation.GetOTPByOtpCode(otptext);
                    if (verify)
                    {

                        OTPObj.ISsuccess = 1;
                        tbl_OTP OTPupdate = cRUDOperation.UpdateOTP(OTPObj);
                    }
                    else
                    {
                        OTPObj.ISsuccess = 2;
                        tbl_OTP OTPupdate = cRUDOperation.UpdateOTP(OTPObj);
                    }

                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }


            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
