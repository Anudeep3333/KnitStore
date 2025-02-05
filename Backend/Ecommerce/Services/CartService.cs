using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class CartService
{
    
    private readonly EcommerceDbContext _dbContext;

    public CartService(EcommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        
        var existingCartItem = _dbContext.Cart
            .Include(c => c.Product) // Include the Product
            .FirstOrDefault(c => c.Id == productId && c.UserId == userId);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity += quantity;
            existingCartItem.ProductPrice = existingCartItem.Product.Price * existingCartItem.Quantity;
            _dbContext.Cart.Update(existingCartItem);
        }
        else
        {
            var product = await _dbContext.Products.FindAsync(productId);
            var cartItem = new Cart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                ProductPrice =  product?.Price ?? 0,
            };
            await _dbContext.Cart.AddAsync(cartItem);
        }

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Cart>> GetCartItemsAsync(int userId)
    {
        return await _dbContext.Cart
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public string DeleteCartItem(int id)
    {
        var cartItem = _dbContext.Cart.Find(id);

        if (cartItem == null)
        {
            return "Cart item not found.";
        }

        _dbContext.Cart.Remove(cartItem);
        _dbContext.SaveChanges();

        return "Cart item deleted successfully.";
    }
    
    public string UpdateCartItem(int id, int quantity)
    {
        var existingItem = _dbContext.Cart
            .Include(c => c.Product) // Include the Product
            .FirstOrDefault(c => c.Id == id);

        if (existingItem == null || existingItem.Product == null)
        {
            return "Cart item or associated product not found.";
        }


        existingItem.Quantity = quantity;
        existingItem.ProductPrice = existingItem.Product.Price*quantity;
        _dbContext.Cart.Update(existingItem);
        _dbContext.SaveChanges();

        return existingItem.ProductPrice.ToString();
    }

    
}