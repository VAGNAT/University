using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.Controllers;
using Services.ViewModel;
using System;
using University.UnitTests;

namespace University.Controller.UnitTests
{
    [TestClass()]
    public class StudentControllerTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void StudentController_ConstructorTest_FirstParametr_Null() => new StudentController(null, MockStudentService.Object, MockOperationsViewModel.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void StudentController_ConstructorTest_SecondParametr_Null() => new StudentController(MockGroupService.Object, null, MockOperationsViewModel.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void StudentController_ConstructorTest_ThirdParametr_Null() => new StudentController(MockGroupService.Object, MockStudentService.Object, null);
        
        [TestMethod()]
        public void AllStudentsTest_Dependency()
        {
            //arrange
            int groupId = PositiveRandomNumber;

            // act
            StudentController.AllStudents(groupId);

            // assert
            MockGroupService.Verify(x => x.Get(It.IsAny<int>()));
            MockGroupService.Verify(x => x.Get(It.Is<int>(val => val.Equals(groupId))));
            MockStudentService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockStudentService.Verify(x => x.GetAll(It.Is<int>(val => val.Equals(groupId))));
        }

        [TestMethod()]
        public void AddStudentTest_HTTPGet_Dependency()
        {
            //arrange
            int groupId = PositiveRandomNumber;

            // act
            StudentController.AddStudent(groupId);

            // assert
            MockOperationsViewModel.Verify(x => x.GetNewStudentViewModel(It.IsAny<int>()));
            MockOperationsViewModel.Verify(x => x.GetNewStudentViewModel(It.Is<int>(val => val.Equals(groupId))));
        }

        [TestMethod()]
        public void AddStudentTest_HTTPPost_Dependency()
        {
            //arange
            MockOperationsViewModel.Setup(x => x.GetStudentFromStudentViewModel(It.IsAny<StudentViewModel>())).Returns(FakeStudent);

            // act
            StudentController.AddStudent(FakeStudentViewModel);

            // assert
            MockOperationsViewModel.Verify(x => x.GetStudentFromStudentViewModel(It.IsAny<StudentViewModel>()));
            MockOperationsViewModel.Verify(x => x.GetStudentFromStudentViewModel(It.Is<StudentViewModel>(val => val.Equals(FakeStudentViewModel))));
            MockStudentService.Verify(x => x.Create(It.IsAny<Student>()));
            MockStudentService.Verify(x => x.Create(It.Is<Student>(val => val.Equals(FakeStudent))));
            MockStudentService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockStudentService.Verify(x => x.GetAll(It.Is<int>(val => val.Equals(FakeStudent.Group.Id))));
        }

        [TestMethod()]
        public void ChangeStudentTest_HTTPGet_Dependency()
        {
            //arrange
            int studentId = PositiveRandomNumber;
            int groupId = PositiveRandomNumber;

            // act
            StudentController.ChangeStudent(studentId, groupId);

            // assert
            MockOperationsViewModel.Verify(x => x.GetNewStudentViewModel(It.IsAny<int>(), It.IsAny<int>()));
            MockOperationsViewModel.Verify(x => x.GetNewStudentViewModel(It.Is<int>(val => val.Equals(groupId)), It.Is<int>(val => val.Equals(studentId))));
        }

        [TestMethod()]
        public void ChangeStudentTest_HTTPPost_Dependency()
        {
            //arange
            MockOperationsViewModel.Setup(x => x.GetStudentFromStudentViewModel(It.IsAny<StudentViewModel>())).Returns(FakeStudent);

            // act
            StudentController.ChangeStudent(FakeStudentViewModel);

            // assert
            MockOperationsViewModel.Verify(x => x.GetStudentFromStudentViewModel(It.IsAny<StudentViewModel>()));
            MockOperationsViewModel.Verify(x => x.GetStudentFromStudentViewModel(It.Is<StudentViewModel>(val => val.Equals(FakeStudentViewModel))));
            MockStudentService.Verify(x => x.Update(It.IsAny<Student>()));
            MockStudentService.Verify(x => x.Update(It.Is<Student>(val => val.Equals(FakeStudent))));
            MockStudentService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockStudentService.Verify(x => x.GetAll(It.Is<int>(val => val.Equals(FakeStudent.Group.Id))));
        }

        [TestMethod()]
        public void DeleteStudentTest_Dependency()
        {
            //arange
            int groupId = PositiveRandomNumber;
            int studentId = PositiveRandomNumber;
            MockGroupService.Setup(x => x.Get(It.IsAny<int>())).Returns(FakeGroup);

            // act
            StudentController.DeleteStudent(studentId, groupId);

            // assert
            MockStudentService.Verify(x => x.Delete(It.IsAny<int>()));
            MockStudentService.Verify(x => x.Delete(It.Is<int>(val => val.Equals(studentId))));
            MockGroupService.Verify(x => x.Get(It.IsAny<int>()));
            MockGroupService.Verify(x => x.Get(It.Is<int>(val => val.Equals(groupId))));
            MockStudentService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockStudentService.Verify(x => x.GetAll(It.Is<int>(val => val.Equals(FakeGroup.Id))));
        }
    }
}