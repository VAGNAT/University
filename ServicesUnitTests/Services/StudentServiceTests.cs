using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Services;
using System;
using University.UnitTests;

namespace University.Services.UnitTests
{
    [TestClass()]
    public class StudentServiceTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void StudentService_ConstructorTest_parametr_Null() => new StudentService(null);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void CreateTest_Parametr_Null() => StudentService.Create(null);

        [TestMethod()]
        public void CreateTest_Dependency()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Students.Create(It.IsAny<Student>()));
            int actual = 1;

            // act
            StudentService.Create(FakeStudent);

            // assert
            MockUnitOfWork.Verify(x => x.Students.Create(It.IsAny<Student>()), Times.Exactly(actual));
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        public void DeleteTest_Dependency()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Students.Delete(It.IsAny<int>()));
            int actual = 1;

            // act
            StudentService.Delete(It.IsAny<int>());

            // assert
            MockUnitOfWork.Verify(x => x.Students.Delete(It.IsAny<int>()), Times.Exactly(actual));
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_Parametr_Null()
        {
            StudentService.Update(null);
        }

        [TestMethod()]
        public void UpdateTest_Dependency()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Students.Update(It.IsAny<Student>()));
            int actual = 1;

            // act
            StudentService.Update(FakeStudent);

            // assert
            MockUnitOfWork.Verify(x => x.Students.Update(It.IsAny<Student>()), Times.Exactly(actual));
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        public void GetTest_Dependency()
        {
            // arrange
            int idGroup = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Students.Read(It.IsAny<int>()));

            // act
            StudentService.Get(idGroup);

            // assert
            MockUnitOfWork.Verify(x => x.Students.Read(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Students.Read(It.Is<int>(val => val.Equals(idGroup))));
        }

        [TestMethod()]
        public void GetAllTest_Dependency()
        {
            // arrange
            int idGroup = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Students.ReadAll(It.IsAny<int>()));

            // act
            StudentService.GetAll(idGroup);

            // assert
            MockUnitOfWork.Verify(x => x.Students.ReadAll(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Students.ReadAll(It.Is<int>(val => val.Equals(idGroup))));
        }

        [TestMethod()]
        public void GetAllTest_Dependency_without_parametr()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Students.ReadAll());

            // act
            StudentService.GetAll();

            // assert
            MockUnitOfWork.Verify(x => x.Students.ReadAll());
        }
    }
}