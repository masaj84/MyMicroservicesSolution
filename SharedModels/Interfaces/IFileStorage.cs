using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Interfaces
{
    public interface IFileStorage
    {
        string AddFile(FileUpload file);
        IEnumerable<FileUpload> GetFilesByCustomer(string businessUserId, string customerId);
        IEnumerable<FileUpload> GetFilesByCustomer(string trackingId);
        bool AllRequiredFilesUploaded(string businessUserId, string customerId);
    }
}
