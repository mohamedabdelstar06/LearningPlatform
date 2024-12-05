using LearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.BLL.ViewModels
{
	public class TrendingViewModel
	{
		public IEnumerable<Course> TrendingCourse { get; }
		public TrendingViewModel(IEnumerable<Course> trendingCourse)
		{
			TrendingCourse = trendingCourse;
		}
	}
}
