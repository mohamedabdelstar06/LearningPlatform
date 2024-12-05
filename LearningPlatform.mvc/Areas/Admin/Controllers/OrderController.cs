using LearningPlatform.DAL.Cart;
using LearningPlatform.DAL.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.mvc.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IOrderRepository _orderRepository;

		public OrderController(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
		{
			_unitOfWork = unitOfWork;
			_orderRepository = orderRepository;
		}

		public IActionResult Index()
		{
			var result = _orderRepository.GetAllOrders();
			return View(result);
		}
        [HttpPost]
        public IActionResult Remove(int id)
        {
            _orderRepository.RemoveOrderById(id);
            return RedirectToAction("Index");
        }
    }
}
