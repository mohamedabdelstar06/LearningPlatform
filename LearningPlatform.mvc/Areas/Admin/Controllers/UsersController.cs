﻿using LearningPlatform.BLL.Utilities;
using LearningPlatform.DAL.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.mvc.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly LearningPlatformDbContext _context;

		public UsersController(LearningPlatformDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var result = (ClaimsIdentity)User.Identity;
			var claim = result.FindFirst(ClaimTypes.NameIdentifier);
			string userid = claim.Value;
			return View(_context.ApplicationUsers.Where(x=>x.Id != userid).ToList());
		}
        public IActionResult LockUnlock(string? id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.LockoutEnd == null || user.LockoutEnd < DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1);
            }
            else
            {
                user.LockoutEnd = DateTime.Now;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
        public IActionResult Delete(string? id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            _context.ApplicationUsers.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
