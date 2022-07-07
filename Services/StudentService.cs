using Model;
using Services.Interfaces;
using Infrastructure.Interfaces;

namespace Services
{
    public class StudentService : ICRUD<Student>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public void Create(Student item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }
            _unitOfWork.Students.Create(item);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Students.Delete(id);
            _unitOfWork.Save();
        }

        public Student Get(int id)
        {
            return _unitOfWork.Students.Read(id);
        }

        public IEnumerable<Student> GetAll(int idGroup = default)
        {
            if (idGroup == default)
            {
                return _unitOfWork.Students.ReadAll();
            }
            return _unitOfWork.Students.ReadAll(idGroup);
        }

        public void Update(Student item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }
            _unitOfWork.Students.Update(item);
            _unitOfWork.Save();
        }
    }
}
