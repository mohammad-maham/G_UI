using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace G_APIs.Common
{


    public static class Common
    {

        public static string GetHash(string text)
        {
            return SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(text))
                .Aggregate("", (x, y) => x + y);

        }

        public static string Base64Encode(string txt)
        {
            var txtBytes = System.Text.Encoding.UTF8.GetBytes(txt);
            return System.Convert.ToBase64String(txtBytes);
        }


    }
}

