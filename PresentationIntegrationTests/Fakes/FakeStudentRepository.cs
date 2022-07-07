using Infrastructure.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using UniversityIntegrationTests.FakeData;

namespace UniversityIntegrationTests.Fakes
{
    public class FakeStudentRepository : IRepository<Student>
    {
        private readonly List<Student> _students;
        public FakeStudentRepository(FakeStudentData fakeStudentData)
        {
            _students = fakeStudentData.Valid.Generate(10);
        }
        public void Create(Student item)
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
            throw new NotImplementedException();
        }
    }
}
