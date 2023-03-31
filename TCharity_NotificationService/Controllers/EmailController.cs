using Microsoft.AspNetCore.Mvc;
using TCharity_NotificationService.Models.EmailModels;

namespace TCharity_NotificationService.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailController> _logger;

    public EmailController(IEmailService emailService, ILogger<EmailController> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    [HttpPost("donate-payment-capture")]
    public async Task<IActionResult> SendDonationPaymentCapture(SendPaymentCaptureRequest request)
    {
        await _emailService.SendPaymentConfirmationAsync(request);
        return Ok();
    }
}