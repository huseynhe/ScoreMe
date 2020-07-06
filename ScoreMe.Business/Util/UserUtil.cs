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
        public static string HashedPassword(string password)
        {

            SHA1 sha = new SHA1CryptoServiceProvider();
            string encryptedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return encryptedPassword;
        }
    }
}
