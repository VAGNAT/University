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
    public class StudentTest : BaseFunctionalTest
    {
        private const string ControllerBaseAddress = "/Student/";

        [TestMethod]
        public async Task Given_OneExistingStudent_OneNewStudent_When_AddNewStudent_Then_TwoExistingStudent_NewStudentInExistingOnes()
        {
            const string studentFirstName = "Alice";
            const string studentLastName = "Moore";
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            // Given
            FakeGroupRepository.Create(FakeGroup);
            FakeStudentRepository.Create(FakeStudent);

            // When
            Student student = await AddStudent(studentFirstName, studentLastName);

            // Then
            Assert.AreEqual(FakeStudentRepository.ReadAll().Count(), 2);
            CollectionAssert.Contains(FakeStudentRepository.ReadAll().ToList(), student);
        }

        [TestMethod]
        public async Task Given_OneExistingStudents_NewFirstNameStudent_NewLastNameStudent_When_ChangeFirstNameAndChangeLastName_Then_OneExistingStudents_StudentWithNewFirstNameAndNewLastNameInExistingOnes()
        {
            const string newStudentFirstName = "Alice";
            const string newStudentLastName = "Moore";
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            // Given
            FakeGroupRepository.Create(FakeGroup);
            FakeStudentRepository.Create(FakeStudent);

            // When
            Student student = await ChangeStudent(newStudentFirstName, newStudentLastName);

            // Then
            Assert.AreEqual(FakeStudentRepository.ReadAll().Count(), 1);
            CollectionAssert.Contains(FakeStudentRepository.ReadAll().ToList(), student);
        }

        [TestMethod]
        public async Task Given_OneExistingStudents_When_DeleteExistingStudent_Then_ZeroExistingStudents_RemoveStudentIsNotInExistingOnes()
        {
            HttpClient.BaseAddress = new Uri(HttpClient.BaseAddress, ControllerBaseAddress);

            // Given
            FakeStudentRepository.Create(FakeStudent);

            // When
            await DeleteStudent(FakeStudentRepository.ReadAll().ToList().IndexOf(FakeStudent));

            // Then
            Assert.AreEqual(FakeStudentRepository.ReadAll().Count(), 0);
            CollectionAssert.DoesNotContain(FakeStudentRepository.ReadAll().ToList(), FakeStudent);
        }

        private async Task<Student> AddStudent(string studentFirstName, string studentLastName)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Student.FirstName", studentFirstName),
             new KeyValuePair<string, string>("Student.LastName", studentLastName),
             new KeyValuePair<string, string>("GroupId", "0"),
             new KeyValuePair<string, string>("GroupName", FakeGroup.Name)
        });

            await HttpClient.PostAsync($"AddStudent", content);
            return new Student { FirstName = studentFirstName, LastName = studentLastName, Group = FakeGroup };
        }

        private async Task<Student> ChangeStudent(string studentFirstName, string studentLastName)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("Student.FirstName", studentFirstName),
             new KeyValuePair<string, string>("Student.LastName", studentLastName),
             new KeyValuePair<string, string>("GroupId", "0"),
             new KeyValuePair<string, string>("GroupName", FakeGroup.Name)
        });

            await HttpClient.PostAsync($"ChangeStudent", content);
            return new Student { FirstName = studentFirstName, LastName = studentLastName, Group = FakeGroup };
        }

        private async Task DeleteStudent(int studentId)
        {
            await HttpClient.GetAsync($"DeleteStudent/{studentId}");
        }
    }
}