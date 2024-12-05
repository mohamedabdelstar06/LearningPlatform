using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Repository.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LearningPlatformDbContext _context;
        private readonly IOrderRepository _orderRepository;

        public DashboardController(IUnitOfWork unitOfWork, IOrderRepository orderRepository, LearningPlatformDbContext context)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.CoursesCount = _unitOfWork.Course.GetAll().Count();
            ViewBag.UsersCount = _context.ApplicationUsers.ToList().Count();
            ViewBag.OrdersCount = _context.Orders.ToList().Count();
            ViewBag.Category = _unitOfWork.Category.GetAll().Count();

            /*var lastSixOrders = _context.Orders
			.Include(x => x.OrderItems)
			.ThenInclude(x => x.Course)
			.Include(x => x.User)
			.OrderByDescending(x => x.Id)
            .Take(6)*/
            var lastSixOrders = _orderRepository.GetAllOrders()
            .OrderByDescending(x => x.Id)
			.Take(6)
			.ToList();
            return View(lastSixOrders);
        }
    }
}
