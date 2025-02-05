using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public string OrderId { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Decimal Amount { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int PostalCode { get; set; }
    public string Country { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; }
    public string PaymentId{ get; set; }
    public string PaymentStatus { get; set; }
    public DateTime OrderDate { get; set; }
}