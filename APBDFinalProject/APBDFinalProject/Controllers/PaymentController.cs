using APBDFinalProject.Exceptions;
using APBDFinalProject.RequestModels;
using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBDFinalProject.Controllers;



[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [Authorize(Roles = "user,admin")]
    [HttpPost]
    public async Task<IActionResult> MakePayment(PaymentRequest request, CancellationToken cancellationToken)
    { 
        await _paymentService.MakePayment(request, cancellationToken);
        return Ok();
    }
}