using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public SignUpController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("SignUp/UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Ok();

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "ProfilePictureUploads");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return Ok();
        }
    }
}
