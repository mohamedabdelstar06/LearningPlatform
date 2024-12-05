using LearningPlatform.BLL.ViewModels;
using LearningPlatform.DAL.Entities;
using LearningPlatform.DAL.Repository.Abstraction;
using LearningPlatform.DAL.Repository.Impelementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.mvc.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

		public HomeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Courses()
		{
			var categories = _unitOfWork.Category.GetAll();
			var courses = _unitOfWork.Course.GetAll();
			var viewModel = new CourseViewModel
			{
				Categories = categories,
				Courses = courses
			};
			
			return View(viewModel);
		}
		public IActionResult Index()
		{
			var categories = _unitOfWork.Category.GetAll();
			var courses = _unitOfWork.Course.GetAll().Take(8);
			var trendingCourse = _unitOfWork.Course.TrendingCourse();
			var viewModel = new CourseViewModel
			{
				Categories = categories,
				Courses = courses,
				TrendingCourse = trendingCourse
			};
			return View(viewModel);
		}
		public IActionResult Details(int id)
		{
			var result = _unitOfWork.Course.GetFirstorDefault(x => x.Id == id, Includeword: "Category");

			return View(result);
		}
        public IActionResult Category(int categoryId)
		{
			var courses = _unitOfWork.Course.GetAll(x => x.CategoryId == categoryId).Select(item => new
			{
				item.Id,
				item.CourseName,
				item.CourseDescription,
				item.Image,
				item.CourseTime,
				item.TotalLecture,
				item.Price,
				Level = item.Level.ToString(),
				ImageUrl = Url.Content("/" + item.Image) 
			});
			return Json(courses);
		}
		public IActionResult GetAllCourses()
		{
			var allcourses= _unitOfWork.Course.GetAll().Take(8).Select(item => new
			{
				item.Id,
				item.CourseName,
				item.CourseDescription,
				item.Image,
				item.CourseTime,
				item.TotalLecture,
				item.Price,
				Level = item.Level.ToString(),
				ImageUrl = Url.Content("/" + item.Image)
			});
			return Json(allcourses);
		}

	}
}
