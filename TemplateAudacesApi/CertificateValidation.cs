using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TemplateAudacesApi
{
    public class CertificateValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            var cert = new X509Certificate2(Path.Combine("DERMOBAMBOO COSMETICOS LTDA48568460000147"), "1234");
            if (clientCertificate.Thumbprint == cert.Thumbprint)
                return true;

            return false;
        }
    }
}
