using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Entities
{
	public class Order
	{
		public Order()
		{
			OrderItems = new HashSet<OrderItem>();
		}
		public int Id { get; set; }
		public string UserId { get; set; }
		public virtual ApplicationUser User{ get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
