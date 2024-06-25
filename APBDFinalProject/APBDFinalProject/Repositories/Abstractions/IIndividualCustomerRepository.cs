using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IIndividualCustomerRepository
{
    Task<int> AddCustomer(IndividualCustomer individualCustomer, CancellationToken cancellationToken);
    Task<int> UpdateCustomer(long pesel, IndividualCustomer individualCustomer, CancellationToken cancellationToken);
    Task DeleteCustomer(long pesel, CancellationToken cancellationToken);
    Task<IndividualCustomer?> GetIndividualCustomerByPesel(long pesel, CancellationToken cancellationToken);
}