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
    public class GroupTest : BaseFunctionalTest
    {
        private const string ControllerBaseAddress = "/Group/";

        [TestMethod]
        public async Task Given_OneExistingGroup_OneNewGroup_When_AddNewGroup_Then_TwoExistingGroup_NewGroupInExistingOnes()
        {
            const string groupName = "SR-20";
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            // Given
            FakeCourseRepository.Create(FakeCourse);
            FakeGroupRepository.Create(FakeGroup);

            // When
            Group group = await AddGroup(groupName);

            // Then
            Assert.AreEqual(FakeGroupRepository.ReadAll().Count(), 2);
            CollectionAssert.Contains(FakeGroupRepository.ReadAll().ToList(), group);
        }

        [TestMethod]
        public async Task Given_OneExistingGroups_NewNameGroup_When_ChangeName_Then_OneExistingGroups_GroupWithNewNameInExistingOnes()
        {
            const string newGroupName = "SR-20";
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            // Given
            FakeCourseRepository.Create(FakeCourse);
            FakeGroupRepository.Create(FakeGroup);

            // When
            Group group = await ChangeGroup(newGroupName);

            // Then
            Assert.AreEqual(FakeGroupRepository.ReadAll().Count(), 1);
            CollectionAssert.Contains(FakeGroupRepository.ReadAll().ToList(), group);
        }

        [TestMethod]
        public async Task Given_OneExistingGroups_When_DeleteExistingGroup_Then_ZeroExistingGroups_RemoveGroupIsNotInExistingOnes()
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            // Given
            FakeGroupRepository.Create(FakeGroup);

            // When
            await DeleteGroup(FakeGroupRepository.ReadAll().ToList().IndexOf(FakeGroup));

            // Then
            Assert.AreEqual(FakeGroupRepository.ReadAll().Count(), 0);
            CollectionAssert.DoesNotContain(FakeGroupRepository.ReadAll().ToList(), FakeGroup);
        }

        private async Task<Group> AddGroup(string groupName)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Group.Name", groupName),
             new KeyValuePair<string, string>("CourseId", "0"),
             new KeyValuePair<string, string>("CourseName", FakeCourse.Name)
        });

            await HttpClient.PostAsync($"AddGroup", content);
            return new Group { Name = groupName, Course = FakeCourse };
        }

        private async Task<Group> ChangeGroup(string groupName)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Group.Name", groupName),
             new KeyValuePair<string, string>("CourseId", "0"),
             new KeyValuePair<string, string>("CourseName", FakeCourse.Name)
        });

            await HttpClient.PostAsync($"ChangeGroup", content);
            return new Group { Name = groupName, Course = FakeCourse };
        }

        private async Task DeleteGroup(int groupId)
        {
            await HttpClient.GetAsync($"DeleteGroup/{groupId}");
        }
    }
}