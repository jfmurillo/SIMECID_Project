using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize(Roles = "Doctor")]
    public class Doctor_MedicalReportModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
