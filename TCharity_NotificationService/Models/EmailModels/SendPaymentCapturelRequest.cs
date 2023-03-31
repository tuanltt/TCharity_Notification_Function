namespace TCharity_NotificationService.Models.EmailModels;

public class SendPaymentCaptureRequest : SendEmailBaseRequest
{
    public string Subject { get; set; } = "TCharity Donation Payment Capture";
    public long Amount { get; set; }
    public string Body { get; set; }
    public string PostUrl { get; set; }


}