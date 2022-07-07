using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System.Threading.Tasks;
using System.Net.Http;
using UniversityIntegrationTests.Fakes;
using UniversityIntegrationTests.FakeData;
using AngleSharp.Html.Parser;

namespace University.IntegrationTests
{

    public abstract class BaseIntegrationTestView
    {
        private WebApplicationFactory<Program> _factory;
        protected FakeCourseData FakeCourseData { get; private set; }
        protected FakeCourseRepository FakeCourseRepository { get; private set; }
        protected FakeGroupData FakeGroupData { get; private set; }
        protected FakeGroupRepository FakeGroupRepository { get; private set; }
        protected FakeStudentData FakeStudentData { get; private set; }
        protected FakeStudentRepository FakeStudentRepository { get; private set; }
        protected HttpClient HttpClient { get; private set; }
        protected HtmlParser Parser { get; private set; }

        [TestInitialize]
        public async Task Initialize()
        {
            const int seed = 1234;

            FakeCourseData = new FakeCourseData(seed);
            FakeGroupData = new FakeGroupData(seed);
            FakeStudentData = new FakeStudentData(seed);
            FakeCourseRepository = new FakeCourseRepository(FakeCourseData);
            FakeGroupRepository = new FakeGroupRepository(FakeGroupData);
            FakeStudentRepository = new FakeStudentRepository(FakeStudentData);

            Parser = new HtmlParser();

            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {                    
                    services.AddScoped<IRepository<Course>>((c) => FakeCourseRepository);
                    services.AddScoped<IRepository<Group>>((g) => FakeGroupRepository);
                    services.AddScoped<IRepository<Student>>((s) => FakeStudentRepository);                    
                });
            });
            HttpClient = _factory.CreateClient();
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            HttpClient?.Dispose();
            _factory?.Dispose();
        }
    }
}
