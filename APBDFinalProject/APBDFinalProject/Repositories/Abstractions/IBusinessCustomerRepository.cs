using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IBusinessCustomerRepository
{
    Task<int> AddCustomer(BusinessCustomer businessCustomer, CancellationToken cancellationToken);
    Task<int> UpdateCustomer(int id, BusinessCustomer businessCustomer, CancellationToken cancellationToken);
}