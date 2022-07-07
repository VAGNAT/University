using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using University.IntegrationTests;

namespace University.Controller.IntegrationTests
{
    [TestClass()]
    public class GroupControllerTests : BaseIntegrationTestView
    {
        private const string ControllerBaseAddress = "/Group/";
        [TestMethod()]
        public async Task AllGroupsTest_View()
        {
            //arrange
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            var expectedGroups = FakeGroupRepository.ReadAll().Select(x => new
            {
                Name = x.Name.ToString()
            }).ToArray();

            foreach (int courseId in Enumerable.Range(0, 9))
            {
                string expectedCourse = FakeCourseRepository.Read(courseId).Name;

                //act
                string response = await HttpClient.GetStringAsync($"AllGroups?courseId={courseId}");

                //assert         
                IHtmlDocument document = Parser.ParseDocument(response);

                IElement table = document.QuerySelector("#group");

                var actualGroups = table.QuerySelectorAll("tr").Select(tr =>
                {
                    IHtmlCollection<IElement> tds = tr.Children;

                    string name = tds[1].TextContent.Trim(' ', '\r', '\n');

                    return new
                    {
                        Name = name
                    };
                }).ToList();

                string actualCourseViewData = document.QuerySelector("#course").TextContent;

                CollectionAssert.AreEquivalent(expectedGroups, actualGroups);
                Assert.AreEqual(expectedCourse, actualCourseViewData);
            }
        }

        [TestMethod()]
        public async Task AddGroupTest_View()
        {
            //arrange
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            foreach (int courseId in Enumerable.Range(0, 9))
            {
                string expectedCourse = FakeCourseRepository.Read(courseId).Name;

                //act
                string response = await HttpClient.GetStringAsync($"AddGroup?courseId={courseId}");

                //assert
                IHtmlDocument document = Parser.ParseDocument(response);

                string actualCourse = document.QuerySelector("#InputCourseName").GetAttribute("value");
                Assert.AreEqual(expectedCourse, actualCourse);
            }
        }

        [TestMethod()]
        public async Task ChangeGroupTest_View()
        {
            //arrange
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);
            
            foreach (int courseId in Enumerable.Range(0, 9))
            {
                foreach (int groupId in Enumerable.Range(0, 9))
                {

                    string expectedCourse = FakeCourseRepository.Read(courseId).Name;
                    string expectedGroup = FakeGroupRepository.Read(groupId).Name;

                    //act
                    string response = await HttpClient.GetStringAsync($"ChangeGroup?groupId={groupId}&courseId={courseId}");

                    //assert
                    IHtmlDocument document = Parser.ParseDocument(response);

                    string actualGroup = document.QuerySelector("#InputGroupName").GetAttribute("value");
                    string actualCourse = document.QuerySelector("#InputCourseName").GetAttribute("value");

                    Assert.AreEqual(expectedCourse, actualCourse);
                    Assert.AreEqual(expectedGroup, actualGroup);
                }
            }
        }
    }
}