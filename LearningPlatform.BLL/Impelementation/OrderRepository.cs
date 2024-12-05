using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Entities;
using LearningPlatform.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Repository.Impelementation
{
	public class OrderRepository :IOrderRepository
	{
		private readonly LearningPlatformDbContext _context;
		public OrderRepository(LearningPlatformDbContext context) 
		{
			_context = context;
		}

		public List<Order> GetOrderAndRoleByUserId(string userId, string role)
		{
			var order = _context.Orders
				.Include(x => x.OrderItems)
				.ThenInclude(x => x.Course)
				.Include(x=>x.User)
				.ToList();

			if (role != "Admin")
			{
				order = order.Where(x => x.UserId == userId).ToList();
			}
			return order;
		}
		public List<Order> GetAllOrders()
		{
			var orders = _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Course)
                .Include(x => x.User)
                .ToList();
			return orders;
		}

		public void StoredOrders(List<ShoppingCartItem> items, string userId)
		{
			var order = new Order()
			{
				UserId = userId

			};
			_context.Orders.Add(order);
			_context.SaveChanges();
			foreach (var item in items)
			{
				var orderitem = new OrderItem()
				{
					Amount = item.Amount,
					Price = (double)item.Course.Price,
					OrderId = order.Id,
					CourseId = item.Course.Id
				};
				_context.OrderItems.Add(orderitem);
			}
			_context.SaveChanges();
		}
        public void RemoveOrderById(int orderId)
        {
            var order = _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefault(x => x.Id == orderId);

            if (order != null)
            {
                _context.OrderItems.RemoveRange(order.OrderItems);
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
	
}
