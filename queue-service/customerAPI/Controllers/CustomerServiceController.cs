using Microsoft.AspNetCore.Mvc;
using customerAPI;
using System.Linq;

namespace customerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerServiceController : ControllerBase
{
    private List<Customer> _customers = new List<Customer>() {
        new Customer() {
        Id = 1,
        Name = "International Bicycles A/S",
        Address1 = "Nydamsvej 8",
        Address2 = null,
        PostalCode = 8362,
        City = "Hørning",
        TaxNumber = "DK-75627732"
        }
    };

    private readonly ILogger<CustomerServiceController> _logger;

    public CustomerServiceController(ILogger<CustomerServiceController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{customerId:int}", Name = "GetCustomerById")]
    public Customer GetCustomerById(int customerId)
    {
         _logger.LogInformation("Metode CustomerService.GetCustomerById called at {DT}",
            DateTime.UtcNow.ToLongTimeString());
        try {
            return _customers.Where(c => c.Id == customerId).First();
        } catch {
            return new Customer {};
        }
    }
}
