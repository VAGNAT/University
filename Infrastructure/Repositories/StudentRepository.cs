using Model;
using Infrastructure.Interfaces;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly UniversityContext _context;
        public StudentRepository(UniversityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Create(Student item)
        {
            _context.Students.Add(item);
        }

        public void Delete(int id)
        {
            Student student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
        }

        public bool Empty(int id)
        {
            return true;
        }

        public Student Read(int id)
        {
            return _context.Students.Find(id);
        }

        public IEnumerable<Student> ReadAll()
        {
            return _context.Students.Include(s => s.Group.Course);
        }

        public IEnumerable<Student> ReadAll(int idGroup)
        {
            return _context.Students.Include(s => s.Group.Course).Where(s => s.Group.Id == idGroup);
        }

        public void Update(Student item)
        {
            _context.Students.Update(item);
        }
    }
}
