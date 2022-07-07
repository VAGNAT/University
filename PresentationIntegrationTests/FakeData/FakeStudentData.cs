using Bogus;
using Model;

namespace UniversityIntegrationTests.FakeData
{
    public class FakeStudentData
    {
        public FakeStudentData(int seed)
        {
            Valid = Valid.UseSeed(seed);
        }

        public Faker<Student> Valid { get; private set; }
            = new Faker<Student>("en")
                .CustomInstantiator(f => new Student { FirstName = f.Name.FirstName(), LastName = f.Name.LastName() });
    }
}
