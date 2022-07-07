using Bogus;
using Infrastructure.EF;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityIntegrationTests.FakeData;

namespace UniversityIntegrationTests
{
    public class BaseIntegrationTestDB
    {
        const int seed = 1234;
        protected UniversityContext Context { get; private set; }
        protected CourseRepository CourseRepository { get; private set; }
        protected GroupRepository GroupRepository { get; private set; }
        protected StudentRepository StudentRepository { get; private set; }
        protected Course FakeCourse { get; private set; }
        protected Group FakeGroup { get; private set; }
        protected Student FakeStudent { get; private set; }
        protected IEnumerable<Course> FakeCourses { get; private set; }
        protected IEnumerable<Group> FakeGroups { get; private set; }
        protected IEnumerable<Student> FakeStudents { get; private set; }
        
        [TestInitialize]
        public void Initialize()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<UniversityContext>();

            var databaseName = nameof(BaseIntegrationTestDB);

            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={databaseName};Trusted_Connection=True;MultipleActiveResultSets=true")
                .UseInternalServiceProvider(serviceProvider);

            Context = new UniversityContext(builder.Options);
            Context.Database.EnsureDeleted();
            Context.Database.Migrate();

            CourseRepository = new CourseRepository(Context);
            GroupRepository = new GroupRepository(Context);
            StudentRepository = new StudentRepository(Context);
                                    
            FakeCourse = new Faker<Course>().RuleFor(c => c.Name, c => c.Name.FirstName()).RuleFor(c => c.Description, c => c.Lorem.Text());
            FakeGroup = new Faker<Group>().RuleFor(g => g.Name, g => g.Name.FirstName()).RuleFor(g => g.Course, g => FakeCourse);
            FakeStudent = new Faker<Student>().RuleFor(s => s.FirstName, s => s.Name.FirstName()).RuleFor(s => s.LastName, s => s.Name.LastName()).RuleFor(g => g.Group, g => FakeGroup);
            FakeCourses = new FakeCourseData(seed).Valid.Generate(10);
            FakeGroups = new FakeGroupData(seed).Valid.Generate(10);
            FakeStudents = new FakeStudentData(seed).Valid.Generate(10);
                        
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}
