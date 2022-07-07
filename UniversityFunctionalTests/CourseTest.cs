using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace University.FunctionalTests
{
    [TestClass]
    public class CourseTest : BaseFunctionalTest
    {
        private const string ControllerBaseAddress = "/Course/";
        
        [TestMethod]
        public async Task Given_OneExistingCourses_OneNewCourse_When_AddNewCourse_Then_TwoExistingCourses_NewCourseInExistingOnes()
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);
                        
            const string courseName = "Chemical";
            const string courseDescription = "During their studies, students undergo several types of internships: educational - in the form of excursions to the chemical laboratories of enterprises and research institutes of the city.";

            // Given
            FakeCourseRepository.Create(FakeCourse);

            // When
            Course course = await AddCourse(courseName, courseDescription);

            // Then
            Assert.AreEqual(FakeCourseRepository.ReadAll().Count(), 2);
            CollectionAssert.Contains(FakeCourseRepository.ReadAll().ToList(), course);
        }

        [TestMethod]
        public async Task Given_OneExistingCourses_NewNameCourse_NewDescriptionCourse_When_ChangeNameAndChangeDescription_Then_OneExistingCourses_CourseWithNewNameAndNewDescriptionInExistingOnes()
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            const string newCourseName = "Chemical";
            const string newCourseDescription = "During their studies, students undergo several types of internships: educational - in the form of excursions to the chemical laboratories of enterprises and research institutes of the city.";

            // Given
            FakeCourseRepository.Create(FakeCourse);

            // When
            Course course = await ChangeCourse(newCourseName, newCourseDescription);

            // Then
            Assert.AreEqual(FakeCourseRepository.ReadAll().Count(), 1);
            CollectionAssert.Contains(FakeCourseRepository.ReadAll().ToList(), course);
        }

        [TestMethod]
        public async Task Given_OneExistingCourses_When_DeleteExistingCourse_Then_ZeroExistingCourses_RemoveCourseIsNotInExistingOnes()
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            // Given
            FakeCourseRepository.Create(FakeCourse);

            // When
            await DeleteCourse(FakeCourseRepository.ReadAll().ToList().IndexOf(FakeCourse));

            // Then
            Assert.AreEqual(FakeCourseRepository.ReadAll().Count(), 0);
            CollectionAssert.DoesNotContain(FakeCourseRepository.ReadAll().ToList(), FakeCourse);
        }

        private async Task<Course> AddCourse(string courseName, string courseDescription)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Name", courseName),
             new KeyValuePair<string, string>("Description", courseDescription)
        });

            await HttpClient.PostAsync($"AddCourse", content);
            return new Course { Name = courseName, Description = courseDescription };
        }

        private async Task<Course> ChangeCourse(string courseName, string courseDescription)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Name", courseName),
             new KeyValuePair<string, string>("Description", courseDescription)
        });

            await HttpClient.PostAsync($"ChangeCourse", content);
            return new Course { Name = courseName, Description = courseDescription };
        }

        private async Task DeleteCourse(int courseId)
        {            
            await HttpClient.GetAsync($"DeleteCourse/{courseId}");            
        }
    }
}