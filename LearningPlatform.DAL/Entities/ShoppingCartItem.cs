using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Entities
{
	public class ShoppingCartItem
	{
		public int Id { get; set; }	
		public Course Course { get; set; }
		public int Amount { get; set; }
		public string ShoppingCartId {  get; set; }
	}
}
