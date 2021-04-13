using Bogus;
using System.Collections.Generic;

namespace SYN001.Fakers
{
    public class InstitutionFaker: Faker<Institution>
    {
        public InstitutionFaker()
        {
            Rules((f, institution) =>
            {
                var countryCode = f.Address.CountryCode();
                var languageCode = f.Random.RandomLocale().Substring(0, 2);

                institution.officialID = $"{countryCode}:{f.Random.AlphaNumeric(10)}";
                institution.cLDId = f.Company.CompanyName();
                institution.countryCode = new Value
                {
                    value = countryCode
                };
                institution.isPublicIndicator = new Value
                {
                    value = f.Random.Number(1).ToString()
                };
                institution.isLiaisonBodyIndicator = new Value
                {
                    value = f.Random.Number(1).ToString()
                };
                institution.Relations = new List<Relation>();
                institution.Name = new List<Translation>
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
                institution.ValidityPeriod = new ValidityPeriod
                {
                    start = f.Date.Past().ToUniversalTime().Date
                };
                institution.ContactInfo = new ContactInfo
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
                };
                institution.PostalAddresses = new List<PostalAddress>
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
                institution.EbmsSignatureCertificates = new List<EbmsSignatureCertificate>
                {
                    new EbmsSignatureCertificate
                    {
                        CertificateIdentification = new CertificateIdentification()
                    }
                };
                institution.BusinessSignatureCertificates = new List<BusinessSignatureCertificate>
                {
                    new BusinessSignatureCertificate
                    {
                        CertificateIdentification = new CertificateIdentification()
                    }
                };
                institution.SystemMessageEndpoint = new MessageEndpoint
                {
                    channel = f.Random.AlphaNumeric(10),
                    messageExchangePattern = "pull"
                };
                institution.BusinessMessageEndpoint = new MessageEndpoint
                {
                    channel = $"{f.Random.AlphaNumeric(10)}:SYSTEM",
                    messageExchangePattern = "pull"
                };
            });
        }
    }
}
