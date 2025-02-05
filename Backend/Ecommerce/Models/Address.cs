using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public class Address
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Street { get; set; }
    public string City { get; set; } 
    public string State { get; set; } 
    public int Pincode { get; set; } 
}

