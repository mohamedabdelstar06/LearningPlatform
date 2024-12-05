using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.Repository.Impelementation
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly LearningPlatformDbContext _context;
		private DbSet<T> _dbSet;

		public GenericRepository(LearningPlatformDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? perdicate = null, string? Includeword = null)
		{
			IQueryable<T> query = _dbSet;
			if(perdicate != null)
			{
				query = query.Where(perdicate);
			}
			if(Includeword != null)
			{
				// _context.Products .include("Category","User")
				foreach(var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(item);
				}
			}
			return query.ToList();
		}

		public T GetFirstorDefault(Expression<Func<T, bool>>? perdicate = null, string? Includeword = null)
		{
			IQueryable<T> query = _dbSet;
			if (perdicate != null)
			{
				query = query.Where(perdicate);
			}
			if (Includeword != null)
			{
				// _context.Products .include("Category","User")
				foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(item);
				}
			}
			return query.SingleOrDefault();
		}

		public void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}
	}
}
