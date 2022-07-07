using Model;
using Services.ViewModel;

namespace Services.Interfaces
{
    public interface IOperationsViewModel
    {
        GroupViewModel GetNewGroupViewModel(int courseId);
        GroupViewModel GetNewGroupViewModel(int courseId, int groupId);
        Group GetGroupFromGroupViewModel(GroupViewModel groupViewModel);
        StudentViewModel GetNewStudentViewModel(int groupId);
        StudentViewModel GetNewStudentViewModel(int groupId, int studentId);
        Student GetStudentFromStudentViewModel(StudentViewModel studentViewModel);
    }
}
