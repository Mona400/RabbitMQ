using RabbitMQ.Client;
using System.Text;

namespace HeaderExchange.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            var properties=channel.CreateBasicProperties();
            properties.Persistent = false;

            Dictionary<string,object>dictionary = new Dictionary<string,object>();
            dictionary.Add("name", "info");
            properties.Headers = dictionary;  
            const string message = " message is sending....!";
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "amq.headers", routingKey: string.Empty, basicProperties: properties, body: body);
            
            
            Console.WriteLine($"send {message}");
            Console.WriteLine("press enter to exist");
            Console.ReadKey();
        }
    }
}
