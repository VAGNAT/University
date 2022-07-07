using Infrastructure.Interfaces;
using Model;
using Services.Interfaces;

namespace Services
{
    public class GroupService : ICRUD<Group>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public void Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }
            _unitOfWork.Groups.Create(item);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            if (_unitOfWork.Groups.Empty(id))
            {
                _unitOfWork.Groups.Delete(id);
                _unitOfWork.Save();
            }
        }

        public Group Get(int id)
        {
            return _unitOfWork.Groups.Read(id);
        }

        public IEnumerable<Group> GetAll(int idCourse = default)
        {
            if (idCourse == default)
            {
                return _unitOfWork.Groups.ReadAll();
            }
            return _unitOfWork.Groups.ReadAll(idCourse);
        }

        public void Update(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }
            _unitOfWork.Groups.Update(item);
            _unitOfWork.Save();
        }
    }
}
