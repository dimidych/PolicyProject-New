using System;
using CryptoToolLib;

namespace LoginService
{
    internal static class CertificateWorker
    {
        public static string CreateCertificate()
        {
            var cryptoWorker = new CryptoWorker(CryptoSystemType.RSA, string.Empty, false);
            var bytes = cryptoWorker.ExportKeyBlob(true);
            return Convert.ToBase64String(bytes);
        }
    }
}