using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ELIMS_MVC.Authorization;
using ELIMS_MVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ELIMS_MVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger
            //,
            //IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            //[Display(Name = "If you are an NAU student or faculty, please enter your User ID # (ex: 1234567)")]
            //public string UserId { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Select one")]
            public string Status { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //string un = Convert.ToString(Input.Email);
                //int index = un.IndexOf("@");
                //if(index > 0)
                //{
                //    un = un.Substring(0, index);
                //}

                var user = new ApplicationUser { FirstName = Input.FirstName, LastName = Input.LastName,  UserName = Input.UserName, Status = Input.Status, Email = Input.Email };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (Input.Status == "Student" || Input.Status == "Researcher (non-student)")
                    {
                        await _userManager.AddToRoleAsync(user, "USERS");
                        user.LabAccess = false;
                    }
                    else if (Input.Status == "Student manager" || Input.Status == "Lab manager (non-student)")
                    {
                        await _userManager.AddToRoleAsync(user, "MANAGERS");
                        user.LabAccess = true;
                    }
                    else if (Input.Status == "Lab administrator")
                    {
                        await _userManager.AddToRoleAsync(user, "ADMINISTRATORS");
                        user.LabAccess = true;
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
