using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using University.FunctionalTestsBDD;

namespace UniversityFunctionalTestBDD
{
    [Binding]
    [Scope(Feature = "Course")]
    public class CourseFeatureSteps : BaseFunctionalTest
    {
        private const string ControllerBaseAddress = "/Course/";
        private async Task AddCourse(string courseName, string courseDescription)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Name", courseName),
             new KeyValuePair<string, string>("Description", courseDescription)
        });

            await HttpClient.PostAsync($"AddCourse", content);
        }

        private async Task ChangeCourse(string courseName, string courseDescription)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Name", courseName),
             new KeyValuePair<string, string>("Description", courseDescription)
        });

            await HttpClient.PostAsync($"ChangeCourse", content);
        }

        private async Task DeleteCourse(int courseId)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            await HttpClient.GetAsync($"DeleteCourse/{courseId}");
        }

        [Given(@"Add an existing course")]
        public void AddAnExistingCourse()
        {
            FakeCourseRepository.Create(FakeCourse);
        }

        [When(@"Add a new course with the name (.*) and the description (.*)")]
        public async Task AddNewCourse(string courseName, string courseDescription)
        {
            await AddCourse(courseName, courseDescription);
        }

        [When(@"Change name to (.*) and description to (.*) in existing course")]
        public async Task ChangeNameAndDescriptionCourse(string courseName, string courseDescription)
        {
            await ChangeCourse(courseName, courseDescription);
        }

        [When(@"Delete existing course")]
        public async Task DeleteExistingCourse()
        {
            await DeleteCourse(FakeCourseRepository.ReadAll().ToList().IndexOf(FakeCourse));
        }

        [Then(@"The number of courses is equal to (.*)")]
        public void CheckNumberOfCourse(int count)
        {
            Assert.AreEqual(FakeCourseRepository.ReadAll().Count(), count);
        }

        [Then(@"The database contains a course with the name (.*) and the description (.*)")]
        public void CheckCourseInExistingOnes(string courseName, string courseDescription)
        {
            CollectionAssert.Contains(FakeCourseRepository.ReadAll().ToList(), new Course { Name = courseName, Description = courseDescription });
        }

        [Then(@"The database not contains a courses")]
        public void CheckCourseInNotExistingOnes()
        {
            CollectionAssert.DoesNotContain(FakeCourseRepository.ReadAll().ToList(), FakeCourse);
        }
    }
}