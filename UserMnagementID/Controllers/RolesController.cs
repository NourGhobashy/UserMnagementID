using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserMnagementID.ViewModels;

namespace UserMnagementID.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleViewModels model)
        {
            if (!ModelState.IsValid)
                return View("Index", await _roleManager.Roles.ToListAsync());

            
            if(await _roleManager.RoleExistsAsync(model.RoleName))
            {
                ModelState.AddModelError("RoleName", "is Exist");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }
            await _roleManager.CreateAsync(new IdentityRole(model.RoleName.Trim()));
            return RedirectToAction(nameof(Index));

        }
    }
}
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//using System.Threading.Tasks;
//using UserMnagementID.Models;

//namespace UserMnagementID.Controllers
//{
//    public class RolesController : Controller
//    {
//        private readonly UserManager<AppUser> _userManager;

//        public RolesController(UserManager<AppUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        // GET: /Roles/ForceResetPasswordByEmail?email=admin@admin.com
//        public async Task<IActionResult> ForceResetPasswordByEmail(string email)
//        {
//            if (string.IsNullOrEmpty(email))
//                return Content("❌ يرجى إدخال الإيميل في الرابط مثل: /Roles/ForceResetPasswordByEmail?email=admin@admin.com");

//            var user = await _userManager.FindByEmailAsync(email);
//            if (user == null)
//                return Content("❌ لم يتم العثور على مستخدم بهذا الإيميل.");

//            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//            var result = await _userManager.ResetPasswordAsync(user, token, "NewPassword123!");

//            if (result.Succeeded)
//                return Content("✅ تم تغيير كلمة المرور إلى: NewPassword123!");

//            return Content("❌ فشل: " + string.Join(", ", result.Errors.Select(e => e.Description)));
//        }
//    }
//}
