using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("add")]
    public IActionResult AddProduct([FromBody] Product product)
    {
        if (product == null)
            return BadRequest(new { Message = "Product data is required." });

        bool isSuccess = _productService.AddProduct(product, out string errorMessage);

        if (isSuccess)
            return Ok(new { Message = "Product added successfully!" });
        else
            return StatusCode(500, new { Message = "Error adding product.", Details = errorMessage });
    }

    [HttpGet("get")]
    public IActionResult GetProducts()
    {
        List<Product> productlist = new List<Product>();
        try
        {
            productlist = _productService.getProducts();
        }
        catch
        {
            return StatusCode(500, new { Message = "Error getting products." });
        }
        return Ok(productlist);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
        {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", error = ex.Message });
        }
    }
}