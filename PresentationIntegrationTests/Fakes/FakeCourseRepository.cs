using Infrastructure.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using UniversityIntegrationTests.FakeData;

namespace UniversityIntegrationTests.Fakes
{
    public class FakeCourseRepository : IRepository<Course>
    {
        private readonly List<Course> _courses;
        public FakeCourseRepository(FakeCourseData fakeCourseData)
        {
            _courses = fakeCourseData.Valid.Generate(10);
        }
        public void Create(Course item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Empty(int id)
        {
            throw new NotImplementedException();
        }

        public Course Read(int id)
        {
            return _courses[id];
        }

        public IEnumerable<Course> ReadAll()
        {
            return _courses;
        }

        public IEnumerable<Course> ReadAll(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Course item)
        {
            throw new NotImplementedException();
        }
    }
}
