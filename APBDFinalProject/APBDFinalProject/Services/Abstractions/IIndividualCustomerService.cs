using APBDFinalProject.Models;
using APBDFinalProject.RequestModels;

namespace APBDFinalProject.Services.Abstractions;

public interface IIndividualCustomerService
{
    Task<int> AddCustomer(IndividualCustomerRequest individualCustomer, CancellationToken cancellationToken);
    Task<int> UpdateCustomer(long pesel, IndividualCustomerUpdateRequest individualCustomer, CancellationToken cancellationToken);
    Task DeleteCustomer(long pesel, CancellationToken cancellationToken);
}