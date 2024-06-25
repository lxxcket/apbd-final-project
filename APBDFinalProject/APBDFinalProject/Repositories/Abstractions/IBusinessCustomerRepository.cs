using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IBusinessCustomerRepository
{
    Task<int> AddCustomer(BusinessCustomer businessCustomer, CancellationToken cancellationToken);
    Task<int> UpdateCustomer(long krs, BusinessCustomer businessCustomer, CancellationToken cancellationToken);
    Task<BusinessCustomer?> GetBusinessCustomerByKRS(long krs, CancellationToken cancellationToken);
}