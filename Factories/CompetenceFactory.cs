using Bogus;
using System.Collections.Generic;

namespace SYN001.Factories
{
    public class CompetenceFactory
    {
        Faker<Competence> faker = new Faker<Competence>();

        public CompetenceFactory()
        {
            faker.Rules((f, competence) =>
            {
                competence.id = f.Random.AlphaNumeric(7);
                competence.applicationRole = f.Random.ListItem(new List<string> {"CaseOwner", "CounterParty", "IntelligentRA"});
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
