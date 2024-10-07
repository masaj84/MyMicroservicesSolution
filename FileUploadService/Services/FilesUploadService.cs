
using FileUploadService.Interfaces;
using MassTransit;
using RabbitMQ.Client;
using SharedModels.Models;
using System.Text;

namespace FileUploadService.Services
{
    public class FilesUploadService : IFilesUploadService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public FilesUploadService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }


        // Asynchroniczna metoda do wysyłania wiadomości
        public async Task SendMessage(FileUpload fileUpload)
        {
            await _publishEndpoint.Publish<FileUpload>(fileUpload);

            Console.WriteLine($"[x] Message sent: {fileUpload.TrackingId}");
        }
    }
}
