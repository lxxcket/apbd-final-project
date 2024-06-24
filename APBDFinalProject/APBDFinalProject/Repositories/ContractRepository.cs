using APBDFinalProject.Contexts;
using APBDFinalProject.Models;

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
}