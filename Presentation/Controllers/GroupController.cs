using Microsoft.AspNetCore.Mvc;
using Model;
using Services.ViewModel;
using Services.Interfaces;

namespace Presentation.Controllers
{
    public class GroupController : Controller
    {
        private readonly ICRUD<Group> _groupService;
        private readonly ICRUD<Course> _courseService;
        private readonly IOperationsViewModel _operationsViewModel;
        public GroupController(ICRUD<Course> courseService, ICRUD<Group> groupService, IOperationsViewModel operationsViewModel)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _operationsViewModel = operationsViewModel ?? throw new ArgumentNullException(nameof(operationsViewModel));
        }
        public IActionResult AllGroups(int courseId)
        {
            ViewBag.Course = _courseService.Get(courseId);
            return View(_groupService.GetAll(courseId));
        }
        public IActionResult AddGroup(int courseId)
        {       
            return View(_operationsViewModel.GetNewGroupViewModel(courseId));
        }

        [HttpPost]
        public IActionResult AddGroup(GroupViewModel groupViewModel)
        {

            Group group = _operationsViewModel.GetGroupFromGroupViewModel(groupViewModel);
            _groupService.Create(group);

            ViewBag.Course = group.Course;
            return View("AllGroups", _groupService.GetAll(group.Course.Id));
        }

        public IActionResult ChangeGroup(int groupId, int courseId)
        {
            return View(_operationsViewModel.GetNewGroupViewModel(courseId, groupId));
        }

        [HttpPost]
        public IActionResult ChangeGroup(GroupViewModel groupViewModel)
        {
            Group group = _operationsViewModel.GetGroupFromGroupViewModel(groupViewModel);
            _groupService.Update(group);

            ViewBag.Course = group.Course;
            return View("AllGroups", _groupService.GetAll(group.Course.Id));
        }

        public IActionResult DeleteGroup(int groupId, int courseId)
        {
            _groupService.Delete(groupId);

            Course course = _courseService.Get(courseId);
            ViewBag.Course = course;
            return View("AllGroups", model: _groupService.GetAll(course.Id));
        }
    }
}
