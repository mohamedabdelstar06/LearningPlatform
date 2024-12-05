using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Entities;
using LearningPlatform.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Repository.Impelementation
{
	public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
	{
		private readonly LearningPlatformDbContext _context;
		public CategoryRepository(LearningPlatformDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(Category category)
		{
			var result = _context.Categories.FirstOrDefault(x=>x.Id == category.Id);
			if (result != null)
			{
				result.CategoryName = category.CategoryName;
				result.CreateTime = DateTime.Now;
			}

		}
	}
}
