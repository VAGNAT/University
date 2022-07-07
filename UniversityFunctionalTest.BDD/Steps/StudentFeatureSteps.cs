using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using University.FunctionalTestsBDD;

namespace UniversityFunctionalTest.BDD.Steps
{
    [Binding]
    [Scope(Feature = "Student")]
    public class StudentFeatureSteps : BaseFunctionalTest
    {
        private const string ControllerBaseAddress = "/Student/";
        private async Task AddStudent(string studentFirstName, string studentLastName)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Student.FirstName", studentFirstName),
             new KeyValuePair<string, string>("Student.LastName", studentLastName),
             new KeyValuePair<string, string>("GroupId", "0"),
             new KeyValuePair<string, string>("GroupName", FakeGroup.Name)
        });

            await HttpClient.PostAsync($"AddStudent", content);
        }

        private async Task ChangeStudent(string studentFirstName, string studentLastName)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Student.FirstName", studentFirstName),
             new KeyValuePair<string, string>("Student.LastName", studentLastName),
             new KeyValuePair<string, string>("GroupId", "0"),
             new KeyValuePair<string, string>("GroupName", FakeGroup.Name)
        });

            await HttpClient.PostAsync($"ChangeStudent", content);
        }

        private async Task DeleteStudent(int studentId)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            await HttpClient.GetAsync($"DeleteStudent/{studentId}");
        }

        [Given(@"Add an existing group")]
        public void AddAnExistingGroup()
        {
            FakeGroupRepository.Create(FakeGroup);
        }

        [Given(@"Add an existing student")]
        public void AddAnExistingStudent()
        {
            FakeStudentRepository.Create(FakeStudent);
        }

        [When(@"Add a new student with the first name (.*) and the last name (.*)")]
        public async Task AddNewStudent(string studentFirstName, string studentLastName)
        {
            await AddStudent(studentFirstName, studentLastName);
        }

        [When(@"Change first name to (.*) and last name to (.*) in existing student")]
        public async Task ChangeNameStudent(string studentFirstName, string studentLastName)
        {
            await ChangeStudent(studentFirstName, studentLastName);
        }

        [When(@"Delete existing student")]
        public async Task DeleteExistingStudent()
        {
            await DeleteStudent(FakeStudentRepository.ReadAll().ToList().IndexOf(FakeStudent));
        }

        [Then(@"The number of students is equal to (.*)")]
        public void CheckNumberOfStudent(int count)
        {
            Assert.AreEqual(FakeStudentRepository.ReadAll().Count(), count);
        }

        [Then(@"The database contains a student with the first name (.*) and the last name (.*)")]
        public void CheckStudentInExistingOnes(string studentFirstName, string studentLastName)
        {
            CollectionAssert.Contains(FakeStudentRepository.ReadAll().ToList(), new Student { FirstName = studentFirstName, LastName = studentLastName, Group = FakeGroup });
        }

        [Then(@"The database not contains a students")]
        public void CheckGroupInNotExistingOnes()
        {
            CollectionAssert.DoesNotContain(FakeStudentRepository.ReadAll().ToList(), FakeStudent);
        }
    }
}
