﻿using Microsoft.AspNetCore.Hosting;
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

                // Obtén el nombre del archivo original
                var fileName = Path.GetFileName(file.FileName);

                // Construye la ruta completa donde se guardará el archivo
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Guarda el archivo en el servidor
                    await file.CopyToAsync(fileStream);
                }

                // Muestra el nombre del archivo en la consola
                Console.WriteLine($"El archivo '{fileName}' se ha cargado correctamente.");
            }
            else
            {
                // Si no se proporciona ningún archivo, muestra un mensaje en la consola
                Console.WriteLine("No se ha seleccionado ningún archivo para cargar.");
            }

            // Retorna una respuesta de éxito al cliente
            return Ok();
        }
    }
}
