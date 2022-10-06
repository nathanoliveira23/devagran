using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Devagran.Ultils
{
    public class MD5Ultils
    {
        public static string MD5HashGenerator(string text)
        {
            MD5 md5Hash = MD5.Create();
            byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            
            StringBuilder stringBuilder = new StringBuilder();
            
            for (int i = 0; i < bytes.Length; i++)
                stringBuilder.Append(bytes[i]);

            return stringBuilder.ToString();
        }
    }
}