using Bogus;
using System;
using System.Collections.Generic;

namespace SYN001.Fakers
{
    public class AccessPointFaker: Faker<AccessPoint>
    {
        public AccessPointFaker()
        {
            Rules((f, ap) =>
            {
                var countryCode = f.Address.CountryCode();
                var languageCode = f.Random.RandomLocale().Substring(0, 2);
                var name = $"AP{countryCode}{f.Random.Number(99):D2}";
                
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
    }
}
