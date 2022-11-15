namespace customerAPI;
public class Claim
{ 
    public long ClaimId { get; set; } 
    public DateTime ClaimDate { get; set; } 
    public int CustomerId { get; set; } 
    public string? ProductName { get; set; } 
    public string? ProductSerialNo { get; set; } 
    public string? ClaimDescription { get; set; } 
} 
