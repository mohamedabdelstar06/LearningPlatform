using LearningPlatform.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL.DB
{
    public class LearningPlatformDbContext:IdentityDbContext<IdentityUser>
    {
        public LearningPlatformDbContext(DbContextOptions<LearningPlatformDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
		public DbSet<Course> Courses{ get; set; }        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
	}
}
