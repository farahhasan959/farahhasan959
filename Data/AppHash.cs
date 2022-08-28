using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RightProject.Data
{
    public static class AppHash
    {
        public static string HashPassword(string input)
        {
            string ps = string.Empty;
            MD5 hash = MD5.Create();
            byte[] data = hash.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));

            }
            ps = builder.ToString();
            return ps;

        }
    }
}
