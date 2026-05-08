using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize]
    public class PrescriptionManagmentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
