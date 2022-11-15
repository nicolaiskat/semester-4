using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

class Receive
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "172.17.0.2" };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            Console.WriteLine(" Applikation startet");

            channel.QueueDeclare(queue: "taxa",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                int id = 0;
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Booking? booking = JsonSerializer.Deserialize<Booking>(message);
                booking.ID = id++;
                booking.TidStempel = DateTime.Now;

                Console.WriteLine(" [x] Received {0}", booking.ToString());
            };
            channel.BasicConsume(queue: "taxa",
                                 autoAck: true,
                                 consumer: consumer);

            while(true) {
                Thread.Sleep(5000);
            };
            
        }
    }
}