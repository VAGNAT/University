using Infrastructure.Interfaces;
using Model;
using Infrastructure.EF;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversityContext _context;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Student> _studentRepository;
        public UnitOfWork(UniversityContext context, IRepository<Course> courseRepository, IRepository<Group> groupRepository, IRepository<Student> studentRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
        }
        public IRepository<Course> Courses => _courseRepository;

        public IRepository<Group> Groups => _groupRepository;

        public IRepository<Student> Students => _studentRepository;

        public void Dispose() => _context.Dispose();

        public void Save() => _context.SaveChanges();
    }
}
