namespace Ecommerce.Models;

public class AddressRequest
{
    public int UserId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }  // Store the hashed password, not plain text
    public string State { get; set; }  // Timestamp for when the user was created
    public int Pincode { get; set; } 
    
}