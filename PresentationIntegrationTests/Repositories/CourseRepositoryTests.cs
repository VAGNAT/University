using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using UniversityIntegrationTests;
using Model;

namespace University.Repositories.IntegrationTests
{
    [TestClass()]
    public class CourseRepositoryTests : BaseIntegrationTestDB
    {
        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            Course expectedCourse = FakeCourse;

            // Act
            CourseRepository.Create(FakeCourse);

            Context.SaveChanges();            

            // Assert          
            Course actualCourse = Context.Courses.FirstOrDefault();
            Assert.AreEqual(expectedCourse, actualCourse);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            Context.Courses.Add(FakeCourse);
            Context.SaveChanges();
            int courseId = Context.Courses.FirstOrDefault().Id;

            // Act
            CourseRepository.Delete(courseId);
            Context.SaveChanges();

            // Assert       
            Assert.IsNull(Context.Courses.FirstOrDefault());
        }

        [TestMethod()]
        public void EmptyCourseTest()
        {
            // Arrange
            Context.Courses.Add(FakeCourse);
            Context.SaveChanges();
            int courseId = Context.Courses.FirstOrDefault().Id;

            // Act
            bool actual = CourseRepository.Empty(courseId);

            // Assert       
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void NotEmptyCourseTest()
        {
            // Arrange
            Context.Courses.Add(FakeCourse);
            Context.Groups.Add(FakeGroup);
            Context.SaveChanges();

            int courseId = Context.Courses.FirstOrDefault().Id;

            // Act
            bool actual = CourseRepository.Empty(courseId);

            // Assert       
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void ReadTest()
        {
            // Arrange
            Course expectedCourse = FakeCourse;

            Context.Courses.Add(FakeCourse);
            Context.SaveChanges();

            int courseId = Context.Courses.FirstOrDefault().Id;

            // Act
            Course actualCourse = CourseRepository.Read(courseId);

            // Assert
            Assert.AreEqual(expectedCourse, actualCourse);
        }

        [TestMethod()]
        public void ReadAllTest()
        {
            // Arrange
            List<Course> expectedCourses = FakeCourses.ToList();
            Context.Courses.AddRange(expectedCourses);
            Context.SaveChanges();

            // Act
            List<Course> actualCourses = CourseRepository.ReadAll().ToList();

            // Assert
            CollectionAssert.AreEqual(expectedCourses, actualCourses);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange                        
            Context.Courses.Add(FakeCourse);

            Context.SaveChanges();
            
            Course expectedCourse = Context.Courses.Find(FakeCourse.Id);
            expectedCourse.Name = "Test";

            // Act
            CourseRepository.Update(expectedCourse);
            Context.SaveChanges();

            // Assert          
            Course actualCourse = Context.Courses.FirstOrDefault();
            Assert.AreEqual(expectedCourse, actualCourse);
        }
    }
}