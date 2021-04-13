using Bogus;
using System.Collections.Generic;

namespace SYN001.Factories
{
    public class CertificateFactory
    {
        Faker<Certificate> faker;

        public CertificateFactory()
        {
            faker = new Faker<Certificate>();
            faker.Rules((f, cert) =>
            {
                cert.serialNumber = f.Random.Hash();
                cert.subject = $"CN={f.Internet.DomainName()}, O={f.Company.CompanyName()}, OU={f.Random.Words(2)}, S={f.Address.County()}, L={f.Address.City()}, C={f.Address.CountryCode()}";
                cert.issuer = $"CN={f.Internet.DomainName()}, O={f.Company.CompanyName()}, OU={f.Random.Words(2)}, S={f.Address.County()}, L={f.Address.City()}, C={f.Address.CountryCode()}";
                cert.validFrom = f.Date.Past().ToUniversalTime().Date;
                cert.validTo = f.Date.Future().ToUniversalTime().Date;
                cert.thumbprint = f.Random.Hash();
                cert.publicKey = f.Random.Bytes(270);
            });
        }

        public List<Certificate> Create(int count)
        {
            return faker.Generate(count);
        }
    }
}
