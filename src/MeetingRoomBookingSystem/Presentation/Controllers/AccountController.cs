
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DataAccess.Identity;
using Service.ServicesContract;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Presentation.Models;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IDepartmentManagementService _departmentManagementService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IDepartmentManagementService departmentManagementService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _departmentManagementService = departmentManagementService;
        }

        //--------Registration Code-----------
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync()
        {
            var model = new RegistrationModel();
            model.SetDepartmentsValues(await _departmentManagementService.GetDepartmentsAsync());
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Name,
                    Pin = model.Pin,
                    DepartmentId = model.DepartmentId
                };     

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "User");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            //return View(model);
            return RedirectToAction("AllUserList", "User");
        }


        //--------Login Code-----------
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            var model = new SigninModel();

            if (!string.IsNullOrEmpty(model.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, model.ErrorMessage);
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View(model);
        }

        [HttpPost, AutoValidateAntiforgeryToken, AllowAnonymous]
        public async Task<IActionResult> LoginAsync(SigninModel model)
        {

            if (ModelState.IsValid)
            {
                // Find the user by Email or PIN
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Pin == model.Pin);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberPassword, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Dashboard");
                }
                
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        //--------Logout Code-----------
        [AllowAnonymous]
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            returnUrl ??= Url.Content("~/");

            return LocalRedirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
