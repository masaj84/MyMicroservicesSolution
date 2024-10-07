
using System.Linq;
using SharedModels.Interfaces;
using SharedModels.Models;

namespace FileUploadService
{
    public class InMemoryFileStorage : IFileStorage
    {
        private readonly List<FileUpload> _fileUploads = new();

        public string AddFile(FileUpload file)
        {
            _fileUploads.Add(file);
            return file.TrackingId;
        }

        public IEnumerable<FileUpload> GetFilesByCustomer(string businessUserId, string customerId)
        {
            return _fileUploads.Where(f => f.BusinessUserId == businessUserId && f.CustomerId == customerId);
        }

        public IEnumerable<FileUpload> GetFilesByCustomer(string trackingId)
        {
            return _fileUploads.Where(f => f.TrackingId == trackingId);
        }

        public bool AllRequiredFilesUploaded(string businessUserId, string customerId)
        {
            var requiredFiles = new List<FileCategory>
            {
                FileCategory.DrivingLicence,
                FileCategory.Agreement,
                FileCategory.Passport
            };


            //var requiredFiles2 = FileCategory.ToListElements();


            var uploadedFiles = _fileUploads
                .Where(f => f.BusinessUserId == businessUserId && f.CustomerId == customerId && f.IsUploaded)
                .Select(f => f.FileCategory);

            return requiredFiles.All(rf => uploadedFiles.Contains(rf));
        }
    }
}
