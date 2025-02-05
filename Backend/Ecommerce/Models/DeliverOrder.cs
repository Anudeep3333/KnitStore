using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public class DeliverOrder
{
    [Key]
    public int Id { get; set; }
    public string UserId{ get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public string DeliveryStatus { get; set; }
}