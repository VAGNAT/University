using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.Controllers;
using System;
using University.UnitTests;

namespace University.Controllers.UnitTests
{
    [TestClass()]
    public class CourseControllerTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void CourseController_ConstructorTest_Parametr_Null() => new CourseController(null);

        [TestMethod()]
        public void AllCoursesTest_Dependency()
        {
            // act
            CourseController.AllCourses();
            // assert
            MockCourseService.Verify(x => x.GetAll(It.IsAny<int>()));
        }

        [TestMethod()]
        public void AddCourseTest_Dependency()
        {
            // act
            CourseController.AddCourse(FakeCourse);

            // assert
            MockCourseService.Verify(x => x.Create(It.IsAny<Course>()));
            MockCourseService.Verify(x => x.GetAll(It.IsAny<int>()));
            MockCourseService.Verify(x => x.Create(It.Is<Course>(val => val.Equals(FakeCourse))));
        }

        [TestMethod()]
        public void ChangeCourseTest_Dependency_Parametr_integer()
        {
            // arrange
            int idCourse = PositiveRandomNumber;

            // act
            CourseController.ChangeCourse(idCourse);

            // assert
            MockCourseService.Verify(x => x.Get(It.IsAny<int>()));
            MockCourseService.Verify(x => x.Get(It.Is<int>(val => val.Equals(idCourse))));
        }

        [TestMethod()]
        public void ChangeCourseTest_Dependency_Parametr_Course()
        {
            // act
            CourseController.ChangeCourse(FakeCourse);

            // assert
            MockCourseService.Verify(x => x.Update(It.IsAny<Course>()));
            MockCourseService.Verify(x => x.Update(It.Is<Course>(val => val.Equals(FakeCourse))));
            MockCourseService.Verify(x => x.GetAll(It.IsAny<int>()));
        }

        [TestMethod()]
        public void DeleteCourseTest_Dependency()
        {
            // arrange
            int idCourse = PositiveRandomNumber;

            // act
            CourseController.DeleteCourse(idCourse);

            // assert
            MockCourseService.Verify(x => x.Delete(It.IsAny<int>()));
            MockCourseService.Verify(x => x.Delete(It.Is<int>(val => val.Equals(idCourse))));
            MockCourseService.Verify(x => x.GetAll(It.IsAny<int>()));
        }


    }
}

