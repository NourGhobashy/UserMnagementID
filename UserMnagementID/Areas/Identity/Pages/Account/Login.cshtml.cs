//// Licensed to the .NET Foundation under one or more agreements.
//// The .NET Foundation licenses this file to you under the MIT license.
//#nullable disable

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.Extensions.Logging;
//using UserMnagementID.Models;

//namespace UserMnagementID.Areas.Identity.Pages.Account
//{
//    public class LoginModel : PageModel
//    {
//        //private readonly SignInManager<AppUser> _signInManager;
//        //private readonly ILogger<LoginModel> _logger;
//        ////private object _userManager;
//        //private readonly UserManager<AppUser> _userManager;
//        //private UserManager<AppUser> userManager;

//        //public LoginModel(SignInManager<AppUser> signInManager, ILogger<LoginModel> logger)
//        //{
//        //    _signInManager = signInManager;
//        //    _userManager = userManager;

//        //    _logger = logger;
//        //}
//        private readonly SignInManager<AppUser> _signInManager;
//        private readonly UserManager<AppUser> _userManager;
//        private readonly ILogger<LoginModel> _logger;

//        public LoginModel(SignInManager<AppUser> signInManager,
//                          UserManager<AppUser> userManager,
//                          ILogger<LoginModel> logger)
//        {
//            _signInManager = signInManager;
//            _userManager = userManager;
//            _logger = logger;
//        }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        [BindProperty]
//        public InputModel Input { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        public IList<AuthenticationScheme> ExternalLogins { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        public string ReturnUrl { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        [TempData]
//        public string ErrorMessage { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        public class InputModel
//        {
//            /// <summary>
//            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//            ///     directly from your code. This API may change or be removed in future releases.
//            /// </summary>
//            //[Required]
//            ////[EmailAddress]
//            //[Display(Name ="Email or Username")]
//            //public string Email { get; set; }

//            [Required]
//            //[EmailAddress]
//            [Display(Name = "First Name")]
//            public string FirstName { get; set; }

//            [Required]
//            //[EmailAddress]
//            [Display(Name = "Last Name")]
//            public string LastName { get; set; }

//            [Required]
//                [Display(Name = "Email or Username")]
//                public string Email { get; set; }

//                [Required]
//                [DataType(DataType.Password)]
//                public string Password { get; set; }

//                [Display(Name = "Remember me?")]
//                public bool RememberMe { get; set; }



//            /// <summary>
//            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//            ///     directly from your code. This API may change or be removed in future releases.
//            /// </summary>
//            //[Required]
//            //[DataType(DataType.Password)]
//            //public string Password { get; set; }

//            /// <summary>
//            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//            ///     directly from your code. This API may change or be removed in future releases.
//            /// </summary>
//            //[Display(Name = "Remember me?")]
//            //public bool RememberMe { get; set; }
//        }

//        public async Task OnGetAsync(string returnUrl = null)
//        {
//            if (!string.IsNullOrEmpty(ErrorMessage))
//            {
//                ModelState.AddModelError(string.Empty, ErrorMessage);
//            }

//            returnUrl ??= Url.Content("~/");

//            // Clear the existing external cookie to ensure a clean login process
//            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

//            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

//            ReturnUrl = returnUrl;
//        }
//        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
//        {
//            returnUrl ??= Url.Content("~/");
//            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

//            string username = Input.Email;

//            // لو المستخدم كتب إيميل، نحاول نجيب الـ UserName المرتبط بيه
//            if (new EmailAddressAttribute().IsValid(Input.Email))
//            {
//                var user = await _userManager.FindByEmailAsync(Input.Email);
//                if (user != null)
//                {
//                    username = user.UserName;
//                }
//            }

//            if (ModelState.IsValid)
//            {
//                // تسجيل الدخول
//                var result = await _signInManager.PasswordSignInAsync(username, Input.Password, Input.RememberMe, lockoutOnFailure: false);

//                if (result.Succeeded)
//                {
//                    _logger.LogInformation("User logged in.");
//                    return LocalRedirect(returnUrl);
//                }

//                if (result.RequiresTwoFactor)
//                {
//                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
//                }

//                if (result.IsLockedOut)
//                {
//                    _logger.LogWarning("User account locked out.");
//                    return RedirectToPage("./Lockout");
//                }

//                // في حالة فشل تسجيل الدخول
//                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

//            }

//            // لو حصلت أخطاء في الفاليديشن أو في تسجيل الدخول
//            return Page();
//        }

//        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
//        //{
//        //    returnUrl ??= Url.Content("~/");

//        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

//        //    var user = await _userManager.FindByEmailAsync(Input.Email);

//        //    var username = new EmailAddressAttribute().IsValid(Input.Email) ? _userManager.FindByEmailAsync(Input.Email).Result.UserName : Input.Email;
//        //    //user?.UserName ?? Input.Email;

//        //    if (ModelState.IsValid)
//        //    {
//        //        // This doesn't count login failures towards account lockout
//        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
//        //        var result = await _signInManager.PasswordSignInAsync(username, Input.Password, Input.RememberMe, lockoutOnFailure: false);
//        //        if (result.Succeeded)
//        //        {
//        //            _logger.LogInformation("User logged in.");
//        //            return LocalRedirect(returnUrl);
//        //        }
//        //        if (result.RequiresTwoFactor)
//        //        {
//        //            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
//        //        }
//        //        if (result.IsLockedOut)
//        //        {
//        //            _logger.LogWarning("User account locked out.");
//        //            return RedirectToPage("./Lockout");
//        //        }
//        //        else
//        //        {
//        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//        //            return Page();
//        //        }
//        //    }

//        //    // If we got this far, something failed, redisplay form
//        //    return Page();
//        //}
//    }
//}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using UserMnagementID.Models;

namespace UserMnagementID.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<AppUser> signInManager,
                          UserManager<AppUser> userManager,
                          ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Email or Username")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
            {
                return Page(); // لو الفورم مش Valid، يعيد عرض الصفحة مع الأخطاء
            }

            // نحاول تحويل الإيميل إلى اسم مستخدم إذا كان إيميل
            string username = Input.Email;

            if (new EmailAddressAttribute().IsValid(Input.Email))
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user != null)
                {
                    username = user.UserName;
                }
            }


            var result = await _signInManager.PasswordSignInAsync(username, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}

