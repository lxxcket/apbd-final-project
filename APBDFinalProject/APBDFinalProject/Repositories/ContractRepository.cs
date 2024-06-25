using APBDFinalProject.Contexts;
using APBDFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDFinalProject.Repositories;

public class ContractRepository : IContractRepository
{
    private IncomeContext _context;

    public ContractRepository(IncomeContext context)
    {
        _context = context;
    }

    public async Task<Contract> CreateContract(Contract contract, CancellationToken cancellationToken)
    {
        await _context.Contracts.AddAsync(contract, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return contract;
    }

    public async Task<Contract?> GetContractByIdAndCustomerId(int contractId, int customerId, CancellationToken cancellationToken)
    {
        return await _context.Contracts.FirstOrDefaultAsync(c => c.Id == contractId && c.IdCustomer == customerId, cancellationToken);
    }

    public async Task UpdateContractAmountPaid(Contract contract,  decimal amount, CancellationToken cancellationToken)
    {
        contract.AmountPaid = amount;
        _context.Contracts.Update(contract);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task MakeContractPaid(Contract contract, CancellationToken cancellationToken)
    {
        contract.IsPaid = true;
        _context.Contracts.Update(contract);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Contract>> GetAllPaidContracts(CancellationToken cancellationToken)
    {
        return await _context.Contracts
            .Where(c => c.IsPaid)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Contract>> GetAllContracts(CancellationToken cancellationToken)
    {
        return await _context.Contracts.ToListAsync(cancellationToken);
    }
}