using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.ErrorManagment
{
    public class CustomError
    {
        public static int UniqueUserNameErrorCode = 11;
        public static string UniqueUserNameErrorDesc = "The username already exists";
        public static int UniqueVOENErrorCode = 12;
        public static string UniqueVOENErrorDesc = "The VOEN already exists";
        public static int UserNameNotFoundCode = 13;
        public static string UserNameNotFoundDesc = "Username not found ";
        public static int PasswordIncorrectCode = 14;
        public static string PasswordIncorrectDesc = "Password is incorrect ";
        public static int PasswordAndConfirmPasswordCode = 15;
        public static string PasswordAndConfirmPasswordDesc = "The password and confirmation password do not match. ";

        public static int EmptyUserNameErrorCode = 16;
        public static string EmptyUserNameErrorDesc = "Username is empty. ";

        public static int EmptyUserPasswordErrorCode = 17;
        public static string EmptyUserPasswordErrorDesc = "Password is empty. ";

        public static int EmptyOTPCodeErrorCode = 18;
        public static string EmptyOTPCodeErrorDesc = "OTP code is empty. ";

        public static int OTPCodeNotSendSMSServiceCode = 19;
        public static string OTPCodeNotSendSMSServiceDesc = "OTP code can not send use service. ";

        public static int PhoneNumberErrorCode = 20;
        public static string PhoneNumberErrorDesc = "PhoneNumber is empty. ";

        public static int ExistRecordErrorCode = 21;
        public static string ExistRecordErrorDesc = "Same record is exist. ";

        public static int NotExistRecordErrorCode = 22;
        public static string NotExistRecordErrorDesc = "This record is not exist. ";

        public static int ProposalRecordExistErrorCode = 23;
        public static string ProposalRecordExistErrorDesc = "This provider has proposals";

        public static int GroupLimitErrorCode = 24;
        public static string GroupLimitErrorDesc = "These groups limit does not contain user";
    }
}
