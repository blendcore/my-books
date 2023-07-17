using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;
using System;
using my_books.Data.Models;
using System.Text.Json;
using my_books.Data.ViewModels;

namespace my_books.Repositories
{
    public class ServiceBus : IServiceBus
    {
        private readonly IConfiguration _configuration;
        public ServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMessageAsync(BookVM book)
        {
            IQueueClient client = new QueueClient(_configuration["AzureServiceBusConnectionString"], _configuration["QueueName"]);
            //Serialize car details object
            var messageBody = JsonSerializer.Serialize(book);
            //Set content type and Guid
            var message = new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                MessageId = Guid.NewGuid().ToString(),
                ContentType = "application/json"
            };
            await client.SendAsync(message);
        }
    }
}
