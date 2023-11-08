namespace VkFinalCase.Schema;

public class DealerRequest
{
    public int UserId { get; set; }
    public string Address { get; set; }
    public string TaxNumber { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Margin { get; set; }
}

public class DealerResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Address { get; set; }
    public string TaxNumber { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Margin { get; set; }

    public List<OrderResponse> Orders { get; set; }
}
