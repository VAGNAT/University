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
    [Scope(Feature = "Group")]
    public class GroupFeatureSteps : BaseFunctionalTest
    {
        private const string ControllerBaseAddress = "/Group/";
        private async Task AddGroup(string groupName)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Group.Name", groupName),
             new KeyValuePair<string, string>("CourseId", "0"),
             new KeyValuePair<string, string>("CourseName", FakeCourse.Name)
        });

            await HttpClient.PostAsync($"AddGroup", content);
        }

        private async Task ChangeGroup(string groupName)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Group.Name", groupName),
             new KeyValuePair<string, string>("CourseId", "0"),
             new KeyValuePair<string, string>("CourseName", FakeCourse.Name)
        });

            await HttpClient.PostAsync($"ChangeGroup", content);
        }

        private async Task DeleteGroup(int groupId)
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            await HttpClient.GetAsync($"DeleteGroup/{groupId}");
        }

        [Given(@"Add an existing course")]
        public void AddAnExistingCourse()
        {
            FakeCourseRepository.Create(FakeCourse);
        }

        [Given(@"Add an existing group")]
        public void AddAnExistingGroup()
        {
            FakeGroupRepository.Create(FakeGroup);
        }

        [When(@"Add a new group with the name (.*)")]
        public async Task AddNewGroup(string groupName)
        {
            await AddGroup(groupName);
        }

        [When(@"Change name to (.*) in existing group")]
        public async Task ChangeNameGroup(string groupName)
        {
            await ChangeGroup(groupName);
        }

        [When(@"Delete existing group")]
        public async Task DeleteExistingGroup()
        {
            await DeleteGroup(FakeGroupRepository.ReadAll().ToList().IndexOf(FakeGroup));
        }

        [Then(@"The number of groups is equal to (.*)")]
        public void CheckNumberOfGroup(int count)
        {
            Assert.AreEqual(FakeGroupRepository.ReadAll().Count(), count);
        }

        [Then(@"The database contains a group with the name (.*)")]
        public void CheckGroupInExistingOnes(string groupName)
        {
            CollectionAssert.Contains(FakeGroupRepository.ReadAll().ToList(), new Group { Name = groupName, Course = FakeCourse });
        }

        [Then(@"The database not contains a groups")]
        public void CheckCourseInNotExistingOnes()
        {
            CollectionAssert.DoesNotContain(FakeGroupRepository.ReadAll().ToList(), FakeGroup);
        }
    }
}
