using APBDFinalProject.Contexts;
using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private IncomeContext _context;

    public PaymentRepository(IncomeContext context)
    {
        _context = context;
    }

    public async Task AddPayment(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }
}