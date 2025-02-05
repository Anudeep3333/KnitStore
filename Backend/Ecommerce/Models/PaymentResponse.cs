namespace Ecommerce.Services;

public class PaymentResponse
{
    public string OrderId { get; set; }
    public string PaymentId { get; set; }
    public string Signature { get; set; }
    public decimal Amount { get; set; }
    public int  AddressId { get; set; }
    public int UserId { get; set; }
}