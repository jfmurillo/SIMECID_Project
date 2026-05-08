using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class UserProfileModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
