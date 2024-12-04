using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Profile
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers.FindAsync(int.Parse(userId));
            if (adminUser == null)
            {
                return NotFound();
            }

            return View("AdminProfile", adminUser);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser == null)
            {
                return NotFound();
            }
            return View(adminUser);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminUser model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminUserExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Profile");
            }
            return View(model);
        }

        private bool AdminUserExists(int id)
        {
            return _context.AdminUsers.Any(e => e.Id == id);
        }
    }
}
