using SharedModels.Models;

namespace FileUploadService.Interfaces
{
    public interface IFilesUploadService
    {
        Task SendMessage(FileUpload fileUpload);
    }
}
