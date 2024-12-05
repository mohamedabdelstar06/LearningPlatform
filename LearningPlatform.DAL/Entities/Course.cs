using LearningPlatform.DAL.Data.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Entities
{
	public class Course
	{
		public int Id { get; set; }
		public string CourseName { get; set; }
		public string CourseDescription { get; set; }
		[ValidateNever]
		public string Image { get; set; }
		public int TotalLecture { get; set; }
		public int CourseTime { get; set; }
		public double Price { get; set; }
		public Level Level { get; set; }
		public bool IsTrending{ get; set; }
		[DisplayName("Category")]
		public int CategoryId { get; set; }
		[ValidateNever]
		public Category Category { get; set; }
	}
}
