using Infrastructure.Interfaces;
using Model;
using System;
using System.Collections.Generic;

namespace UniversityFunctionalTestsBDD.Fakes
{
    public class FakeCourseRepository : IRepository<Course>
    {
        private readonly List<Course> _courses = new List<Course>();
        
        public void Create(Course item)
        {
            _courses.Add(item);
        }

        public void Delete(int id)
        {
            _courses.Remove(_courses[id]);
        }

        public bool Empty(int id)
        {
            return true;
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
            _courses.Clear();
            _courses.Add(item);            
        }
    }
}
