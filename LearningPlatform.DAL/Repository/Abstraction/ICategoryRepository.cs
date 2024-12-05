using LearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Repository.Abstraction
{
	public interface ICategoryRepository:IGenericRepository<Category>
	{
		void Update(Category category);
	}
}
