using Microsoft.AspNetCore.Mvc;

namespace BookingHandlerService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BookingHandlerServiceController : ControllerBase
{
    private readonly ILogger<BookingHandlerServiceController> _logger;
    private readonly BookingRepository _repository;

    public BookingHandlerServiceController(ILogger<BookingHandlerServiceController> logger, BookingRepository repository)
    {
        _logger = logger;
        _repository = repository;
    } 

    [HttpGet("/", Name = "Home")]
    public string Home()
    {
        return "Welcome to your taxa handler";
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
