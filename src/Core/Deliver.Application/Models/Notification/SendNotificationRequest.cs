namespace Deliver.Application.Models.Notification;

public class SendNotificationRequest
{
    public required string Token { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required IReadOnlyDictionary<string, string> Data { get; set; }
}