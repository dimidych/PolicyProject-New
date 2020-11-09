using System.Security.Cryptography;
using System.Text;

namespace LoginService
{
    internal static class Hasher
    {
        internal static string Hash(string securePassword)
        {
            var md5Hasher = MD5.Create();
            var buffer = Encoding.Default.GetBytes(securePassword);
            var data = md5Hasher.ComputeHash(buffer);
            return Bytes2String(data);
        }

        private static string Bytes2String(byte[] data)
        {
            var sBuilder = new StringBuilder();

            foreach (var t in data)
                sBuilder.Append(t.ToString("x2"));

            return sBuilder.ToString();
        }
    }
}