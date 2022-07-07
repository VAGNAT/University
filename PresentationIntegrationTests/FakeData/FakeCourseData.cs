using Bogus;
using Model;

namespace UniversityIntegrationTests.FakeData
{
    public class FakeCourseData
    {
        public FakeCourseData(int seed)
        {
            Valid = Valid.UseSeed(seed);
        }

        public Faker<Course> Valid { get; private set; }
            = new Faker<Course>("en")
                .CustomInstantiator(f => new Course { Name = f.Name.FullName(), Description = f.Lorem.Text() });
    }
}
