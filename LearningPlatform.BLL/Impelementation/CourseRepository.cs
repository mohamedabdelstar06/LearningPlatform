using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Entities;
using LearningPlatform.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Repository.Impelementation
{
	public class CourseRepository : GenericRepository<Course>,ICourseRepository
	{
		private readonly LearningPlatformDbContext _context;
		public CourseRepository(LearningPlatformDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(Course course)
		{
			var result = _context.Courses.FirstOrDefault(x=>x.Id == course.Id);
			if (result != null)
			{
				result.CourseName = course.CourseName;
				result.CourseDescription = course.CourseDescription;
				result.CourseTime = course.CourseTime;
				result.TotalLecture = course.TotalLecture;
				result.Image = course.Image;
				result.Price = course.Price;
				result.CategoryId = course.CategoryId;
			}

		}
		public IEnumerable<Course> TrendingCourse()
		{
			return _context.Courses.Include(c => c.Category).Where(p => p.IsTrending); 
		}
	}
}
