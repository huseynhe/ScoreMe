using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScoreMe.UTILITY
{
    public class NumberHelper
    {
        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        private static string GetNumberPrefix(string userName)
        {

            string userFullName = RemoveSpecialCharacters(userName);
            int length = userFullName.Length;
            string prefix = string.Empty;
            switch (length)
            {
                case 2:
                    prefix = userFullName.Substring(0, 2);
                    break;
                case 3:
                    prefix = userFullName.Substring(1, 2);
                    break;
                case 5:
                    prefix = userFullName.Substring(3, 2);
                    break;
                case 9:
                    prefix = userFullName.Substring(0, 2);
                    break;
                case 10:
                    prefix = userFullName.Substring(1, 2);
                    break;
                case 12:
                    prefix = userFullName.Substring(3, 2);
                    break;
                case 13:
                    prefix = userFullName.Substring(4, 2);
                    break;
                default:
                    prefix = string.Empty;
                    break;
            }


            return prefix;
        }

        public static bool ControlSameOrOther(string userName,string prefix)
        {

            string userPrefix = GetNumberPrefix(userName);
            string numberPrefix = GetNumberPrefix(prefix);

            if (userPrefix==numberPrefix)
            {
                return true;
            }
            if (userPrefix=="50" &&numberPrefix=="51")
            {
                return true;
            }
            if (userPrefix == "51" && numberPrefix == "50")
            {
                return true;
            }
            if (userPrefix == "55" && numberPrefix == "99")
            {
                return true;
            }
            if (userPrefix == "99" && numberPrefix == "55")
            {
                return true;
            }
            if (userPrefix == "70" && numberPrefix == "77")
            {
                return true;
            }
            if (userPrefix == "77" && numberPrefix == "70")
            {
                return true;
            }
            return false;
        }
    }
}
