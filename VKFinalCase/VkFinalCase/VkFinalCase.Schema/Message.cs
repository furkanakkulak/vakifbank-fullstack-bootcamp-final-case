namespace VkFinalCase.Schema;

public class MessageRequest
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }
    public DateTime SentDate { get; set; }
}

public class MessageResponse
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }
    public DateTime SentDate { get; set; }
    public UserResponse Sender { get; set; }
    public UserResponse Receiver { get; set; }
}