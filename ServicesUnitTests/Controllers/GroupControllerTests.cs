using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.Controllers;
using Services.ViewModel;
using System;
using University.UnitTests;

namespace University.Controllers.UnitTests
{
    [TestClass()]
    public class GroupControllerTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void GroupController_ConstructorTest_FirstParametr_Null() => new GroupController(null, MockGroupService.Object, MockOperationsViewModel.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void GroupController_ConstructorTest_SecondParametr_Null() => new GroupController(MockCourseService.Object, null, MockOperationsViewModel.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void GroupController_ConstructorTest_ThirdParametr_Null() => new GroupController(MockCourseService.Object, MockGroupService.Object, null);

        [TestMethod()]
        public void AllGroupsTest_Dependency()
        {
            //arrange
            int courseId = PositiveRandomNumber;

            // act
            GroupController.AllGroups(courseId);

            // assert
            MockGroupService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockGroupService.Verify(x => x.GetAll(It.Is<int>(val => val.Equals(courseId))));
            MockCourseService.Verify(x => x.Get(It.IsAny<int>()));
            MockCourseService.Verify(x => x.Get(It.Is<int>(val => val.Equals(courseId))));
        }

        [TestMethod()]
        public void AddGroupTest_HTTPGet_Dependency()
        {
            //arrange
            int courseId = PositiveRandomNumber;

            // act
            GroupController.AddGroup(courseId);

            // assert
            MockOperationsViewModel.Verify(x => x.GetNewGroupViewModel(It.IsAny<int>()));
            MockOperationsViewModel.Verify(x => x.GetNewGroupViewModel(It.Is<int>(val => val.Equals(courseId))));
        }

        [TestMethod()]
        public void AddGroupTest_HTTPPost_Dependency()
        {
            //arange
            MockOperationsViewModel.Setup(x => x.GetGroupFromGroupViewModel(It.IsAny<GroupViewModel>())).Returns(FakeGroup);

            // act
            GroupController.AddGroup(FakeGroupViewModel);

            // assert
            MockOperationsViewModel.Verify(x => x.GetGroupFromGroupViewModel(It.IsAny<GroupViewModel>()));
            MockOperationsViewModel.Verify(x => x.GetGroupFromGroupViewModel(It.Is<GroupViewModel>(val => val.Equals(FakeGroupViewModel))));
            MockGroupService.Verify(x => x.Create(It.IsAny<Group>()));
            MockGroupService.Verify(x => x.Create(It.Is<Group>(val => val.Equals(FakeGroup))));
            MockGroupService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockGroupService.Verify(x => x.GetAll(It.Is<int>(val => val.Equals(FakeGroup.Course.Id))));
        }

        [TestMethod()]
        public void ChangeGroupTest_HTTPGet_Dependency()
        {
            //arrange
            int courseId = PositiveRandomNumber;
            int groupId = PositiveRandomNumber;

            // act
            GroupController.ChangeGroup(groupId, courseId);

            // assert
            MockOperationsViewModel.Verify(x => x.GetNewGroupViewModel(It.IsAny<int>(), It.IsAny<int>()));
            MockOperationsViewModel.Verify(x => x.GetNewGroupViewModel(It.Is<int>(val => val.Equals(courseId)), It.Is<int>(val => val.Equals(groupId))));
        }

        [TestMethod()]
        public void ChangeGroupTest_HTTPPost_Dependency()
        {
            //arange
            MockOperationsViewModel.Setup(x => x.GetGroupFromGroupViewModel(It.IsAny<GroupViewModel>())).Returns(FakeGroup);

            // act
            GroupController.ChangeGroup(FakeGroupViewModel);

            // assert
            MockOperationsViewModel.Verify(x => x.GetGroupFromGroupViewModel(It.IsAny<GroupViewModel>()));
            MockOperationsViewModel.Verify(x => x.GetGroupFromGroupViewModel(It.Is<GroupViewModel>(val => val.Equals(FakeGroupViewModel))));
            MockGroupService.Verify(x => x.Update(It.IsAny<Group>()));
            MockGroupService.Verify(x => x.Update(It.Is<Group>(val => val.Equals(FakeGroup))));
            MockGroupService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockGroupService.Verify(x => x.GetAll(It.Is<int>(val => val.Equals(FakeGroup.Course.Id))));
        }

        [TestMethod()]
        public void DeleteGroupTest_Dependency()
        {
            //arange
            int groupId = PositiveRandomNumber;
            int courseId = PositiveRandomNumber;
            MockCourseService.Setup(x => x.Get(It.IsAny<int>())).Returns(FakeCourse);

            // act
            GroupController.DeleteGroup(groupId, courseId);

            // assert
            MockGroupService.Verify(x => x.Delete(It.IsAny<int>()));
            MockGroupService.Verify(x => x.Delete(It.Is<int>(val => val.Equals(groupId))));
            MockCourseService.Verify(x => x.Get(It.IsAny<int>()));
            MockCourseService.Verify(x => x.Get(It.Is<int>(val => val.Equals(courseId))));
            MockGroupService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockGroupService.Verify(x => x.GetAll(It.Is<int>(val => val.Equals(FakeCourse.Id))));
        }
    }
}