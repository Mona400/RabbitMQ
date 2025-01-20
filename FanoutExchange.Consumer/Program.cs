using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace FanoutExchange.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            Console.WriteLine("Waiting For Message...!");
            ConsumeMessage("q.fanout1");
            ConsumeMessage("q.fanout2");
            ConsumeMessage("q.fanout3");
            ConsumeMessage("q.fanout4");
            ConsumeMessage("q.fanout5");
            
            Console.WriteLine("press enter to exist");
            Console.ReadKey();
            void ConsumeMessage(string queue)
            {
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var bodey = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(bodey);
                    Console.WriteLine($"received {message} From {queue}...!");
                };
                channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
            }
        }
    }
}
