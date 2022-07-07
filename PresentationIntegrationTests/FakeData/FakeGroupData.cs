using Bogus;
using Model;

namespace UniversityIntegrationTests.FakeData
{
    public class FakeGroupData
    {
        public FakeGroupData(int seed)
        {
            Valid = Valid.UseSeed(seed);
        }
        public Faker<Group> Valid { get; private set; }
            = new Faker<Group>("en")
                .CustomInstantiator(f => new Group { Name = f.Name.FullName(), Course = new Course() });
    }
}
