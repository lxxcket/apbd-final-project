using APBDFinalProject.Contexts;
using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IContractRepository
{
    Task<Contract> CreateContract(Contract contract, CancellationToken cancellationToken);
    Task<Contract?> GetContractByIdAndCustomerId(int contractId, int customerId, CancellationToken cancellationToken);

    Task UpdateContractAmountPaid(Contract contract, decimal amount, CancellationToken cancellationToken);
    Task MakeContractPaid(Contract contract, CancellationToken cancellationToken);
    
    Task<List<Contract>> GetAllPaidContracts(CancellationToken cancellationToken);
    Task<List<Contract>> GetAllContracts(CancellationToken cancellationToken);
}