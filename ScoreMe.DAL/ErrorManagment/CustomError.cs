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
    }
}
