using Infrastructure.Interfaces;
using Model;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        private readonly UniversityContext _context;
        public GroupRepository(UniversityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Create(Group item)
        {
            _context.Groups.Add(item);
        }

        public void Delete(int id)
        {
            Group group = _context.Groups.Find(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
            }
        }

        public bool Empty(int id)
        {
            return !_context.Students.Any(s => s.Group.Id == id);
        }

        public Group? Read(int id)
        {
            return _context.Groups.Where(g => g.Id == id).Include(g => g.Course).FirstOrDefault();
        }

        public IEnumerable<Group> ReadAll()
        {
            return _context.Groups.Include(g => g.Course);
        }

        public IEnumerable<Group> ReadAll(int idCourse)
        {
            return _context.Groups.Include(g => g.Course).Where(g => g.Course.Id == idCourse);
        }

        public void Update(Group item)
        {
            _context.Groups.Update(item);
        }
    }
}
