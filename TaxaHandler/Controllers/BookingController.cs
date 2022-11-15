using Microsoft.AspNetCore.Mvc;
using TaxaHandler.Repository;

namespace TaxaHandler.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingHandlerController : ControllerBase
{
    private readonly BookingRepository _repository;
    private readonly ILogger<BookingHandlerController> _logger;

    public BookingHandlerController(ILogger<BookingHandlerController> logger, BookingRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("/", Name = "Home")]
    public string Home()
    {
        return "Welcome to homepage";
    }

    [HttpGet(Name = "Index")]
    public string Index()
    {
        return "Welcome to your booking handler service controller";
    }

    [HttpGet("bookings", Name = "bookings")]    
    public List<BookingDTO> Get()
    {
        return _repository.GetBookings();
    }
}
