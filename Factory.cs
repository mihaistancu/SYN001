using SYN001.Fakers;
using System;

namespace SYN001
{
    public class Factory
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Version { get; set; }
        public int InstitutionsPerAccessPoint { get; set; }
        public int AccessPointCount { get; set; }
        
        public StandardBusinessDocument Create()
        {
            var doc = GetStandardBusinessDocument();
            LinkCertificates(doc);
            LinkInstitutions(doc);
            return doc;
        }

        private StandardBusinessDocument GetStandardBusinessDocument()
        {
            return new StandardBusinessDocument
            {
                schemaLocation = GetSchemaLocation(),
                StandardBusinessDocumentHeader = GetHeader(),
                SYN001 = GetSed()
            };
        }

        private string GetSchemaLocation()
        {
            return "http://eessi.dgempl.ec.europa.eu/namespaces/sbdh SYNC_SUC_01-4.2-DataSource-DataDestination-Start-SYN001-4.2.xsd";
        }

        private StandardBusinessDocumentHeader GetHeader()
        {
            return new StandardBusinessDocumentHeader
            {
                HeaderVersion = "1.1",
                Sender = new Participant
                {
                    ContactTypeIdentifier = "DataSource",
                    Identifier = new Identifier
                    {
                        Authority = "urn:eu:europa:ec:dgempl:eessi:ir",
                        Value = Sender
                    }
                },
                Receiver = new Participant
                {
                    ContactTypeIdentifier = "DataDestination",
                    Identifier = new Identifier
                    {
                        Authority = "urn:eu:europa:ec:dgempl:eessi:ir",
                        Value = Receiver
                    }
                },
                DocumentIdentification = new DocumentIdentification
                {
                    CreationDateAndTime = DateTime.UtcNow,
                    CaseAction = "Start",
                    Type = "SYN001",
                    InstanceIdentifier = Guid.NewGuid().ToString(),
                    TypeVersion = "4.2"
                },
                BusinessScope = new BusinessScope
                {
                    DocumentVersion = "1",
                    SetId = Guid.NewGuid().ToString(),
                    CaseId = new CaseId
                    {
                        InstanceIdentifier = Guid.NewGuid().ToString(),
                        BusinessServiceName = "SYNC_SUC_01:4.2"
                    }
                }
            };
        }

        private SYN001 GetSed()
        {
            return new SYN001
            {
                IRSync = new IRSync
                {
                    version = Version,
                    InstitutionRepository = new InstitutionRepository
                    {
                        AccessPoints = new AccessPointFaker().Generate(AccessPointCount),
                        Institutions = new InstitutionFaker().Generate(AccessPointCount * InstitutionsPerAccessPoint),
                        Certificates = new CertificateFaker().Generate(AccessPointCount * 3 + AccessPointCount * InstitutionsPerAccessPoint * 2),
                    }
                }
            };
        }
    
        private void LinkCertificates(StandardBusinessDocument doc)
        {
            var ir = doc.SYN001.IRSync.InstitutionRepository;
            
            for (int i=0; i<ir.AccessPoints.Count; i++)
            {
                var ebms = ir.AccessPoints[i].EbmsSignatureCertificates[0].CertificateIdentification;
                ebms.thumbprint = ir.Certificates[i * 3].thumbprint;

                var internalTls = ir.AccessPoints[i].InternalTLSCertificates[0].CertificateIdentification;
                internalTls.thumbprint = ir.Certificates[i * 3 + 1].thumbprint;

                var externalTls = ir.AccessPoints[i].ExternalTLSCertificates[0].CertificateIdentification;
                externalTls.thumbprint = ir.Certificates[i * 3 + 2].thumbprint;
            }

            for (int i = 0; i < ir.Institutions.Count; i++)
            {
                var ebms = ir.Institutions[i].EbmsSignatureCertificates[0].CertificateIdentification;
                ebms.thumbprint = ir.Certificates[ir.AccessPoints.Count + i * 2].thumbprint;

                var business = ir.Institutions[i].BusinessSignatureCertificates[0].CertificateIdentification;
                business.thumbprint = ir.Certificates[ir.AccessPoints.Count + i * 2 + 1].thumbprint;
            }
        }

        private void LinkInstitutions(StandardBusinessDocument doc)
        {
            var ir = doc.SYN001.IRSync.InstitutionRepository;

            for (int i = 0; i < ir.AccessPoints.Count; i++)
            {
                for (int j = 0; j < InstitutionsPerAccessPoint; j++)
                {
                    ir.AccessPoints[i].LinkedInstitutions.Add(
                        new LinkedInstitution
                        {
                            Institution = new InstitutionIdentification
                            {
                                officialID = ir.Institutions[i * InstitutionsPerAccessPoint + j].officialID,
                                countryCode = ir.Institutions[i * InstitutionsPerAccessPoint + j].countryCode
                            }
                        });
                }
            }
        }
    }
}
