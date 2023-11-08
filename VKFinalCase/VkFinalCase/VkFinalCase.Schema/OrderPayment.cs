namespace VkFinalCase.Schema;

public class OrderPaymentRequest
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    // public DateTime PaymentDate { get; set; }
}

public class OrderPaymentResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public OrderResponse Order { get; set; }
}
