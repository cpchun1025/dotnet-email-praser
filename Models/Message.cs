using System;

public class Message
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Sender { get; set; }
    public string Content { get; set; }
    public DateTime ReceivedAt { get; set; }
}