using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Repository.Impelementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly LearningPlatformDbContext _context;
		public ICategoryRepository Category { get; private set; }
		public ICourseRepository Course { get; private set; }
		public UnitOfWork(LearningPlatformDbContext context)
		{
			_context = context;
			Category = new CategoryRepository(context);
			Course = new CourseRepository(context);
		}		

		public int Complete()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
