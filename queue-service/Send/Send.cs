using System;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "172.17.0.2" };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            while(true) {
                Console.WriteLine("\nNavn på kunde: ");
                var name = Console.ReadLine();
                Console.WriteLine("Addresse for afhentning: ");
                var afhenting = Console.ReadLine();
                Console.WriteLine("Destination: ");
                var destination = Console.ReadLine();
                
                Console.WriteLine("Eksempel: MM/DD/YYYY hh:mm:ss \nTidspunkt for afhenting: "); 
                var datepickup = DateTime.Parse(Console.ReadLine());

                Booking booking = new Booking (
                    name, afhenting, destination, datepickup
                    );

                var body = JsonSerializer.SerializeToUtf8Bytes(booking);

                channel.BasicPublish(exchange: "",
                                    routingKey: "taxa",
                                    basicProperties: null,
                                    body: body);
                Console.WriteLine(" [x] Sent new booking");
            }
        }
    }
}