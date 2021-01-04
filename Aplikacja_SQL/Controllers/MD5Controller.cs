using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Aplikacja_SQL.Controllers
{
    public class MD5Controller : Controller
    {
        //Method Crypto
        public static string ToMD5Hash(string word)
        {
            if(word == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] md5HashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(word));

                foreach (byte b in md5HashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
            }
            return sb.ToString();
        }
    }
}