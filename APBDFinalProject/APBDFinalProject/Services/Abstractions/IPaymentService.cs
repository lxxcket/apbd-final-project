using APBDFinalProject.RequestModels;

namespace APBDFinalProject.Services.Abstractions;

public interface IPaymentService
{
    Task MakePayment(PaymentRequest paymentRequest, CancellationToken cancellationToken);
}