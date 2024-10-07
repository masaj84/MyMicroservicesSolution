using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Enums;

namespace SharedModels.Models
{
    public class FileUpload
    {
        public string? TrackingId { get; set; } = Guid.NewGuid().ToString();
        public int CustomerId { get; set; }  
        public string FileName { get; set; }
        public FileCategory FileCategory { get; set; }
        public bool IsUploaded { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
