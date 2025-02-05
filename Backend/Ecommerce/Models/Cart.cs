namespace Ecommerce.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }

    public Product Product { get; set; }
    public User? User { get; set; }
}