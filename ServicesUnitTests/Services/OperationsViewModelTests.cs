using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using System;
using University.UnitTests;

namespace University.Services.UnitTests
{
    [TestClass()]
    public class OperationsViewModelTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void OperationsViewModel_ConstructorTest_FirstParametr_Null() => new OperationsViewModel(null, MockGroupService.Object, MockStudentService.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void OperationsViewModel_ConstructorTest_SecondParametr_Null() => new OperationsViewModel(MockCourseService.Object, null, MockStudentService.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void OperationsViewModel_ConstructorTest_ThirdParametr_Null() => new OperationsViewModel(MockCourseService.Object, MockGroupService.Object, null);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void GetGroupFromGroupViewModelTest_Parametr_Null() => OperationsViewModel.GetGroupFromGroupViewModel(null);

        [TestMethod()]
        public void GetGroupFromGroupViewModelTest_Dependency()
        {
            //act
            OperationsViewModel.GetGroupFromGroupViewModel(FakeGroupViewModel);

            //assert
            MockCourseService.Verify(x => x.Get(It.IsAny<int>()));
            MockCourseService.Verify(x => x.Get(It.Is<int>(val => val.Equals(FakeGroupViewModel.CourseId))));
        }

        [TestMethod()]
        public void GetNewGroupViewModelTest_TwoParametr_Dependency()
        {
            //arrange
            int courseId = PositiveRandomNumber;
            int groupId = PositiveRandomNumber;
            MockGroupService.Setup(x => x.Get(It.IsAny<int>())).Returns(FakeGroup);
            MockCourseService.Setup(x => x.Get(It.IsAny<int>())).Returns(FakeCourse);

            //act
            OperationsViewModel.GetNewGroupViewModel(courseId, groupId);

            //assert
            MockCourseService.Verify(x => x.Get(It.IsAny<int>()));
            MockCourseService.Verify(x => x.Get(It.Is<int>(val => val.Equals(courseId))));
            MockGroupService.Verify(x => x.Get(It.IsAny<int>()));
            MockGroupService.Verify(x => x.Get(It.Is<int>(val => val.Equals(groupId))));
        }

        [TestMethod()]
        public void GetNewGroupViewModelTest_OneParametr_Dependency()
        {
            //arrange
            int courseId = PositiveRandomNumber;
            MockCourseService.Setup(x => x.Get(It.IsAny<int>())).Returns(FakeCourse);

            //act
            OperationsViewModel.GetNewGroupViewModel(courseId);

            //assert
            MockCourseService.Verify(x => x.Get(It.IsAny<int>()));
            MockCourseService.Verify(x => x.Get(It.Is<int>(val => val.Equals(courseId))));
        }

        [TestMethod()]
        public void GetNewStudentViewModelTest_OneParametr_Dependency()
        {
            //arrange
            int groupId = PositiveRandomNumber;
            MockGroupService.Setup(x => x.Get(It.IsAny<int>())).Returns(FakeGroup);

            //act
            OperationsViewModel.GetNewStudentViewModel(groupId);

            //assert
            MockGroupService.Verify(x => x.Get(It.IsAny<int>()));
            MockGroupService.Verify(x => x.Get(It.Is<int>(val => val.Equals(groupId))));
        }

        [TestMethod()]
        public void GetNewStudentViewModelTest_TwoParametr_Dependency()
        {
            //arrange
            int groupId = PositiveRandomNumber;
            int studentId = PositiveRandomNumber;
            MockGroupService.Setup(x => x.Get(It.IsAny<int>())).Returns(FakeGroup);
            MockStudentService.Setup(x => x.Get(It.IsAny<int>())).Returns(FakeStudent);

            //act
            OperationsViewModel.GetNewStudentViewModel(groupId, studentId);

            //assert
            MockGroupService.Verify(x => x.Get(It.IsAny<int>()));
            MockGroupService.Verify(x => x.Get(It.Is<int>(val => val.Equals(groupId))));
            MockStudentService.Verify(x => x.Get(It.IsAny<int>()));
            MockStudentService.Verify(x => x.Get(It.Is<int>(val => val.Equals(studentId))));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void GetStudentFromStudentViewModelTest_Parametr_Null() => OperationsViewModel.GetStudentFromStudentViewModel(null);

        [TestMethod()]
        public void GetStudentFromStudentViewModelTest_Dependency()
        {
            //act
            OperationsViewModel.GetStudentFromStudentViewModel(FakeStudentViewModel);

            //assert
            MockGroupService.Verify(x => x.Get(It.IsAny<int>()));
            MockGroupService.Verify(x => x.Get(It.Is<int>(val => val.Equals(FakeStudentViewModel.GroupId))));
        }
    }
}