using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<FileUpload> FileUploads { get; set; }
    }
}
