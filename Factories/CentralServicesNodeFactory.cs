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
                EbMSSignatureCertificates = new List<EbMSSignatureCertificate>
                {
                    new EbMSSignatureCertificate
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
                TLSCertificates = new List<TLSCertificate>
                {
                    new TLSCertificate
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
