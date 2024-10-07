using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Interfaces
{
    public interface IInMemoryFileStorage
    {
        string AddFile(int userId, string userName, int customerId, FileUpload file);
        //IEnumerable<FileUpload> GetFilesByCustomer(string businessUserId, string customerId);
        //IEnumerable<FileUpload> GetFilesByCustomer(string trackingId);
        bool AllRequiredFilesUploaded(int businessUserId, int customerId);
        //bool CheckAllFilesUploaded(string businessUserId, string customerId);
        List<FileUpload> GetFilesForCustomer(int customerId);
        List<FileUpload> GetFilesForUser(int userId);
    }
}
