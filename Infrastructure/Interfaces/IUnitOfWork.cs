using Model;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Course> Courses { get; }
        IRepository<Group> Groups { get; }
        IRepository<Student> Students { get; }
        void Dispose();
        void Save();
    }
}
