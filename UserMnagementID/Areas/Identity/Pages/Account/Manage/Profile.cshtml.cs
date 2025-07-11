//namespace UserMnagementID.Areas.Identity.Pages.Account.Manage
//{
//    public class Profile
//    {
//    }
//}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using UserMnagementID.Models;

namespace UserMnagementID.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ProfileModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                await _signInManager.SignOutAsync(); // يسجلك خروج
                return RedirectToPage("/Account/Login"); // يرجعك لصفحة الدخول
            }

            Username = user.UserName;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;

            return Page();
        }

        //    public async Task<IActionResult> OnGetAsync()
        //    {
        //        var user = await _userManager.GetUserAsync(User);

        //        if (user == null)
        //        {
        //            await _signInManager.SignOutAsync();
        //            return RedirectToPage("/Account/Login");
        //        }

        //        Username = user.UserName;
        //        Email = user.Email;
        //        FirstName = user.FirstName;
        //        LastName = user.LastName;

        //        return Page();
        //    }
        //}
    }
}