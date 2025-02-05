using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] CartDto cartDto)
    {
        try
        {
            await _cartService.AddToCartAsync(cartDto.UserId, cartDto.ProductId, cartDto.Quantity);
            return Ok(new { message = "Product added to cart successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCartItems(int userId)
    {
        try
        {
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            return Ok(cartItems);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    
    [HttpPut("{id}")]
    public IActionResult UpdateCartItem(int id, [FromBody] Cart item)
    {
        var message = _cartService.UpdateCartItem(id, item.Quantity);
        if (message == "Cart item not found.")
        {
            return NotFound(new { message });
        }
        return Ok(new { message });
    }

    // Delete Cart Item
    [HttpDelete("{id}")]
    public IActionResult DeleteCartItem(int id)
    {
        var message = _cartService.DeleteCartItem(id);
        if (message == "Cart item not found.")
        {
            return NotFound(new { message });
        }
        return Ok(new { message });
    }   
}
