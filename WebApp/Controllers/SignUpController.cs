using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

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
            if (file != null && file.Length > 0)
            {
                // Ruta donde se guardará el archivo
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                // Asegúrate de que la carpeta exista, si no, créala
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Obtén el nombre original del archivo
                var originalFileName = Path.GetFileName(file.FileName);

                // Combina el nombre original con la ruta de la carpeta de carga
                var filePath = Path.Combine(uploadsFolder, originalFileName);

                // Guarda el archivo en el servidor
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Devuelve un mensaje indicando que la carga del archivo se ha completado
                return Content("File uploaded successfully.");
            }

            // Si no se ha enviado ningún archivo, devolver un BadRequest u otro resultado adecuado
            return BadRequest("No file was uploaded.");
        }
    }
}
