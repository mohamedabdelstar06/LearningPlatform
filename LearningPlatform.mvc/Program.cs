using LearningPlatform.DAL.DB;
using LearningPlatform.DAL.Repository.Abstraction;
using LearningPlatform.DAL.Repository.Impelementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LearningPlatform.BLL.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using LearningPlatform.DAL.Cart;

namespace LearningPlatform.mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Enhancement ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("defaultConnection");

            builder.Services.AddDbContext<LearningPlatformDbContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4))                
                .AddDefaultTokenProviders().AddDefaultUI()
                .AddEntityFrameworkStores<LearningPlatformDbContext>();

            builder.Services.AddSingleton<IEmailSender,EmailSender>();

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Services.AddScoped(x => ShoppingCart.GetShoppingCart(x));
			builder.Services.AddSession();

			builder.Services.AddScoped<IOrderRepository, OrderRepository>();
			builder.Services.AddRazorPages();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "User",
                pattern: "{area=User}/{controller=Dashboard}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
