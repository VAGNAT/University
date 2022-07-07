using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;
using Services.ViewModel;

namespace Presentation.Controllers
{
    public class StudentController : Controller
    {
        private readonly ICRUD<Group> _groupService;
        private readonly ICRUD<Student> _studentService;
        private readonly IOperationsViewModel _operationsViewModel;

        public StudentController(ICRUD<Group> groupService, ICRUD<Student> studentService, IOperationsViewModel operationsViewModel)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _operationsViewModel = operationsViewModel ?? throw new ArgumentNullException(nameof(_operationsViewModel));
        }

        public IActionResult AllStudents(int groupId)
        {
            ViewBag.Group = _groupService.Get(groupId);
            return View(_studentService.GetAll(groupId));
        }

        public IActionResult AddStudent(int groupId)
        {
            return View(_operationsViewModel.GetNewStudentViewModel(groupId));
        }

        [HttpPost]
        public IActionResult AddStudent(StudentViewModel studentViewModel)
        {
            Student student = _operationsViewModel.GetStudentFromStudentViewModel(studentViewModel);
            _studentService.Create(student);

            ViewBag.Group = student.Group;
            return View("AllStudents", _studentService.GetAll(student.Group.Id));
        }

        public IActionResult ChangeStudent(int studentId, int groupId)
        {
            return View(_operationsViewModel.GetNewStudentViewModel(groupId, studentId));
        }

        [HttpPost]
        public IActionResult ChangeStudent(StudentViewModel studentViewModel)
        {
            Student student = _operationsViewModel.GetStudentFromStudentViewModel(studentViewModel);
            _studentService.Update(student);

            ViewBag.Group = student.Group;
            return View("AllStudents", _studentService.GetAll(student.Group.Id));
        }

        public IActionResult DeleteStudent(int studentId, int groupId)
        {
            _studentService.Delete(studentId);
            Group group = _groupService.Get(groupId);

            ViewBag.Group = group;
            return View("AllStudents", _studentService.GetAll(group.Id));
        }
    }
}
