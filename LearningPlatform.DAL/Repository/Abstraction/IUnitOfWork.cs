using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Repository.Abstraction
{
	public interface IUnitOfWork:IDisposable
	{
		ICategoryRepository Category { get; }
		ICourseRepository Course { get; }
		int Complete();
	}
}
