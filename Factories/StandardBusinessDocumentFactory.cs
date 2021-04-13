using System;

namespace SYN001.Factories
{
    public class StandardBusinessDocumentFactory
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Version { get; set; }

        public InstitutionRepositoryFactory InstitutionRepositoryFactory { get; set; }

        public StandardBusinessDocumentFactory()
        {
            InstitutionRepositoryFactory = new InstitutionRepositoryFactory();
        }

        public StandardBusinessDocument Create()
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
                    InstitutionRepository = InstitutionRepositoryFactory.Create()
                }
            };
        }
    }
}
