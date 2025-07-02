using Microsoft.AspNetCore.Mvc;
using NZwalksApi.Models.Domain;
using NZwalksApi.Models.DTO;
using NZwalksApi.Repositories;

namespace NZwalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController :ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository) {
            this.imageRepository = imageRepository;
        }

        // POST: api/images/upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto request)
        {   
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.FileName).ToLower())){
                ModelState.AddModelError("File", "Unsupported file extnesion");
            }

            if (request.File.Length > 5 * 1024 * 1024) // 5 MB limit
            {
                ModelState.AddModelError("File", "File size exceeds the limit of 5 MB");
            }
        }
    }
}
