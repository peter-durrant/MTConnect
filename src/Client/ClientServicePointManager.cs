using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Hdd.MTConnect.Client
{
    public sealed class ClientServicePointManager : IDisposable
    {
        public ClientServicePointManager(SecurityProtocolType securityProtocolType)
        {
            ServicePointManager.ServerCertificateValidationCallback += AcceptAllCertifications;
            ServicePointManager.SecurityProtocol = securityProtocolType;
        }

        public void Dispose()
        {
            ServicePointManager.ServerCertificateValidationCallback -= AcceptAllCertifications;
        }

        private static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return sslPolicyErrors == SslPolicyErrors.None;
        }
    }
}