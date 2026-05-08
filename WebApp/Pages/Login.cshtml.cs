using CoreApp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserManager _userManager;

        public LoginModel(UserManager userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Email and password are required.";
                return Page();
            }

            try
            {
                bool authenticated = _userManager.Authenticate(Email, Password);
                if (!authenticated)
                {
                    ErrorMessage = "Invalid email or password.";
                    return Page();
                }
            }
            catch
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }

            try
            {
                var userWithRole = _userManager.RetrieveRoleByUserEmail(Email);
                string role = userWithRole?.Role ?? "User";

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Email),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Role, role)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = false });

                return role switch
                {
                    "Doctor"    => RedirectToPage("/Doctor-Profile"),
                    "Nurse"     => RedirectToPage("/Nurse-Profile"),
                    "Secretary" => RedirectToPage("/Secretary-Profile"),
                    "Admin"     => RedirectToPage("/UserProfile"),
                    _           => RedirectToPage("/Patient-UserProfile")
                };
            }
            catch
            {
                ErrorMessage = "Login succeeded but user profile could not be loaded.";
                return Page();
            }
        }
    }
}
