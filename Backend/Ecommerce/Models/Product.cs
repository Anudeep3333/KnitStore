using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string SKU { get; set; }
    public decimal Discount { get; set; }
    public decimal Rating { get; set; }
    public int ReviewsCount { get; set; }
    public string Specifications { get; set; } // JSON formatted string
    public bool IsFeatured { get; set; }
    public string AvailabilityStatus { get; set; } = "In Stock";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
