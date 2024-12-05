using LearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Repository.Abstraction
{
	public interface IOrderRepository
	{
		void StoredOrders(List<ShoppingCartItem> items, string userId);
		List<Order> GetOrderAndRoleByUserId(string userId, string role);
		List<Order> GetAllOrders();
        void RemoveOrderById(int orderId);

    }
}
