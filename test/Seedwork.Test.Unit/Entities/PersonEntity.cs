using Bogus;

namespace Hqv.Seedwork.Test.Unit.Entities
{
    public class PersonEntity
    {
        private static readonly Faker<PersonEntity> PeopleFaker;

        static PersonEntity()
        {
            PeopleFaker = new Faker<PersonEntity>()
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Age, f => f.Random.Number(10, 80));
        }

        public static PersonEntity Create()
        {
            return PeopleFaker.Generate();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string HairColor { get; set; }


    }
}