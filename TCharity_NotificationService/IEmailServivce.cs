using TCharity_NotificationService.Models.EmailModels;

namespace TCharity_NotificationService;

public interface IEmailService
{
    Task SendPaymentConfirmationAsync(SendPaymentCaptureRequest request);
    Task SendDisburseNotificationAsync(SendDisburseNotificationRequest request);
}