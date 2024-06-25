using APBDFinalProject.RequestModels;

namespace APBDFinalProject.Services.Abstractions;

public interface IBusinessCustomerService
{
    Task<int> AddCustomer(BusinessCustomerRequest businessCustomer, 
        CancellationToken cancellationToken);

    Task<int> UpdateCustomer(long krs, BusinessCustomerUpdateRequest businessCustomerUpdateRequest, 
        CancellationToken cancellationToken);
}