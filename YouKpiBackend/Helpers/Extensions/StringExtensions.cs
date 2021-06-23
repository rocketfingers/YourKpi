using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YouKpiBackend.Helpers.Extensions
{
    public static class StringExtensions
    {
        /// <summary>Remove new lines in string and trim
        /// </summary>
        public static string RmNlTrim(this String oldString)
        {
            var newString = oldString.Replace("\n", "");
            newString = newString.Trim();

            return newString;
        }
    
    public static string HashSha256(this string str)
        {
            using (SHA256 algorithm = SHA256.Create())
            {
                return GetHash(algorithm, str);
            }
        }
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
