using Infrastructure.Interfaces;
using Model;
using Infrastructure.EF;


namespace Infrastructure.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly UniversityContext _context;

        public CourseRepository(UniversityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Create(Course item)
        {
            _context.Courses.Add(item);
        }

        public void Delete(int id)
        {
            Course course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
        }

        public bool Empty(int id)
        {
            return !_context.Groups.Any(g => g.Course.Id == id);
        }

        public Course Read(int id)
        {
            return _context.Courses.Find(id);
        }

        public IEnumerable<Course> ReadAll()
        {
            return _context.Courses;
        }

        public IEnumerable<Course> ReadAll(int id)
        {
            return _context.Courses;
        }
                
        public void Update(Course item)
        {
            _context.Courses.Update(item);
        }
    }
}
