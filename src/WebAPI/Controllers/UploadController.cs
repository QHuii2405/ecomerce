using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost("multiple")]
        [Authorize] // Allow any authenticated user (for Return requests)
        public async Task<IActionResult> UploadImages(List<IFormFile>? files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files provided.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".mp4", ".mov", ".avi" };
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var urls = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (Array.Exists(allowedExtensions, e => e == extension))
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        urls.Add($"/uploads/{uniqueFileName}");
                    }
                }
            }

            return Ok(new { urls });
        }
    }
}
