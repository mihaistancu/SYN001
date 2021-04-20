using Bogus;
using System.Collections.Generic;

namespace SYN001.Factories
{
    public class AccessPointFactory
    {
        Faker<AccessPoint> faker;

        public AccessPointFactory()
        {
            faker = new Faker<AccessPoint>();
            faker.Rules((f, ap) =>
            {
                var countryCode = f.Random.ListItem(Constraints.Countries);
                var languageCode = f.Random.RandomLocale().Substring(0, 2);
                var name = $"AP{countryCode}{f.Random.AlphaNumeric(4)}";

                ap.officialID = $"{countryCode}:{name}";
                ap.countryCode = new Value
                {
                    value = countryCode
                };
                ap.Name = new List<Translation>
                {
                    new Translation
                    {
                        languageCode = languageCode,
                        text = f.Company.CompanyName()
                    },
                    new Translation
                    {
                        languageCode = "en",
                        text = f.Company.CompanyName()
                    }
                };
                ap.ContactDetails = new ContactDetails
                {
                    ContactInfo = new ContactInfo
                    {
                        URLs = new List<URL>
                        {
                            new URL
                            {
                                uRL = f.Internet.Url()
                            }
                        },
                        EmailAddresses = new List<EmailAddress>
                        {
                            new EmailAddress
                            {
                                emailAddress = f.Internet.Email()
                            }
                        },
                        FaxNumbers = new List<FaxNumber>
                        {
                            new FaxNumber
                            {
                                faxNumber = f.Phone.PhoneNumber()
                            }
                        },
                        PhoneNumbers = new List<PhoneNumber>
                        {
                            new PhoneNumber
                            {
                                phoneNumber = f.Phone.PhoneNumber()
                            }
                        }
                    }
                };
                ap.PostalAddresses = new List<PostalAddress>
                {
                    new PostalAddress
                    {
                        isVisitorAddress = false,
                        Street = new Street
                        {
                            Translation = new Translation
                            {
                                languageCode = languageCode,
                                text = f.Address.StreetName()
                            }
                        },
                        Town = new Town
                        {
                            Translation = new Translation
                            {
                                languageCode = languageCode,
                                text = f.Address.City()
                            }
                        }
                    }
                };
                ap.LinkedInstitutions = new List<LinkedInstitution>();
                ap.EbMSSignatureCertificates = new List<EbMSSignatureCertificate>
                {
                    new EbMSSignatureCertificate
                    {
                        CertificateIdentification = new CertificateIdentification()
                    }
                };
                ap.InternalTLSCertificates = new List<InternalTLSCertificate>
                {
                    new InternalTLSCertificate
                    {
                        CertificateIdentification = new CertificateIdentification()
                    }
                };
                ap.ExternalTLSCertificates = new List<ExternalTLSCertificate>
                {
                    new ExternalTLSCertificate
                    {
                        CertificateIdentification = new CertificateIdentification()
                    }
                };
                ap.ValidityPeriod = new ValidityPeriod
                {
                    start = f.Date.Past().ToUniversalTime().Date
                };
                ap.SystemMessageEndpoint = new MessageEndpoint
                {
                    channel = $"https://{name.ToLower()}.eessi.testa.eu/eessi/",
                    messageExchangePattern = "push"
                };
                ap.BusinessMessageEndpoint = new MessageEndpoint
                {
                    channel = $"https://{name.ToLower()}.eessi.testa.eu/eessi/",
                    messageExchangePattern = "push"
                };
            });
        }
    
        public List<AccessPoint> Create(int count)
        {
            return faker.Generate(count);
        }
    }
}
