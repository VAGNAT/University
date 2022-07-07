using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using University.IntegrationTests;

namespace University.Controller.IntegrationTests
{
    [TestClass()]
    public class StudentControllerTests: BaseIntegrationTestView
    {
        private const string ControllerBaseAddress = "/Student/";
        [TestMethod()]
        public async Task AllStudentsTest_View()
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            var expectedStudents = FakeStudentRepository.ReadAll().Select(x => new
            {
                FirstName = x.FirstName.ToString(),
                LastName = x.LastName.ToString(),
            }).ToArray();

            foreach (int groupId in Enumerable.Range(0, 9))
            {
                string expectedGroup = FakeGroupRepository.Read(groupId).Name;

                //act
                string response = await HttpClient.GetStringAsync($"AllStudents?groupId={groupId}");

                //assert         
                IHtmlDocument document = Parser.ParseDocument(response);

                IElement table = document.QuerySelector("#student");

                var actualGroups = table.QuerySelectorAll("tr").Select(tr =>
                {
                    IHtmlCollection<IElement> tds = tr.Children;

                    string firstName = tds[1].TextContent.Trim(' ', '\r', '\n');
                    string lastName = tds[2].TextContent.Trim(' ', '\r', '\n');

                    return new
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };
                }).ToList();

                string actualgroupViewBag = document.QuerySelector("#group").TextContent;

                CollectionAssert.AreEquivalent(expectedStudents, actualGroups);
                Assert.AreEqual(expectedGroup, actualgroupViewBag);
            }
        }

        [TestMethod()]
        public async Task AddStudentTest_View()
        {
            //arrange
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            foreach (int groupId in Enumerable.Range(0, 9))
            {
                string expectedGroup = FakeGroupRepository.Read(groupId).Name;

                //act
                string response = await HttpClient.GetStringAsync($"AddStudent?groupId={groupId}");

                //assert
                IHtmlDocument document = Parser.ParseDocument(response);

                string actualGroup = document.QuerySelector("#InputGroupName").GetAttribute("value");

                Assert.AreEqual(expectedGroup, actualGroup);
            }
        }

        [TestMethod()]
        public async Task ChangeStudentTest_View()
        {
            //arrange
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            foreach (int studentId in Enumerable.Range(0, 9))
            {
                foreach (int groupId in Enumerable.Range(0, 9))
                {
                    Student fakeStudent = FakeStudentRepository.Read(studentId);
                    var expectedStudent = new { FirstName = fakeStudent.FirstName, LastName = fakeStudent.LastName };
                    string expectedGroup = FakeGroupRepository.Read(groupId).Name;

                    //act
                    string response = await HttpClient.GetStringAsync($"ChangeStudent?studentId={studentId}&groupId={groupId}");

                    //assert
                    IHtmlDocument document = Parser.ParseDocument(response);

                    string actualGroup = document.QuerySelector("#InputGroupName").GetAttribute("value");
                    var actualStudent = new
                    {
                        FirstName = document.QuerySelector("#InputFirstName").GetAttribute("value"),
                        LastName = document.QuerySelector("#InputLastName").GetAttribute("value")
                    };

                    Assert.AreEqual(expectedStudent, actualStudent);
                    Assert.AreEqual(expectedGroup, actualGroup);
                }
            }
        }
    }
}