using Infrastructure.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using UniversityIntegrationTests.FakeData;

namespace UniversityIntegrationTests.Fakes
{
    public class FakeGroupRepository : IRepository<Group>
    {
        private readonly List<Group> _groups;
        public FakeGroupRepository(FakeGroupData fakeGroupData)
        {
            _groups = fakeGroupData.Valid.Generate(10);
        }
        public void Create(Group item)
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

        public Group Read(int id)
        {
            return _groups[id];
        }

        public IEnumerable<Group> ReadAll()
        {
            return _groups;
        }

        public IEnumerable<Group> ReadAll(int id)
        {
            return _groups;
        }

        public void Update(Group item)
        {
            throw new NotImplementedException();
        }
    }
}
