using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
namespace Ecommerce.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    
    private readonly PaymentService _paymentService;
    private readonly OrderService _orderService;

    public PaymentController(PaymentService paymentService,OrderService orderService)
    {
        _paymentService = paymentService;
        _orderService = orderService;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] decimal amount)
    {
        try
        {
            var result=await _paymentService.GetOrder(amount);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); 
        }
    }

    // Verify Razorpay Payment Signature
    [HttpPost("verify")]
    public async Task<IActionResult> VerifyPayment([FromBody] PaymentResponse paymentResponse)
    {
        try
        {
            bool isVerified=await _paymentService.IsPaymentSuccessful(paymentResponse);
            
            if (isVerified){
                return Ok(new { message = "Payment verified successfully" });
            }
            return BadRequest(new { message = "Invalid payment signature" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}



