using Microsoft.AspNetCore.Mvc;

namespace customerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly ClaimService claimService;
 
    private readonly ILogger<ClaimsController> _logger;

    public ClaimsController(ILogger<ClaimsController> logger, ClaimService _claimService)
    {
        claimService = _claimService;
        _logger = logger;
    }

    [HttpPost(Name = "PostClaim")]
    public string PostClaim(Claim claim)
    {
        _logger.LogInformation("Metode ClaimService.PostClaim called at {DT}",
            DateTime.UtcNow.ToLongTimeString());
        try 
        {
            claimService.AddClaim(claim);
            return $"Claim has been received {claim.ClaimDescription}.";
        } 
        catch (Exception ex) 
        {
            LogError(ex);
            return "There was an error trying to make your claim. Try again.";
        }
       
    }  

    [HttpGet(Name = "GetAllClaims")]
    public List<Claim> GetAllClaims()
    {
        _logger.LogInformation("Metode ClaimService.GetAllClaims called at {DT}",
            DateTime.UtcNow.ToLongTimeString());
        return claimService.GetClaims();
    }     

    private void LogError(Exception ex) 
    {
        _logger.LogError(ex.Message);
    }
}
