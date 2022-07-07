using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using UniversityIntegrationTests;
using Model;
using Microsoft.EntityFrameworkCore;

namespace University.Repositories.IntegrationTests
{
    [TestClass()]
    public class StudentRepositoryTests : BaseIntegrationTestDB
    {
        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            Student expectedStudent = FakeStudent;

            // Act
            StudentRepository.Create(FakeStudent);

            Context.SaveChanges();

            // Assert          
            Student actualStudent = Context.Students.Include(c => c.Group.Course).FirstOrDefault();
            Assert.AreEqual(expectedStudent, actualStudent);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            Context.Students.Add(FakeStudent);
            Context.SaveChanges();
            int studentId = Context.Students.FirstOrDefault().Id;

            // Act
            StudentRepository.Delete(studentId);
            Context.SaveChanges();

            // Assert       
            Assert.IsNull(Context.Students.FirstOrDefault());
        }

        [TestMethod()]
        public void ReadTest()
        {
            // Arrange
            Student expectedStudent = FakeStudent;

            Context.Students.Add(FakeStudent);
            Context.SaveChanges();

            int studentId = Context.Students.FirstOrDefault().Id;

            // Act
            Student actualStudent = StudentRepository.Read(studentId);

            // Assert
            Assert.AreEqual(expectedStudent, actualStudent);
        }

        [TestMethod()]
        public void ReadAllTest_without_parametr()
        {
            // Arrange
            List<Student> expectedStudents = FakeStudents.Select(s => new Student { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, Group = FakeGroup }).ToList();
            Context.Students.AddRange(expectedStudents);
            Context.SaveChanges();

            // Act
            List<Student> actualStudents = StudentRepository.ReadAll().ToList();

            // Assert
            CollectionAssert.AreEqual(expectedStudents, actualStudents);
        }

        [TestMethod()]
        public void ReadAllTest_One_Parametr()
        {
            // Arrange
            List<Student> expectedStudents = FakeStudents.Select(s => new Student { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, Group = FakeGroup }).ToList();
            Context.Students.AddRange(expectedStudents);
            Context.SaveChanges();

            int groupId = FakeGroup.Id;
            // Act
            List<Student> actualStudents = StudentRepository.ReadAll(groupId).ToList();

            // Assert
            CollectionAssert.AreEqual(expectedStudents, actualStudents);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange                        
            Context.Students.Add(FakeStudent);

            Context.SaveChanges();

            Student expectedStudent = Context.Students.Find(FakeStudent.Id);
            expectedStudent.FirstName = "Test";

            // Act
            StudentRepository.Update(expectedStudent);
            Context.SaveChanges();

            // Assert          
            Student actualStudent = Context.Students.Include(c => c.Group.Course).FirstOrDefault();
            Assert.AreEqual(expectedStudent, actualStudent);
        }
    }
}