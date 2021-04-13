using System.Collections.Generic;

namespace SYN001.Factories
{
    public class CentralServicesNodeFactory
    {
        public CentralServicesNode Create()
        {
            return new CentralServicesNode
            {
                officialID = "EU:CSN01",
                EbmsSignatureCertificates = new List<EbmsSignatureCertificate>
                {
                    new EbmsSignatureCertificate
                    {
                        CertificateIdentification = new CertificateIdentification
                        {
                            thumbprint = ""
                        }
                    }
                },
                SystemMessageEndpoint = new MessageEndpoint
                {
                    channel = "https://csn.eu/",
                    messageExchangePattern = "push"
                },
                TLSCertificates = new List<TlsCertificate>
                {
                    new TlsCertificate
                    {
                        CertificateIdentification = new CertificateIdentification
                        {
                            thumbprint = ""
                        }
                    }
                }
            };
        }
    }
}
