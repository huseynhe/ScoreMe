using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.Business.Util
{
    public class UserUtil
    {
        public static string SHA1HashedPassword(string password)
        {

            SHA1 sha = new SHA1CryptoServiceProvider();
            string encryptedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return encryptedPassword;
        }
        public static string MD5HashedPassword(string password)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            string encryptedPassword = Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return encryptedPassword;
        }
    }
}
