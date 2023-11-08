namespace VkFinalCase.Schema;

public class UserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
}

public class UserResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public DealerResponse Dealer { get; set; }
    public List<OrderResponse> Orders { get; set; }
    public List<MessageResponse> SentMessages { get; set; }
    public List<MessageResponse> ReceivedMessages { get; set; }
}