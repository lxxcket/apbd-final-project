using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IIndividualCustomerRepository
{
    Task<int> AddCustomer(IndividualCustomer individualCustomer, CancellationToken cancellationToken);
    Task<int> UpdateCustomer(int pesel, IndividualCustomer individualCustomer, CancellationToken cancellationToken);
    Task DeleteCustomer(int pesel, CancellationToken cancellationToken);
    Task<IndividualCustomer?> GetIndividualCustomerByPesel(int pesel, CancellationToken cancellationToken);
}