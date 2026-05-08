using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize(Roles = "Secretary")]
    public class Secretary_AppointmentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
