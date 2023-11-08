namespace VkFinalCase.Schema;

public class OrderRequest
{
    public int DealerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    // public DateTime OrderDate { get; set; }
    // public string Status { get; set; }
    public int PaymentMethodId { get; set; }
}

public class OrderResponse
{
    public int Id { get; set; }
    public int DealerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public PaymentMethodResponse PaymentMethod { get; set; }

}
