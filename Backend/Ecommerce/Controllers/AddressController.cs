using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController: ControllerBase
{
    private readonly AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("{userId}")]
    public async Task<List<Address>> GetAddresses(int userId)
    {
        try
        {
            return await _addressService.GetAddresses(userId);
        }
        catch (Exception ex)
        {
            return new List<Address>();
        }
    }

    [HttpPost("add")] 
    public async Task<IActionResult> AddAddress([FromBody] AddressRequest addressRequest)
    {
        try
        {
            Address result = await _addressService.AddAddress(addressRequest);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}