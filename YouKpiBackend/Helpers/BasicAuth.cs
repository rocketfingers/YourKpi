using System;

namespace YouKpiBackend.Helpers
{
    public class BasicAuthHelper
    {
        static System.Text.Encoding ISO_8859_1_ENCODING = System.Text.Encoding.GetEncoding("ISO-8859-1");
        public static (string, string) GetUsernameAndPasswordFromAuthorizeHeader(string authorizeHeader)
        {
            string encodedUsernamePassword = authorizeHeader.Substring("Basic ".Length).Trim();
            string usernamePassword = ISO_8859_1_ENCODING.GetString(Convert.FromBase64String(encodedUsernamePassword));

            string username = usernamePassword.Split(':')[0];
            string password = usernamePassword.Split(':')[1];

            return (username, password);

        }
    }
}
   