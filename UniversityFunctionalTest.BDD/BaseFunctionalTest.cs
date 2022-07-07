using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System.Threading.Tasks;
using System.Net.Http;
using Bogus;
using UniversityFunctionalTestsBDD.Fakes;
using TechTalk.SpecFlow;

namespace University.FunctionalTestsBDD
{

    public abstract class BaseFunctionalTest
    {
        const int seed = 1234;        
        private WebApplicationFactory<Program> _factory;
        protected Faker Faker { get; private set; }
        protected FakeCourseRepository FakeCourseRepository { get; private set; }
        protected FakeGroupRepository FakeGroupRepository { get; private set; }
        protected FakeStudentRepository FakeStudentRepository { get; private set; }
        protected Course FakeCourse { get; private set; }
        protected Group FakeGroup { get; private set; }
        protected Student FakeStudent { get; private set; }
        protected HttpClient HttpClient { get; private set; }
        protected int PositiveRandomNumber => Faker.Random.Number(seed);

        [BeforeScenario(Order = 0)]
        public async Task Initialize()
        {
            Faker = new Faker("en");

            FakeCourse = new Faker<Course>().RuleFor(c => c.Id, c => PositiveRandomNumber).RuleFor(c => c.Name, c => c.Name.FirstName()).RuleFor(c => c.Description, c => c.Lorem.Text());
            FakeGroup = new Faker<Group>().RuleFor(g => g.Id, g => PositiveRandomNumber).RuleFor(g => g.Name, g => g.Name.FirstName()).RuleFor(g => g.Course, g => FakeCourse);
            FakeStudent = new Faker<Student>().RuleFor(s => s.Id, s => PositiveRandomNumber).RuleFor(s => s.FirstName, s => s.Name.FirstName()).RuleFor(s => s.LastName, s => s.Name.LastName()).RuleFor(g => g.Group, g => FakeGroup);
            FakeCourseRepository = new FakeCourseRepository();
            FakeGroupRepository = new FakeGroupRepository();
            FakeStudentRepository = new FakeStudentRepository();

           
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {                    
                    services.AddSingleton<IRepository<Course>>((c) => FakeCourseRepository);
                    services.AddSingleton<IRepository<Group>>((g) => FakeGroupRepository);
                    services.AddSingleton<IRepository<Student>>((s) => FakeStudentRepository);
                });
            });
            HttpClient = _factory.CreateClient();
        }

        [AfterScenario(Order = 0)]
        public async Task Cleanup()
        {
            HttpClient?.Dispose();
            _factory?.Dispose();
        }
    }
}
