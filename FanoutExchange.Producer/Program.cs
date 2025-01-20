using RabbitMQ.Client;
using System.Text;

namespace FanoutExchange.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            const string message = " message is sending....!";
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "amq.fanout", routingKey: string.Empty, basicProperties: null, body: body);
            Console.WriteLine($"send {message}");
            Console.WriteLine("press enter to exist");
            Console.ReadKey();
        }
    }
}
