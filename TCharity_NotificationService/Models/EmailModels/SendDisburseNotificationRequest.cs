namespace TCharity_NotificationService.Models.EmailModels;

public class SendDisburseNotificationRequest : SendEmailBaseRequest
{
    public string PostUrl { get; set; }

    public long TotalAmount { get; set; }
}