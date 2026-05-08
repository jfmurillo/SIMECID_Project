using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize(Roles = "Nurse")]
    public class Nurse_MedicalReportModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
