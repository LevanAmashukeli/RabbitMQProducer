using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQProducer
{
    class Program
    {
        static void Main(string[] args)
        {

            //var factory = new ConnectionFactory() { HostName = "localhost" };
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5672") };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MyTestQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "MyTestQueue",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" Message Sent {0}", message);
            }

            Console.ReadLine();
        }
    }
}
