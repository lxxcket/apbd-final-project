using APBDFinalProject.Contexts;
using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IContractRepository
{
    Task<Contract> CreateContract(Contract contract, CancellationToken cancellationToken);

}