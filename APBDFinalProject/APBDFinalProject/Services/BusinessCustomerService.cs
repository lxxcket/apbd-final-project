using APBDFinalProject.Models;
using APBDFinalProject.Repositories;
using APBDFinalProject.RequestModels;
using APBDFinalProject.Services.Abstractions;

namespace APBDFinalProject.Services;

public class BusinessCustomerService : IBusinessCustomerService
{
    private IBusinessCustomerRepository _businessCustomerRepository;

    public BusinessCustomerService(IBusinessCustomerRepository businessCustomerRepository)
    {
        _businessCustomerRepository = businessCustomerRepository;
    }

    public async Task<int> AddCustomer(BusinessCustomerRequest businessCustomer, CancellationToken cancellationToken)
    {
        return await _businessCustomerRepository.AddCustomer(new BusinessCustomer()
        {
            Address = businessCustomer.Address,
            BusinessName = businessCustomer.BusinessName,
            KRS = businessCustomer.KRS,
            Email = businessCustomer.Email,
            PhoneNumber = businessCustomer.PhoneNumber
        }, cancellationToken);
    }

    public async Task<int> UpdateCustomer(int id, BusinessCustomerUpdateRequest businessCustomerUpdateRequest, CancellationToken cancellationToken)
    {
        return await _businessCustomerRepository.UpdateCustomer(id, new BusinessCustomer()
        {
            Address = businessCustomerUpdateRequest.Address,
            BusinessName = businessCustomerUpdateRequest.BusinessName,
            Email = businessCustomerUpdateRequest.Email,
            PhoneNumber = businessCustomerUpdateRequest.PhoneNumber
        }, cancellationToken);
    }
}