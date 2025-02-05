using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }  // Store the hashed password, not plain text
    public DateTime DateCreated { get; set; }  // Timestamp for when the user was created
    public string Role { get; set; } = "Customer";
}