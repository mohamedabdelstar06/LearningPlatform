using LearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.BLL.ViewModels
{
	public class CourseViewModel
	{
		public IEnumerable<Course> TrendingCourse { get; set; }		
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Course> Courses { get; set; }
	}
}

