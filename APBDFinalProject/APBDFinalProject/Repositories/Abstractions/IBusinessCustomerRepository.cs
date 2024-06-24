using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IBusinessCustomerRepository
{
    Task<int> AddCustomer(BusinessCustomer businessCustomer, CancellationToken cancellationToken);
    Task<int> UpdateCustomer(int krs, BusinessCustomer businessCustomer, CancellationToken cancellationToken);
    Task<BusinessCustomer?> GetBusinessCustomerByKRS(int krs, CancellationToken cancellationToken);
}