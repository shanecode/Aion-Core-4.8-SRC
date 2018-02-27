using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASM31
{
    class Utils
    {
        public static String encodePassword(String password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            string pwdhash = System.Convert.ToBase64String(hashBytes);
            return pwdhash;
        }
    }
}
