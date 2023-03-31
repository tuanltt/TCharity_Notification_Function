namespace TCharity_NotificationService;

public class SendGridOption
{
    public string ApiKey { get; set; } = default!;
    public string FromEmail { get; set; } = default!;
    public string FromName { get; set; } = default!;
    
}