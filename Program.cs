using CommandLine;
using SYN001.Factories;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SYN001
{
    partial class Program
    {
        static StandardBusinessDocument document;

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                var factory = new StandardBusinessDocumentFactory();
                factory.InstitutionRepositoryFactory.AccessPointCount = o.AccessPointCount;
                factory.InstitutionRepositoryFactory.InstitutionsPerAccessPoint = o.InstitutionsPerAccessPoint;
                factory.InstitutionRepositoryFactory.InstitutionFactory.CompetencesPerInstitution = o.CompetencesPerInstitution;
                document = factory.Create();

                document.StandardBusinessDocumentHeader.Sender.Identifier.Value = o.SenderId;
                document.StandardBusinessDocumentHeader.Receiver.Identifier.Value = o.ReceiverId;
                document.SYN001.IRSync.version = o.IRVersion;
                document.SYN001.IRSync.InstitutionRepository.CentralServicesNode.officialID = o.CsnId;
                document.SYN001.IRSync.InstitutionRepository.AccessPoints[0].officialID = o.ApId;
                document.SYN001.IRSync.InstitutionRepository.AccessPoints[0].LinkedInstitutions[0].Institution.officialID = o.InstId;
                document.SYN001.IRSync.InstitutionRepository.Institutions[0].officialID = o.InstId;
                document.SYN001.IRSync.InstitutionRepository.Institutions[0].BusinessMessageEndpoint.channel = o.InstUrl;
                document.SYN001.IRSync.InstitutionRepository.Institutions[0].SystemMessageEndpoint.channel = o.InstUrl;
                document.SYN001.IRSync.InstitutionRepository.Institutions[0].BusinessMessageEndpoint.messageExchangePattern = o.InstPattern;
                document.SYN001.IRSync.InstitutionRepository.Institutions[0].SystemMessageEndpoint.messageExchangePattern = o.InstPattern;

                if (o.CsnEbmsCertPath != null)
                {
                    Replace(document.SYN001.IRSync.InstitutionRepository.CentralServicesNode.EbMSSignatureCertificates[0].CertificateIdentification, o.CsnEbmsCertPath);
                }

                if (o.CsnTlsCertPath != null)
                {
                    Replace(document.SYN001.IRSync.InstitutionRepository.CentralServicesNode.TLSCertificates[0].CertificateIdentification, o.CsnTlsCertPath);                    
                }

                if (o.ApEbmsCertPath != null)
                {
                    Replace(document.SYN001.IRSync.InstitutionRepository.AccessPoints[0].EbMSSignatureCertificates[0].CertificateIdentification, o.ApEbmsCertPath);
                }

                if (o.ApIntTlsCertPath != null)
                {
                    Replace(document.SYN001.IRSync.InstitutionRepository.AccessPoints[0].InternalTLSCertificates[0].CertificateIdentification, o.ApIntTlsCertPath);
                }

                if (o.ApExtTlsCertPath != null)
                {
                    Replace(document.SYN001.IRSync.InstitutionRepository.AccessPoints[0].ExternalTLSCertificates[0].CertificateIdentification, o.ApExtTlsCertPath);
                }

                if (o.InstEbmsCertPath != null)
                {
                    Replace(document.SYN001.IRSync.InstitutionRepository.Institutions[0].EbMSSignatureCertificates[0].CertificateIdentification, o.InstEbmsCertPath);
                }
                
                if (o.InstBusinessCertPath != null)
                {
                    Replace(document.SYN001.IRSync.InstitutionRepository.Institutions[0].BusinessSignatureCertificates[0].CertificateIdentification, o.InstBusinessCertPath);
                }

                var filename = $"SYN001-{DateTime.Now.ToString("HH-mm-ss")}";
                Serializer.Serialize(document, File.Create(filename));
            });
        }

        private static void Replace(CertificateIdentification reference, string pfxPath)
        {   
            var certificate = document.SYN001.IRSync.InstitutionRepository.Certificates.Find(c => c.thumbprint == reference.thumbprint);
            var thumbprint = new X509Certificate2(pfxPath).Thumbprint;
            certificate.thumbprint = thumbprint;
            reference.thumbprint = thumbprint;
        }
    }
}
