using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class FileUpload
    {
        public string TrackingId { get; set; } = Guid.NewGuid().ToString(); //Track ID
        public string BusinessUserId { get; set; }  //Business ID
        public string CustomerId { get; set; }  //Customer ID
        public string FileName { get; set; }
        public FileCategory FileCategory { get; set; }
        public bool IsUploaded { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
