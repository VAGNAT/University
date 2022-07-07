using Bogus;
using Infrastructure.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.Controllers;
using Services;
using Services.Interfaces;
using Services.ViewModel;
using System;

namespace University.UnitTests
{
    public abstract class BaseUnitTest
    {
        const int seed = 1234;
        protected Faker Faker { get; private set; }
        protected Mock<IUnitOfWork> MockUnitOfWork { get; private set; }
        protected Mock<ICRUD<Course>> MockCourseService { get; private set; }
        protected Mock<ICRUD<Group>> MockGroupService { get; private set; }
        protected Mock<ICRUD<Student>> MockStudentService { get; private set; }
        protected Mock<IOperationsViewModel> MockOperationsViewModel { get; private set; }
        protected CourseController CourseController { get; private set; }
        protected GroupController GroupController { get; private set; }
        protected StudentController StudentController { get; private set; }
        protected OperationsViewModel OperationsViewModel { get; private set; }
        protected CourseService CourseService { get; private set; }
        protected GroupService GroupService { get; private set; }
        protected StudentService StudentService { get; private set; }
        protected GroupViewModel FakeGroupViewModel { get; private set; }
        protected StudentViewModel FakeStudentViewModel { get; private set; }
        protected Course FakeCourse { get; private set; }
        protected Group FakeGroup { get; private set; }
        protected Student FakeStudent { get; private set; }
        protected int PositiveRandomNumber => Faker.Random.Number(seed); 

        [TestInitialize]
        public void Initialize()
        {
            Randomizer.Seed = new Random(seed);

            Faker = new Faker("en");

            MockUnitOfWork = new Mock<IUnitOfWork>();
            MockCourseService = new Mock<ICRUD<Course>>();
            MockGroupService = new Mock<ICRUD<Group>>();
            MockStudentService = new Mock<ICRUD<Student>>();
            MockOperationsViewModel = new Mock<IOperationsViewModel>();
            OperationsViewModel = new OperationsViewModel(MockCourseService.Object, MockGroupService.Object, MockStudentService.Object);
            CourseController = new CourseController(MockCourseService.Object);
            GroupController = new GroupController(MockCourseService.Object, MockGroupService.Object, MockOperationsViewModel.Object);
            StudentController = new StudentController(MockGroupService.Object, MockStudentService.Object, MockOperationsViewModel.Object);
            CourseService = new CourseService(MockUnitOfWork.Object);
            GroupService = new GroupService(MockUnitOfWork.Object);
            StudentService = new StudentService(MockUnitOfWork.Object);
            FakeCourse = new Faker<Course>().RuleFor(c => c.Id, c => PositiveRandomNumber).RuleFor(c => c.Name, c => c.Name.FirstName()).RuleFor(c => c.Description, c => c.Lorem.Text());
            FakeGroup = new Faker<Group>().RuleFor(g => g.Id, g => PositiveRandomNumber).RuleFor(g => g.Name, g => g.Name.FirstName()).RuleFor(g => g.Course, g => FakeCourse);
            FakeStudent = new Faker<Student>().RuleFor(s => s.Id, s => PositiveRandomNumber).RuleFor(s => s.FirstName, s => s.Name.FirstName()).RuleFor(s => s.LastName, s => s.Name.LastName()).RuleFor(g => g.Group, g => FakeGroup);
            FakeGroupViewModel = new Faker<GroupViewModel>().RuleFor(g => g.Group, g => FakeGroup).RuleFor(g => g.CourseId, g => FakeCourse.Id).RuleFor(g => g.CourseName, g => FakeCourse.Name);
            FakeStudentViewModel = new Faker<StudentViewModel>().RuleFor(s => s.Student, s => FakeStudent).RuleFor(s => s.GroupId, s => FakeGroup.Id).RuleFor(s => s.GroupName, s => FakeGroup.Name);
        }
    }
}
