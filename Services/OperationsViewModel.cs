using Model;
using Services.Interfaces;
using Services.ViewModel;

namespace Services
{
    public class OperationsViewModel : IOperationsViewModel
    {
        private readonly ICRUD<Course> _courseService;
        private readonly ICRUD<Group> _groupService;
        private readonly ICRUD<Student> _studentService;
        public OperationsViewModel(ICRUD<Course> courseService, ICRUD<Group> groupService, ICRUD<Student> studentService)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }
        public Group GetGroupFromGroupViewModel(GroupViewModel groupViewModel)
        {
            Group group = groupViewModel.Group ?? throw new ArgumentNullException(nameof(groupViewModel));
            group.Course = _courseService.Get(groupViewModel.CourseId);
            return group;
        }

        public GroupViewModel GetNewGroupViewModel(int courseId, int groupId)
        {
            Course course = _courseService.Get(courseId);
            return new GroupViewModel { Group = _groupService.Get(groupId), CourseId = course.Id, CourseName = course.Name };
        }

        public GroupViewModel GetNewGroupViewModel(int courseId)
        {
            Course course = _courseService.Get(courseId);
            return new GroupViewModel { Group = new Group(), CourseId = course.Id, CourseName = course.Name };
        }

        public StudentViewModel GetNewStudentViewModel(int groupId)
        {
            Group group = _groupService.Get(groupId);
            return new StudentViewModel { Student = new Student(), GroupId = group.Id, GroupName = group.Name };
        }

        public StudentViewModel GetNewStudentViewModel(int groupId, int studentId)
        {
            Group group = _groupService.Get(groupId);
            return new StudentViewModel { Student = _studentService.Get(studentId), GroupId = group.Id, GroupName = group.Name };
        }

        public Student GetStudentFromStudentViewModel(StudentViewModel studentViewModel)
        {
            Student student = studentViewModel.Student ?? throw new ArgumentNullException(nameof(studentViewModel));
            student.Group = _groupService.Get(studentViewModel.GroupId);
            return student;
        }
    }
}
