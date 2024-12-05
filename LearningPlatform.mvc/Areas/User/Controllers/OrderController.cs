using LearningPlatform.DAL.Cart;
using LearningPlatform.DAL.Repository.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.mvc.Areas.User.Controllers
{
	[Area("User")]
    [Authorize]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ShoppingCart _shoppingCart;
		private readonly IOrderRepository _orderRepository;

		public OrderController(IUnitOfWork unitOfWork, ShoppingCart shoppingCart, IOrderRepository orderRepository)
		{
			_unitOfWork = unitOfWork;
			_shoppingCart = shoppingCart;
			_orderRepository = orderRepository;
		}
		public ActionResult Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string roleId = User.FindFirstValue(ClaimTypes.Role);
			var order = _orderRepository.GetOrderAndRoleByUserId(userId,roleId);
			return View(order);
		}
		public IActionResult ShoppingCart()
		{
			var item = _shoppingCart.GetShoppingCartItems();
			ViewBag.Total = _shoppingCart.GetShoppingCartTotal();
			return View(item);
		}

        public IActionResult AddToCart(int id)
        {
            var item = _unitOfWork.Course.GetFirstorDefault(x => x.Id == id);
            if (item != null)
            {
                _shoppingCart.AddItemToShoppingCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }
        public IActionResult RemoveFromCart(int id)
        {
            var item = _unitOfWork.Course.GetFirstorDefault(x => x.Id == id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromShoppingCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
		public IActionResult CompleteOrder()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_orderRepository.StoredOrders(items, userId);
			_shoppingCart.ClearShoppingCart();
			return View("CompleteOrder");
		}



	}
}
