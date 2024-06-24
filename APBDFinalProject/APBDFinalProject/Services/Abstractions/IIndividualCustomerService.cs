using APBDFinalProject.Models;
using APBDFinalProject.RequestModels;

namespace APBDFinalProject.Services.Abstractions;

public interface IIndividualCustomerService
{
    Task<int> AddCustomer(IndividualCustomerRequest individualCustomer, CancellationToken cancellationToken);
    Task<int> UpdateCustomer(int pesel, IndividualCustomerUpdateRequest individualCustomer, CancellationToken cancellationToken);
    Task DeleteCustomer(int pesel, CancellationToken cancellationToken);
}