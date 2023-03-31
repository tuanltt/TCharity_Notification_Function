namespace TCharity_NotificationService.Models.EmailModels;

public class SendEmailBaseRequest
{
    public string ToEmail { get; set; }
    public string ToEmailName { get; set; }

    public IEnumerable<string> CCEmails { get; set; } = new List<string>();
    public string Subject { get; set; }

}