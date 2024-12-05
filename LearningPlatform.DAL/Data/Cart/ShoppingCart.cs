using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace LearningPlatform.DAL.Cart
{
	public class ShoppingCart
	{
		private readonly LearningPlatformDbContext _context;
        public string ShoppingCartId { get; set; }
        public ShoppingCart(LearningPlatformDbContext context)
        {
            _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
			var session = service.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = service.GetRequiredService<LearningPlatformDbContext>();
            string cartId = session.GetString("CartId")??Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId};
        }
        // Get All Item in Shopping Cart
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return _context.ShoppingCartItems.Where(x=>x.ShoppingCartId == ShoppingCartId)
                .Include(x=>x.Course).ToList();
        }
        // Calculate Total Amount in Shopping Cart Item
        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(x=>x.ShoppingCartId==ShoppingCartId)
                .Select(x=> (double)x.Course.Price * x.Amount).Sum();
            return total;
        }
        public int GetShoppingCartTotalAmount()
            =>_context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select
            (x=>x.Amount).Sum();
        // Add item to cart
        public void AddItemToShoppingCart(Course course)
        {
            var shoppingCartItem =  _context.ShoppingCartItems.FirstOrDefault(x => x.ShoppingCartId == ShoppingCartId && x.Course.Id== course.Id);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Course = course,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
                
            }
            else
            {
                shoppingCartItem.Amount += 1; 
            }
			 _context.SaveChanges();
		}
        //Removing From Cart
        public void RemoveItemFromShoppingCart(Course course)
        {
			var shoppingCartItem =  _context.ShoppingCartItems.FirstOrDefault(x => x.ShoppingCartId == ShoppingCartId && x.Course.Id == course.Id);
            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount -= 1;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
                 _context.SaveChanges();

            }

		}
        public void ClearShoppingCart()
        {
            var items = _context.ShoppingCartItems
				.Where(x => x.ShoppingCartId == ShoppingCartId).ToList();
            _context.ShoppingCartItems.RemoveRange(items);
            _context.SaveChanges();
        }
	}
}
