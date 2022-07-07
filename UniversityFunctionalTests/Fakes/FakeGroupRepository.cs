using Infrastructure.Interfaces;
using Model;
using System.Collections.Generic;

namespace UniversityFunctionalTests.Fakes
{
    public class FakeGroupRepository : IRepository<Group>
    {
        private readonly List<Group> _groups = new List<Group>();
        
        public void Create(Group item)
        {
            _groups.Add(item);
        }

        public void Delete(int id)
        {
            _groups.Remove(_groups[id]);
        }

        public bool Empty(int id)
        {
            return true;
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
            _groups.Clear();
            _groups.Add(item);
        }
    }
}
