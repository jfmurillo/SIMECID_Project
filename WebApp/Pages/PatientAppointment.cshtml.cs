using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize(Roles = "User")]
    public class PatientAppointmentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
