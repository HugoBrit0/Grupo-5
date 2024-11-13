using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name ?? string.Empty;
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.Username == username);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var username = User.Identity?.Name ?? string.Empty;
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.Username == username);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity?.Name ?? string.Empty;
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.Username == username);

                if (userProfile == null)
                {
                    return NotFound();
                }

                userProfile.FullName = model.FullName;
                userProfile.Email = model.Email;
                userProfile.PhoneNumber = model.PhoneNumber;
                userProfile.Address = model.Address;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadProfilePicture(IFormFile profilePicture)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var username = User.Identity?.Name ?? string.Empty;
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.Username == username);

            if (userProfile == null)
            {
                return NotFound();
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + profilePicture.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(fileStream);
            }

            userProfile.ProfilePicturePath = "/uploads/" + uniqueFileName;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
