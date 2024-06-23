using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IIndividualCustomerRepository
{
    Task<int> AddCustomer(IndividualCustomer individualCustomer, CancellationToken cancellationToken);
    Task<int> UpdateCustomer(int id, IndividualCustomer individualCustomer, CancellationToken cancellationToken);
    Task DeleteCustomer(int id, CancellationToken cancellationToken);
}