using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using University.IntegrationTests;
using UniversityIntegrationTests.Fakes;

namespace University.Controller.IntegrationTests
{

    [TestClass()]
    public class CourseControllerTests : BaseIntegrationTestView
    {
        private const string ControllerBaseAddress = "/Course/";

        [TestMethod()]
        public async Task AllCoursesTestView()
        {
            //arrange
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            var expectedCourses = FakeCourseRepository.ReadAll().Select(x => new
            {
                Name = x.Name.ToString(),
                Description = x.Description.ToString(),
            }).ToArray();

            //act
            var response = await HttpClient.GetStringAsync("AllCourses");

            //assert         
            IHtmlDocument document = Parser.ParseDocument(response);

            IElement table = document.QuerySelector("#course");

            var actualCourses = table.QuerySelectorAll("tr").Select(tr =>
                {
                    var tds = tr.Children;

                    var name = tds[1].TextContent.Trim(' ', '\r', '\n');
                    var description = tds[2].TextContent.Trim(' ', '\r', '\n');

                    return new
                    {
                        Name = name,
                        Description = description,
                    };
                }).ToList();

            CollectionAssert.AreEquivalent(expectedCourses, actualCourses);
        }

        [TestMethod()]
        public async Task ChangeCourseTestView()
        {
            //arrange
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            foreach (int courseId in Enumerable.Range(0,9))
            {
                Course fakeCourse = FakeCourseRepository.Read(courseId);
                var expectedCourse = new { Name = fakeCourse.Name, Description = fakeCourse.Description };

                //act
                var response = await HttpClient.GetStringAsync($"ChangeCourse/{courseId}");

                //assert         
                IHtmlDocument document = Parser.ParseDocument(response);

                var actualCourse = new
                {
                    Name = document.QuerySelector("#InputName").GetAttribute("value").Trim(' ', '\r', '\n'),
                    Description = document.QuerySelector("#InputDescription").TextContent.Trim(' ', '\r', '\n'),
                };

                Assert.AreEqual(expectedCourse, actualCourse);
            }           
        }
    }
}