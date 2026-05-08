using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize(Roles = "Doctor")]
    public class Doctor_AppointmentModel : PageModel
    {
        public string? Email { get; set; }

        public void OnGet()
        {
            Email = User.Identity?.Name;
        }
    }
}
