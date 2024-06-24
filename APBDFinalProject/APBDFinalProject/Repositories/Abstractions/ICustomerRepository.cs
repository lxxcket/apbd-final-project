using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface ICustomerRepository
{
    Task<bool> HasContractWithSoftware(int customerId, int versionId, CancellationToken cancellationToken);
    Task<Customer?> GetCustomerById(int id, CancellationToken cancellationToken);
    Task<bool> HadAnyPaidContract(int customerId,CancellationToken cancellationToken);
}