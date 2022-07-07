using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Services;
using System;
using University.UnitTests;

namespace University.Services.UnitTests
{
    [TestClass()]
    public class GroupServiceTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void GroupService_ConstructorTest_Parametr_Null() => new GroupService(null);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void CreateTest_Parametr_Null()
        {
            GroupService.Create(null);
        }

        [TestMethod()]
        public void CreateTest_Dependency()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Groups.Create(It.IsAny<Group>()));
            int actual = 1;

            // act
            GroupService.Create(FakeGroup);

            // assert
            MockUnitOfWork.Verify(x => x.Groups.Create(It.IsAny<Group>()), Times.Exactly(actual));
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        public void DeleteTest_Dependency_Empty_Group()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Groups.Empty(It.IsAny<int>())).Returns(true);
            MockUnitOfWork.Setup(x => x.Groups.Delete(It.IsAny<int>()));
            int actual = 1;

            // act
            GroupService.Delete(It.IsAny<int>());

            // assert
            MockUnitOfWork.Verify(x => x.Groups.Empty(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Groups.Delete(It.IsAny<int>()), Times.Exactly(actual));
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        public void DeleteTest_Dependency_NotEmpty_Group()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Groups.Empty(It.IsAny<int>())).Returns(false);
            MockUnitOfWork.Setup(x => x.Groups.Delete(It.IsAny<int>()));
            
            // act
            GroupService.Delete(It.IsAny<int>());

            // assert
            MockUnitOfWork.Verify(x => x.Groups.Empty(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Groups.Delete(It.IsAny<int>()), Times.Never);
            MockUnitOfWork.Verify(x => x.Save(), Times.Never);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_Parametr_Null()
        {
            GroupService.Update(null);
        }

        [TestMethod()]
        public void UpdateTest_Dependency()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Groups.Update(It.IsAny<Group>()));
            int actual = 1;

            // act
            GroupService.Update(FakeGroup);

            // assert
            MockUnitOfWork.Verify(x => x.Groups.Update(It.IsAny<Group>()), Times.Exactly(actual));
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        public void GetTest_Dependency()
        {
            // arrange
            int id = 123;
            MockUnitOfWork.Setup(x => x.Groups.Read(It.IsAny<int>()));

            // act
            GroupService.Get(id);

            // assert
            MockUnitOfWork.Verify(x => x.Groups.Read(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Groups.Read(It.Is<int>(val => val.Equals(id))));
        }

        [TestMethod()]
        public void GetAllTest_Dependency()
        {
            // arrange
            int idGroup = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Groups.ReadAll(It.IsAny<int>()));

            // act
            GroupService.GetAll(idGroup);

            // assert
            MockUnitOfWork.Verify(x => x.Groups.ReadAll(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Groups.ReadAll(It.Is<int>(val => val.Equals(idGroup))));
        }

        [TestMethod()]
        public void GetAllTest_Dependency_without_parametr()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Groups.ReadAll());

            // act
            GroupService.GetAll();

            // assert
            MockUnitOfWork.Verify(x => x.Groups.ReadAll());
        }
    }
}