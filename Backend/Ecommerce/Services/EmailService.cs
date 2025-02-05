using Ecommerce.Models;

namespace Ecommerce.Services;

public class EmailService
{
    public async Task SendPaymentConfirmationAsync(Order order)
    {
        // Simulate sending email (replace with actual implementation)
        string subject = $"Payment Confirmation for Order #{order.Id}";
        string body = $"""
                           Dear {order.CustomerName},
                       
                           Your payment of ₹{order.Amount} for Order #{order.Id} has been successfully processed.
                       
                           Payment ID: {order.PaymentId}
                           Order Date: {order.OrderDate}
                       
                           Thank you for your purchase!
                       
                           Regards,
                           Your Company Name
                       """;

        // Simulated email sending logic (use SMTP or third-party service like SendGrid, MailKit, etc.)
        Console.WriteLine("Sending email...");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");

        await Task.CompletedTask;
    }
}