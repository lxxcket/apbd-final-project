using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IPaymentRepository
{
    Task AddPayment(Payment payment);
}