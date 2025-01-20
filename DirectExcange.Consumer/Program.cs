using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DirectExcange.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            Console.WriteLine("waiting for message...!");
            var consumer=new EventingBasicConsumer(channel);

            //This function execute every time  when the consumer consume the message from the queue
            consumer.Received += (model, ea) =>
            {
                var bodey=ea.Body.ToArray();
                var message=Encoding.UTF8.GetString(bodey);

                //do some processing 
                //ex: notification function
                Console.WriteLine("received message...!");
            };
            channel.BasicConsume(queue: "qu-notification", autoAck:true,consumer:consumer);
            Console.WriteLine("press enter to exist");
            Console.ReadKey();
        }
    }
}
