namespace SYN001.Factories
{
    public class InstitutionRepositoryFactory
    {
        public int AccessPointCount { get; set; }
        public int InstitutionsPerAccessPoint { get; set; }

        public AccessPointFactory AccessPointFactory { get; set; }
        public InstitutionFactory InstitutionFactory { get; set; }
        public CertificateFactory CertificateFactory { get; set; }

        public InstitutionRepository Create()
        {
            var institutionCount = AccessPointCount * InstitutionsPerAccessPoint;

            var result = new InstitutionRepository
            {
                AccessPoints = AccessPointFactory.Create(AccessPointCount),
                Institutions = InstitutionFactory.Create(institutionCount),
                Certificates = CertificateFactory.Create(AccessPointCount * 3 + institutionCount * 2)
            };

            LinkInstitutions(result);
            LinkCertificates(result);
            return result;
        }

        private void LinkInstitutions(InstitutionRepository ir)
        {
            for (int i = 0; i < ir.AccessPoints.Count; i++) 
            {
                for (int j = 0; j < InstitutionsPerAccessPoint; j++)
                {
                    var k = i * InstitutionsPerAccessPoint + j;
                    ir.AccessPoints[i].LinkedInstitutions.Add(new LinkedInstitution
                    {
                        Institution = new InstitutionIdentification
                        {
                            officialID = ir.Institutions[k].officialID,
                            countryCode = ir.Institutions[k].countryCode
                        },
                        ValidityPeriod = ir.Institutions[k].ValidityPeriod
                    });
                }
            }
        }

        private void LinkCertificates(InstitutionRepository ir)
        {
            for (int i = 0; i < ir.AccessPoints.Count; i++)
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
    }
}
