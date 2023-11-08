namespace VkFinalCase.Schema;

public class ProductRequest 
{
    public string Name { get; set; } 
    public decimal Price { get; set; } 
    public int StockQuantity { get; set; } 
    public int MinStockQuantity { get; set; }
}
public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int MinStockQuantity { get; set; }
    public List<OrderResponse> Orders { get; set; }
}