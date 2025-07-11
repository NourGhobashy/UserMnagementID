using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UserMnagementID.Models;
using UserMnagementID.ViewModels;

namespace UserMnagementID.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var usersList = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(userViewModels);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // نحصل على كل الأدوار
            var allRoles = await _roleManager.Roles.ToListAsync();

            // نجهز قائمة الـ RoleViewModel كلها غير محددة (isSelected = false)
            var roleViewModels = allRoles.Select(role => new RoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                isSelected = false
            }).ToList();

            // نجهز الـ ViewModel النهائي لإرسال البيانات للفورم
            var viewModel = new AddUserViewModel
            {
                Roles = roleViewModels
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Add(AddUserViewModel model)
        {
            var errors = new Dictionary<string, string>();

            if (!ModelState.IsValid)
            {
                errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value.Errors.First().ErrorMessage
                    );

                return Json(new { success = false, errors });
            }

            if (!model.Roles.Any(r => r.isSelected))
            {
                errors.Add("Roles", "Please select at least one role.");
                return Json(new { success = false, errors });
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                errors.Add("Email", "Email already exists.");
                return Json(new { success = false, errors });
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                errors.Add("UserName", "Username already exists.");
                return Json(new { success = false, errors });
            }

            var user = new AppUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            if (model.ProfileImage != null && model.ProfileImage.Length > 0)
            {
                using var ms = new MemoryStream();
                await model.ProfileImage.CopyToAsync(ms);
                user.ProfilePicture = ms.ToArray();
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errors.Add("", error.Description);
                }

                return Json(new { success = false, errors });
            }

            var selectedRoles = model.Roles
                .Where(r => r.isSelected)
                .Select(r => r.RoleName)
                .ToList();

            if (selectedRoles.Any())
            {
                await _userManager.AddToRolesAsync(user, selectedRoles);
            }

            return Json(new { success = true });
        }


        private async Task<List<RoleViewModel>> GetAllRolesAsync()
        {
            return await _roleManager.Roles
                .Select(r => new RoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    isSelected = false
                })
                .ToListAsync();
        }


        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var viewModel = new ProfileFormViewModel
            {
                ProfileID= userId,
                FirstName=user.FirstName,
                LastName=user.LastName,
                UserName=user.UserName,
                Email=user.Email,
                ExistingProfilePicture = user.ProfilePicture
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.ProfileID);
            if (user == null)
                return NotFound();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail != null && userWithSameEmail.Id != model.ProfileID)
            {
                ModelState.AddModelError("Email", "This email is already exist");
                return View(model);
            }

            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
            if (userWithSameUserName != null && userWithSameUserName.Id != model.ProfileID)
            {
                ModelState.AddModelError("UserName", "This UserName is already exist");
                return View(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;

            // حفظ التعديلات
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index)); // ← ده اللي بيرجع للصفحة الرئيسية بعد التعديل
        }



        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();
            var roleViewModels = new List<RoleViewModel>();

            foreach (var role in roles)
            {
                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                roleViewModels.Add(new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    isSelected = isInRole
                });
            }

            var viewModel = new UserRoleViewModel
            {
                UserId = user.Id,
                UserName = $"{user.FirstName} {user.LastName}",
                Roles = roleViewModels
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if(userRoles.Any(r => r==role.RoleName) && !role.isSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                if(!userRoles.Any(r => r==role.RoleName) && role.isSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
