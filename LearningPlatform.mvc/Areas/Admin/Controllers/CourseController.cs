using LearningPlatform.BLL.ViewModels;
using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Entities;
using LearningPlatform.DAL.Repository.Abstraction;
using LearningPlatform.DAL.Repository.Impelementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningPlatform.mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private IUnitOfWork _unitOfWork; 
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

		public IActionResult Index(string searchTerm)
        {

            var categories = _unitOfWork.Course.GetAll(Includeword:"Category");
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CourseVM courseVM = new CourseVM()
            {
                Course = new Course(),
                CategoryList = _unitOfWork.Category.GetAll().Select(x=> new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })
            };
            return View(courseVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseVM courseVM,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Courses");
                    var ext = Path.GetExtension(file.FileName);
                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream); 
                    }
                    courseVM.Course.Image = @"Images\Courses\" + filename + ext;
                }

                _unitOfWork.Course.Add(courseVM.Course);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(courseVM.Course);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            //var category = _context.Categories.Find(id);

			CourseVM courseVM = new CourseVM()
			{
				Course = _unitOfWork.Course.GetFirstorDefault(x => x.Id == id),
				CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
				{
					Text = x.CategoryName,
					Value = x.Id.ToString()
				})
			};
			return View(courseVM);
        }
        [HttpPost]
        public IActionResult Edit(CourseVM courseVM,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
				string RootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string filename = Guid.NewGuid().ToString();
					var Upload = Path.Combine(RootPath, @"Images\Courses");
					var ext = Path.GetExtension(file.FileName);

                    if (courseVM.Course.Image != null)
                    {
                        var oldImg = Path.Combine(RootPath,courseVM.Course.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImg))
                        {
                            System.IO.File.Delete(oldImg);
                        }
                    }

					using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
					{
						file.CopyTo(filestream);
					}
					courseVM.Course.Image = @"Images\Courses\" + filename + ext;
				}


				//_context.Categories.Update(category);
				//_context.SaveChanges();
				_unitOfWork.Course.Update(courseVM.Course);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(courseVM.Course);
        }



        [HttpPost]
        public IActionResult DeleteCourse(int id)
        {
            var course = _unitOfWork.Course.GetFirstorDefault(x => x.Id == id);           
            _unitOfWork.Course.Remove(course);
            var oldImg = Path.Combine(_webHostEnvironment.WebRootPath, course.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImg))
            {
                System.IO.File.Delete(oldImg);
            }

            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
        



    }
}
