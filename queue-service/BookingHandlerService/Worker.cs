using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace WorkerService;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly BookingRepository _repository;
    public Worker(ILogger<Worker> logger,  BookingRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            try 
            {
                FindNewBookings();
                _logger.LogInformation("No more new bookings at time {time}", DateTimeOffset.Now);
            } 
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            };

            await Task.Delay(10000, stoppingToken);
        }
        _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
    }

    public void FindNewBookings() {
        var factory = new ConnectionFactory() { HostName = "172.17.0.2" };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            _logger.LogInformation(" Connection established at: {time}", DateTimeOffset.Now);
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
                BookingDTO? booking = JsonSerializer.Deserialize<BookingDTO>(message);
                booking.ID = id++;
                booking.TidStempel = DateTime.Now;

                _repository.AddBooking(booking);
                Console.WriteLine(" [x] Received {0}", booking.ToString());
            };
            channel.BasicConsume(queue: "taxa",
                                 autoAck: true,
                                 consumer: consumer);

        }
    }
}