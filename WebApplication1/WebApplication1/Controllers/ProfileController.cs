using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.IdentityUserId == userId);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }
    }
}