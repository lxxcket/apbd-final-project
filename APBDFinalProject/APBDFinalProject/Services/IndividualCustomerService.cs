using APBDFinalProject.Exceptions;
using APBDFinalProject.Models;
using APBDFinalProject.Repositories;
using APBDFinalProject.RequestModels;
using APBDFinalProject.Services.Abstractions;

namespace APBDFinalProject.Services;

public class IndividualCustomerService : IIndividualCustomerService
{
    private IIndividualCustomerRepository _individualCustomerRepository;

    public IndividualCustomerService(IIndividualCustomerRepository individualCustomerRepository)
    {
        _individualCustomerRepository = individualCustomerRepository;
    }

    public async Task<int> AddCustomer(IndividualCustomerRequest individualCustomer,
        CancellationToken cancellationToken)
    {
        return await _individualCustomerRepository.AddCustomer(new IndividualCustomer()
        {
            PESEL = individualCustomer.PESEL,
            FirstName = individualCustomer.FirstName,
            LastName = individualCustomer.LastName,
            Address = individualCustomer.Address,
            Email = individualCustomer.Email,
            PhoneNumber = individualCustomer.PhoneNumber
        }, cancellationToken);
    }

    public async Task<int> UpdateCustomer(int id, IndividualCustomerUpdateRequest individualCustomer,
        CancellationToken cancellationToken)
    {
        return await _individualCustomerRepository.UpdateCustomer(id, new IndividualCustomer()
        {
            FirstName = individualCustomer.FirstName,
            LastName = individualCustomer.LastName,
            Address = individualCustomer.Address,
            Email = individualCustomer.Email,
            PhoneNumber = individualCustomer.PhoneNumber
        }, cancellationToken);
    }

    public async Task DeleteCustomer(int id, CancellationToken cancellationToken)
    {
        await _individualCustomerRepository.DeleteCustomer(id, cancellationToken);
    }
}