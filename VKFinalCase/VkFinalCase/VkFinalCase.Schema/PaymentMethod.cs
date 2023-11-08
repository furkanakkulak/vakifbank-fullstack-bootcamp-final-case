namespace VkFinalCase.Schema;

public class PaymentMethodRequest
{
    public string Name { get; set; }
}
public class PaymentMethodResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Diğer özellikler eklenebilir
}