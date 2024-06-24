using APBDFinalProject.Contexts;
using APBDFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDFinalProject.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private IncomeContext _context;

    public CustomerRepository(IncomeContext context)
    {
        _context = context;
    }

    public async Task<bool> HasContractWithSoftware(int customerId, int versionId, CancellationToken cancellationToken)
    {
        return  await _context.Customers
            .AnyAsync(c => c.Id == customerId &&
                           c.Contracts.Any(contract => contract.IdVersion == versionId && !contract.IsPaid), cancellationToken);
    }

    public async Task<Customer?> GetCustomerById(int id, CancellationToken cancellationToken)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> HadAnyPaidContract(int customerId, CancellationToken cancellationToken)
    {
        return await _context.Customers.AnyAsync(c => c.Id == customerId &&
                                                      c.Contracts.Any(contract => contract.IsPaid), cancellationToken);
    }
}