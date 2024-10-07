using MassTransit;
using SharedModels.Models;

namespace NotificationService.Services
{
    public class NotificationConsumer : IConsumer<FileUpload>
    {
        public NotificationConsumer()
        {

        }

        public async Task Consume(ConsumeContext<FileUpload> context)
        {
            var message = context.Message;
            Console.WriteLine($"Received notification, track ID: {message.TrackingId}");

           
            Console.WriteLine($"Email end: all files reqired for track ID: {message.TrackingId}, has been uploaded successfully");
        }
    }
}
