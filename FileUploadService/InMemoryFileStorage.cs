using System.Linq;
using SharedModels.Enums;
using SharedModels.Interfaces;
using SharedModels.Models;

namespace FileUploadService
{
    public class InMemoryFileStorage : IInMemoryFileStorage
    {
        private readonly Dictionary<int, User> _users = new();
        private readonly Dictionary<int, Customer> _customers = new();
        private readonly List<FileUpload> _fileUploads = new();

        public string AddFile(int userId, string userName, int customerId, FileUpload file)
        {
            if (!_users.ContainsKey(userId))
            {
                var user = new User
                {
                    UserId = userId,
                    Name = userName,
                    Email = $"{userName}@example.com", //assumption for an email
                    Customers = new List<Customer>()
                };
                AddUser(user);             }

            var existingUser = _users[userId];

            var customer = existingUser.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                customer = new Customer
                {
                    CustomerId = _customers.Count + 1, 
                    //Name = customerName,
                    UserId = userId,
                    User = existingUser,
                    FileUploads = new List<FileUpload>()
                };
                AddCustomer(customer); 
                existingUser.Customers.Add(customer); 
            }

            customer.FileUploads.Add(file);
            _customers[customer.CustomerId] = customer;
            _users[userId] = existingUser;

            return file.TrackingId;
        }

        public void AddUser(User user)
        {
            if (!_users.ContainsKey(user.UserId))
            {
                _users[user.UserId] = user;
            }
        }

        public void AddCustomer(Customer customer)
        {
            if (!_customers.ContainsKey(customer.CustomerId))
            {
                _customers[customer.CustomerId] = customer;
            }
        }

        public IEnumerable<FileUpload> GetFilesByCustomer(string trackingId)
        {
            return _fileUploads.Where(f => f.TrackingId == trackingId);
        }

        public User GetUserById(int userId)
        {
            return _users.ContainsKey(userId) ? _users[userId] : null;
        }

        public IEnumerable<Customer> GetCustomersByUser(int userId)
        {
            return _customers.Values.Where(c => c.UserId == userId);
        }

        public bool AllRequiredFilesUploaded(int userId, int customerId)
        {
            var customer = _customers.Values.FirstOrDefault(c => c.CustomerId == customerId && c.UserId == userId);

            if (customer == null)
            {
                return false; 
            }

            var requiredFiles = new List<FileCategory>
            {
                FileCategory.DrivingLicence,
                FileCategory.Agreement,
                FileCategory.Passport
            };


            var uploadedFiles = customer.FileUploads.Where(f => f.IsUploaded).Select(f => f.FileCategory);

            return requiredFiles.All(rf => uploadedFiles.Contains(rf));
        }


        public List<FileUpload> GetFilesForCustomer(int customerId)
        {
            var customer = _customers.Values.FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return new List<FileUpload>();
            }

            return customer.FileUploads.ToList();
        }

        public List<FileUpload> GetFilesForUser(int userId)
        {
            var customersForUser = _customers.Values.Where(c => c.UserId == userId);
            var filesForUser = customersForUser.SelectMany(c => c.FileUploads).ToList();

            return filesForUser;
        }
    }
}
