using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Entities
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int Amount { get; set; }
		public double Price { get; set; }
		public int OrderId { get; set; }
		[ForeignKey(nameof(OrderId))]
		public virtual Order Order { get; set; }
		public int CourseId { get; set; }
		public virtual Course Course { get; set; }
	}
}
