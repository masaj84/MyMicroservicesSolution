using FileUploadService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enums;
using SharedModels.Interfaces;
using SharedModels.Models;

namespace FileUploadService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        private readonly IInMemoryFileStorage _inMemoryFileStorage;
        private readonly IFilesUploadService _filesUploadService;


        public FileUploadController(IInMemoryFileStorage inMemoryFileStorage, IFilesUploadService filesUploadService)
        {
            _inMemoryFileStorage = inMemoryFileStorage;
            _filesUploadService = filesUploadService;
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadFile([FromBody] FileUpload fileUpload)
        //{            
        //    if (fileUpload == null)
        //        return BadRequest("Error: No files attached!");

        //    var trackingId = _fileStorage.AddFile(fileUpload);

        //    //If all types of docs are sent => call RabbitMQ
        //    if (_fileStorage.AllRequiredFilesUploaded(fileUpload.BusinessUserId, fileUpload.CustomerId))
        //    {
        //        await _filesUploadService.SendMessage(fileUpload);
        //        return Ok(new { message = "All files has been uploaded", trackingId });
        //    }

        //    return Ok(new { message = "Files uploaded."});
        //}

        [HttpPost]
        public async Task<IActionResult> UploadFile(int userId, string userName,  int customerId, FileCategory filecategory, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileUpload = new FileUpload
            {
                CustomerId = customerId,
                FileName = file.FileName,
                FileCategory = filecategory, 
                IsUploaded = true,
                TrackingId = Guid.NewGuid().ToString(),
                UploadedAt = DateTime.UtcNow
            };

            var trackingId = _inMemoryFileStorage.AddFile(userId, userName, customerId, fileUpload);

            bool allFilesUploaded = _inMemoryFileStorage.AllRequiredFilesUploaded(userId, customerId);


            if (allFilesUploaded)
            {
                await _filesUploadService.SendMessage(fileUpload);
                return Ok(new { message = "All files has been uploaded", trackingId });
            }

            return Ok(new { message = "File uploaded successfully." });
        }


        [HttpGet("GetFilesForUser")]
        public IActionResult GetFilesForUser(int userId)
        {
            var files = _inMemoryFileStorage.GetFilesForUser(userId).ToList();
            if (files.Count == 0)
                return NotFound("No files found for this ID.");

            return Ok(files);
        }

        [HttpGet("GetFilesForCustomer")]
        public IActionResult GetFilesForCustomer(int customerId)
        {
            var files = _inMemoryFileStorage.GetFilesForCustomer(customerId).ToList();
            if (files.Count == 0)
                return NotFound("No files found for this ID.");

            return Ok(files);
        }

        
    }
}