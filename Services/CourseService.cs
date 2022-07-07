using Model;
using Services.Interfaces;
using Infrastructure.Interfaces;

namespace Services
{
    public class CourseService : ICRUD<Course>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public void Create(Course item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }
            _unitOfWork.Courses.Create(item);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            if (_unitOfWork.Courses.Empty(id))
            {
                _unitOfWork.Courses.Delete(id);
                _unitOfWork.Save();
            }
        }

        public Course Get(int id)
        {
            return _unitOfWork.Courses.Read(id);
        }

        public IEnumerable<Course> GetAll(int id = default)
        {
            return _unitOfWork.Courses.ReadAll();
        }

        public void Update(Course item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }
            _unitOfWork.Courses.Update(item);
            _unitOfWork.Save();
        }
    }
}