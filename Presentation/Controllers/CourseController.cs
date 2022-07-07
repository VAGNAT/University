using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

namespace Presentation.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICRUD<Course> _courseService;

        public CourseController(ICRUD<Course> courseService)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
        }

        public IActionResult AllCourses() => View(_courseService.GetAll());

        public IActionResult AddCourse() => View();

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            if (course is null)
            {
                return BadRequest();
            }
            _courseService.Create(course);

            return View("AllCourses", _courseService.GetAll());
        }

        public IActionResult ChangeCourse(int id)
        {
            return View(_courseService.Get(id));
        }

        [HttpPost]
        public IActionResult ChangeCourse(Course course)
        {
            if (course is null)
            {
                return BadRequest();
            }
            _courseService.Update(course);

            return View("AllCourses", _courseService.GetAll());
        }

        public IActionResult DeleteCourse(int id)
        {
            _courseService.Delete(id);

            return View("AllCourses", _courseService.GetAll());
        }
    }
}
