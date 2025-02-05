using System.Text;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Services;

public class PaymentService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OrderService _orderService;
    private readonly AddressService _addressService;
    private readonly UserService _userService;
    
    // Razorpay Test API Key and Secret
    private readonly string _razorpayKeyId = "rzp_test_ouVr2uLbwcqVkb";  // Use your actual test key
    private readonly string _razorpayKeySecret = "VA5ufUUOF3OPZIrUCZdAebfj";  // Use your actual test secret key

    public PaymentService(IHttpClientFactory httpClientFactory,OrderService orderService,AddressService addressService)
    {
        _httpClientFactory = httpClientFactory;
        _orderService = orderService;
        _addressService = addressService;
        //_userService = userService;
    }
    
    public async Task<string> GetOrder(decimal amt)
    {
        // Razorpay order API endpoint
        var url = "https://api.razorpay.com/v1/orders";

        // Create the request body
        var orderData = new
        {
            amount = amt,  
            currency = "INR",  // Currency
            receipt = "order_receipt_1",
            payment_capture = 1, 
            
        };

        // Serialize data to JSON
        var jsonData = JsonConvert.SerializeObject(orderData);

        // Initialize HttpClient
        var client = _httpClientFactory.CreateClient();

        // Set up the HTTP request
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
        };

        // Add basic authentication using Razorpay key ID and secret
        client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic", 
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_razorpayKeyId}:{_razorpayKeySecret}"))
            );

        // Make the API call to Razorpay
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            return error;
        }
    }

    public async Task<bool> IsPaymentSuccessful(PaymentResponse paymentResponse)
    {
        //User user=await _userService.GetUser(paymentResponse.UserId);
        Address address= await _addressService.GetAddress(paymentResponse.AddressId);
        
        // Generate the signature using Razorpay's guidelines
        string generatedSignature = GenerateSignature(paymentResponse.OrderId, paymentResponse.PaymentId);

        // Compare the generated signature with the received signature
        if (generatedSignature == paymentResponse.Signature)
        {
            var order = await _orderService.GetOrderByIdAsync(paymentResponse.OrderId);
            if (order != null)
            {
                order.PaymentId = paymentResponse.PaymentId;
                order.PaymentStatus = "Paid";
                order.OrderDate = DateTime.Now;
                order.OrderId = paymentResponse.OrderId;
                order.Email = "dummy@email.com";
                order.Phone = 393193939;
                order.Address = address.Street;
                order.Amount = paymentResponse.Amount;
                order.City = address.City;
                order.State = address.State;
                order.PostalCode = address.Pincode;
                
                await _orderService.UpdateOrderAsync(order);
            }
            return true;
        }
        return false;
    }
    private string GenerateSignature(string orderId, string paymentId)
    {
        // Concatenate OrderId and PaymentId
        var rawString = orderId + "|" + paymentId;
        // Compute HMACSHA256
        using (var hmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes( _razorpayKeySecret)))
        {
            var hashBytes = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawString));
            // Convert to hexadecimal string
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}