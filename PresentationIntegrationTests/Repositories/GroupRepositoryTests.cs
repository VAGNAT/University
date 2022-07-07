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
    public class GroupRepositoryTests : BaseIntegrationTestDB
    {
        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            Group expectedGroup = FakeGroup;

            // Act
            GroupRepository.Create(FakeGroup);

            Context.SaveChanges();

            // Assert          
            Group actualGroup = Context.Groups.Include(c => c.Course).FirstOrDefault();
            Assert.AreEqual(expectedGroup, actualGroup);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            Context.Groups.Add(FakeGroup);
            Context.SaveChanges();
            int groupId = Context.Groups.FirstOrDefault().Id;

            // Act
            GroupRepository.Delete(groupId);
            Context.SaveChanges();

            // Assert       
            Assert.IsNull(Context.Groups.FirstOrDefault());
        }

        [TestMethod()]
        public void EmptyGroupTest()
        {
            // Arrange
            Context.Groups.Add(FakeGroup);
            Context.SaveChanges();
            int groupId = Context.Groups.FirstOrDefault().Id;

            // Act
            bool actual = GroupRepository.Empty(groupId);

            // Assert       
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void NotEmptyCourseTest()
        {
            // Arrange
            Context.Groups.Add(FakeGroup);
            Context.Students.Add(FakeStudent);
            Context.SaveChanges();

            int groupId = Context.Groups.FirstOrDefault().Id;

            // Act
            bool actual = GroupRepository.Empty(groupId);

            // Assert       
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void ReadTest()
        {
            // Arrange
            Group expectedGroup = FakeGroup;

            Context.Groups.Add(FakeGroup);
            Context.SaveChanges();

            int groupId = Context.Groups.FirstOrDefault().Id;

            // Act
            Group actualGroup = GroupRepository.Read(groupId);

            // Assert
            Assert.AreEqual(expectedGroup, actualGroup);
        }

        [TestMethod()]
        public void ReadAllTest_without_parametr()
        {
            // Arrange
            List<Group> expectedGroups = FakeGroups.Select(s=>new Group { Id = s.Id, Name = s.Name, Course = FakeCourse }).ToList();
            Context.Groups.AddRange(expectedGroups);
            Context.SaveChanges();

            // Act
            List<Group> actualGroups = GroupRepository.ReadAll().ToList();

            // Assert
            CollectionAssert.AreEqual(expectedGroups, actualGroups);
        }

        [TestMethod()]
        public void ReadAllTest_One_Parametr()
        {
            // Arrange
            List<Group> expectedGroups = FakeGroups.Select(s => new Group { Id = s.Id, Name = s.Name, Course = FakeCourse }).ToList();
            Context.Groups.AddRange(expectedGroups);
            Context.SaveChanges();

            int courseId = FakeCourse.Id;

            // Act
            List<Group> actualGroups = GroupRepository.ReadAll(courseId).ToList();

            // Assert
            CollectionAssert.AreEqual(expectedGroups, actualGroups);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange                        
            Context.Groups.Add(FakeGroup);

            Context.SaveChanges();

            Group expectedGroup = Context.Groups.Find(FakeGroup.Id);
            expectedGroup.Name = "Test";

            // Act
            GroupRepository.Update(expectedGroup);
            Context.SaveChanges();

            // Assert          
            Group actualGroup = Context.Groups.Include(c => c.Course).FirstOrDefault();
            Assert.AreEqual(expectedGroup, actualGroup);
        }
    }
}