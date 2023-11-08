namespace VkFinalCase.Schema;

public class TokenRequest
{
    public string Username  { get; set; }
    public string Password { get; set; }
}

public class TokenResponse
{
    public DateTime ExpireDate { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}
