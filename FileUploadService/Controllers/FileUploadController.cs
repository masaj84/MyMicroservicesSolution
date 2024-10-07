using FileUploadService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Interfaces;
using SharedModels.Models;

namespace FileUploadService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        private readonly IFileStorage _fileStorage;
        private readonly IFilesUploadService _filesUploadService;


        public FileUploadController(IFileStorage fileStorage, IFilesUploadService filesUploadService)
        {
            _fileStorage = fileStorage;
            _filesUploadService = filesUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromBody] FileUpload fileUpload)
        {            
            if (fileUpload == null)
                return BadRequest("Error: No files attached!");


            var trackingId = _fileStorage.AddFile(fileUpload);

            //If all types of docs are sent => call RabbitMQ
            if (_fileStorage.AllRequiredFilesUploaded(fileUpload.BusinessUserId, fileUpload.CustomerId))
            {
                await _filesUploadService.SendMessage(fileUpload);
                return Ok(new { message = "All files has been uploaded", trackingId });
            }

            return Ok(new { message = "Files uploaded. This file track ID is: ", trackingId });
        }

        [HttpGet("{trackingId}")]
        public IActionResult GetStatus(string trackingId)
        {
            var files = _fileStorage.GetFilesByCustomer(trackingId).ToList();
            if (files.Count == 0)
                return NotFound("No files found for this ID.");

            return Ok(files);
        }
    }
}