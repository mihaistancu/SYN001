using Bogus;

namespace SYN001.Fakers
{
    public class CertificateFaker: Faker<Certificate>
    {
        public CertificateFaker()
        {
            Rules((f, cert) =>
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
    }
}
