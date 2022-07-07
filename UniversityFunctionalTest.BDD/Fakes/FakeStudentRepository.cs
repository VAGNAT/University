using Infrastructure.Interfaces;
using Model;
using System.Collections.Generic;

namespace UniversityFunctionalTestsBDD.Fakes
{
    public class FakeStudentRepository : IRepository<Student>
    {
        private readonly List<Student> _students = new List<Student>();
       
        public void Create(Student item)
        {
            _students.Add(item);
        }

        public void Delete(int id)
        {
            _students.Remove(_students[id]);
        }

        public bool Empty(int id)
        {
            return true;
        }

        public Student Read(int id)
        {
            return _students[id];
        }

        public IEnumerable<Student> ReadAll()
        {
            return _students;
        }

        public IEnumerable<Student> ReadAll(int id)
        {
            return _students;
        }

        public void Update(Student item)
        {
            _students.Clear();
            _students.Add(item);
        }
    }
}
