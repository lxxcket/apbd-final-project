using APBDFinalProject.Contexts;
using APBDFinalProject.Exceptions;
using APBDFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDFinalProject.Repositories;

public class IndividualCustomerRepository : IIndividualCustomerRepository
{
    private IncomeContext _context;

    public IndividualCustomerRepository(IncomeContext context)
    {
        _context = context;
    }

    public async Task<int> AddCustomer(IndividualCustomer individualCustomer, CancellationToken cancellationToken)
    {
        await _context.IndividualCustomers.AddAsync(individualCustomer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return individualCustomer.Id;
    }

    public async Task<int> UpdateCustomer(int pesel, IndividualCustomer individualCustomer, CancellationToken cancellationToken)
    {
        var affectedRows = await _context.IndividualCustomers.Where(e => e.PESEL == pesel)
            .ExecuteUpdateAsync(updates =>
                updates.SetProperty(customer => customer.FirstName, individualCustomer.FirstName)
                    .SetProperty(customer => customer.LastName, individualCustomer.LastName)
                    .SetProperty(customer => customer.Email, individualCustomer.Email)
                    .SetProperty(customer => customer.Address, individualCustomer.Address)
                    .SetProperty(customer => customer.PhoneNumber, individualCustomer.PhoneNumber), cancellationToken);
        return affectedRows;
    }

    public async Task DeleteCustomer(int pesel, CancellationToken cancellationToken)
    {
        var customer = await _context.IndividualCustomers.FirstOrDefaultAsync(c => c.PESEL == pesel, cancellationToken);
        
        customer!.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IndividualCustomer?> GetIndividualCustomerByPesel(int pesel, CancellationToken cancellationToken)
    {
        return await _context.IndividualCustomers.FirstOrDefaultAsync(c => c.PESEL == pesel, cancellationToken);
    }
}