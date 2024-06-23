using APBDFinalProject.Contexts;
using APBDFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDFinalProject.Repositories;

public class BusinessCustomerRepository : IBusinessCustomerRepository
{
    private IncomeContext _context;

    public BusinessCustomerRepository(IncomeContext context)
    {
        _context = context;
    }

    public async Task<int> AddCustomer(BusinessCustomer businessCustomer, CancellationToken cancellationToken)
    {
        await _context.BusinessCustomers.AddAsync(businessCustomer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return businessCustomer.Id;
    }

    public async Task<int> UpdateCustomer(int id, BusinessCustomer businessCustomer, CancellationToken cancellationToken)
    {
        var affectedRows = await _context.BusinessCustomers.Where(e => e.Id == id)
            .ExecuteUpdateAsync(updates =>
                updates.SetProperty(customer => customer.BusinessName, businessCustomer.BusinessName)
                    .SetProperty(customer => customer.Email, businessCustomer.Email)
                    .SetProperty(customer => customer.Address, businessCustomer.Address)
                    .SetProperty(customer => customer.PhoneNumber, businessCustomer.PhoneNumber), cancellationToken);
        return affectedRows;
    }
}