using Bogus;
using System.Collections.Generic;

namespace SYN001.Factories
{
    public class CompetenceFactory
    {
        Faker<Competence> faker;

        public CompetenceFactory()
        {
            faker = new Faker<Competence>();
            faker.Rules((f, competence) =>
            {
                competence.id = f.Random.AlphaNumeric(7);
                competence.applicationRole = f.Random.ListItem(Constraints.Roles);
                competence.ValidityPeriod = new ValidityPeriod
                {
                    start = f.Date.Past().ToUniversalTime().Date
                };
                competence.BUCType = new BUCType
                { 
                    name = f.Random.Replace("#_???_##")
                };
                competence.isEESSIReady = f.Random.Bool();
            });
        }

        public List<Competence> Create(int count)
        {
            return faker.Generate(count);
        }
    }
}
