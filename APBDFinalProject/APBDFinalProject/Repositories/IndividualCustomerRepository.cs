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

    public async Task<int> UpdateCustomer(int id, IndividualCustomer individualCustomer, CancellationToken cancellationToken)
    {
        var affectedRows = await _context.IndividualCustomers.Where(e => e.Id == id)
            .ExecuteUpdateAsync(updates =>
                updates.SetProperty(customer => customer.FirstName, individualCustomer.FirstName)
                    .SetProperty(customer => customer.LastName, individualCustomer.LastName)
                    .SetProperty(customer => customer.Email, individualCustomer.Email)
                    .SetProperty(customer => customer.Address, individualCustomer.Address)
                    .SetProperty(customer => customer.PhoneNumber, individualCustomer.PhoneNumber), cancellationToken);
        return affectedRows;
    }

    public async Task DeleteCustomer(int id, CancellationToken cancellationToken)
    {
        var customer = await _context.IndividualCustomers.FindAsync(id);
        if (customer == null)
        {
            throw new DomainException("Customer with given id was not found");
        }

        customer.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}