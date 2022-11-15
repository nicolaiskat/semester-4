namespace customerAPI;
public class ClaimService
{ 
    private List<Claim> claims = new();

    public void AddClaim(Claim claim) 
    {
        claims.Add(claim);
    }

    public List<Claim> GetClaims() 
    {
        return claims;
    }
} 
