using APBDFinalProject.Exceptions;
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
        await BusinessCustomerWithGivenKrsAlreadyExists(businessCustomer.KRS, cancellationToken);
        return await _businessCustomerRepository.AddCustomer(new BusinessCustomer()
        {
            Address = businessCustomer.Address,
            BusinessName = businessCustomer.BusinessName,
            KRS = businessCustomer.KRS,
            Email = businessCustomer.Email,
            PhoneNumber = businessCustomer.PhoneNumber
        }, cancellationToken);
    }

    public async Task<int> UpdateCustomer(int krs, BusinessCustomerUpdateRequest businessCustomerUpdateRequest, CancellationToken cancellationToken)
    {
        await BusinessCustomerWithGivenKrsDoesNotExist(krs, cancellationToken);
        return await _businessCustomerRepository.UpdateCustomer(krs, new BusinessCustomer()
        {
            Address = businessCustomerUpdateRequest.Address,
            BusinessName = businessCustomerUpdateRequest.BusinessName,
            Email = businessCustomerUpdateRequest.Email,
            PhoneNumber = businessCustomerUpdateRequest.PhoneNumber
        }, cancellationToken);
    }

    private async Task BusinessCustomerWithGivenKrsDoesNotExist(int krs, CancellationToken cancellationToken)
    {
        BusinessCustomer? customer = await _businessCustomerRepository.GetBusinessCustomerByKRS(krs, cancellationToken);
        if (customer == null)
        {
            throw new DomainException("Business customer with given KRS does not exist in the system");
        }
    }

    private async Task BusinessCustomerWithGivenKrsAlreadyExists(int krs, CancellationToken cancellationToken)
    {
        BusinessCustomer? customer = await _businessCustomerRepository.GetBusinessCustomerByKRS(krs, cancellationToken);
        if (customer!= null)
        {
            throw new DomainException("Business customer with given KRS already exists");
        }
    }
}