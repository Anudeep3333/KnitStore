using Ecommerce.Models;

namespace Ecommerce.Services;

public class OrderService
{
    private readonly EcommerceDbContext _context;
    private readonly UserService _userService;

    public OrderService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderByIdAsync(string orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }

    public async Task UpdateOrderAsync(Order order)
    {

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
    
    public async Task MoveProductsToDeliverOrderAsync(string userId)
    {
        // Fetch products from the Cart table for the user
        var cartItems = _context.Cart.Where(c => c.UserId.ToString() == userId).ToList();

        if (!cartItems.Any())
            throw new Exception("Cart is empty. Cannot proceed with the order.");

        foreach (var cartItem in cartItems)
        {
            // Create a new delivery order entry
            var deliverOrder = new DeliverOrder
            {
                UserId = userId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                OrderDate = DateTime.UtcNow,
                DeliveryStatus = "Pending" // Default status
            };

            // Add to DeliverOrder table
            _context.DeliverOrders.Add(deliverOrder);
        }

        // Remove the items from the Cart table
        _context.Cart.RemoveRange(cartItems);

        // Save changes to the database
        await _context.SaveChangesAsync();
    }
}
