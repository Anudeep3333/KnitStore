using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ecommerce.Models;


namespace Ecommerce.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    // POST api/account/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        try
        {
            var user = await _userService.RegisterUser(registerRequest);
            return Ok(new { Message = "Registration successful", UserId = user.Id });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);  // Handle errors like username already taken or passwords mismatch
        }
    }
    
    // POST api/account/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var user = await _userService.LoginUser(loginRequest);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); 
        }
    }

}