using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Entities;
using LearningPlatform.DAL.Repository.Abstraction;
using LearningPlatform.DAL.Repository.Impelementation;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Add(category);
                //_context.SaveChanges();
                _unitOfWork.Category.Add(category);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            //var category = _context.Categories.Find(id);
            var category = _unitOfWork.Category.GetFirstorDefault(x => x.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Update(category);
                //_context.SaveChanges();
                _unitOfWork.Category.Update(category);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var category = _unitOfWork.Category.GetFirstorDefault(x => x.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult DeleteCategory(int? id)
        {
            var category = _unitOfWork.Category.GetFirstorDefault(x => x.Id == id);
            if (category == null)
            {
                NotFound();
            }
            //_context.Categories.Remove(category);
            //_context.SaveChanges();
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}
