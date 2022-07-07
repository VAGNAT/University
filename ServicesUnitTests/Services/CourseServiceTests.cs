using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Services;
using System;
using University.UnitTests;

namespace University.Services.UnitTests
{
    [TestClass()]
    public class CourseServiceTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void CourseService_ConstructorTest_Parametr_Null() => new CourseService(null);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void CreateTest_Parametr_Null()
        {
            CourseService.Create(null);
        }

        [TestMethod()]
        public void CreateTest_Dependency()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Courses.Create(It.IsAny<Course>()));
            int actual = 1;

            // act
            CourseService.Create(FakeCourse);

            // assert
            MockUnitOfWork.Verify(x => x.Courses.Create(It.IsAny<Course>()), Times.Exactly(actual));
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        public void DeleteTest_Dependency_Empty_Course()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Courses.Empty(It.IsAny<int>())).Returns(true);
            MockUnitOfWork.Setup(x => x.Courses.Delete(It.IsAny<int>()));
            int actual = 1;

            // act
            CourseService.Delete(It.IsAny<int>());

            // assert
            MockUnitOfWork.Verify(x => x.Courses.Empty(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Courses.Delete(It.IsAny<int>()), Times.Exactly(actual));
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        public void DeleteTest_Dependency_NotEmpty_Course()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Courses.Empty(It.IsAny<int>())).Returns(false);
            MockUnitOfWork.Setup(x => x.Courses.Delete(It.IsAny<int>()));

            // act
            CourseService.Delete(It.IsAny<int>());

            // assert
            MockUnitOfWork.Verify(x => x.Courses.Empty(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Courses.Delete(It.IsAny<int>()), Times.Never);
            MockUnitOfWork.Verify(x => x.Save(), Times.Never);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_Parametr_Null()
        {
            CourseService.Update(null);
        }

        [TestMethod()]
        public void UpdateTest_Dependency()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Courses.Update(It.IsAny<Course>()));
            int actual = 1;

            // act
            CourseService.Update(FakeCourse);

            // assert
            MockUnitOfWork.Verify(x => x.Save(), Times.Exactly(actual));
        }

        [TestMethod()]
        public void GetTest_Dependency()
        {
            // arrange
            int id = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Courses.Read(It.IsAny<int>()));

            // act
            CourseService.Get(id);

            // assert
            MockUnitOfWork.Verify(x => x.Courses.Read(It.IsAny<int>()));
            MockUnitOfWork.Verify(x => x.Courses.Read(It.Is<int>(val => val.Equals(id))));
        }

        [TestMethod()]
        public void GetAllTest_Dependency()
        {
            // arrange
            MockUnitOfWork.Setup(x => x.Courses.ReadAll());

            // act
            CourseService.GetAll();

            // assert
            MockUnitOfWork.Verify(x => x.Courses.ReadAll());
        }
    }
}