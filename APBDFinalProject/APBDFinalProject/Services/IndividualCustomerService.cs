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
        await IndividualCustomerWithGivenPeselAlreadyExists(individualCustomer.PESEL, cancellationToken);
        
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

    public async Task<int> UpdateCustomer(int pesel, IndividualCustomerUpdateRequest individualCustomer,
        CancellationToken cancellationToken)
    {
        await IndividualCustomerWithGivenPeselDoesNotExist(pesel, cancellationToken);
        
        return await _individualCustomerRepository.UpdateCustomer(pesel, new IndividualCustomer()
        {
            FirstName = individualCustomer.FirstName,
            LastName = individualCustomer.LastName,
            Address = individualCustomer.Address,
            Email = individualCustomer.Email,
            PhoneNumber = individualCustomer.PhoneNumber
        }, cancellationToken);
    }

    public async Task DeleteCustomer(int pesel, CancellationToken cancellationToken)
    {
        await IndividualCustomerWithGivenPeselDoesNotExist(pesel, cancellationToken);
        
        await _individualCustomerRepository.DeleteCustomer(pesel, cancellationToken);
    }

    private async Task IndividualCustomerWithGivenPeselDoesNotExist(int pesel, CancellationToken cancellationToken)
    {
        IndividualCustomer? individualCustomer = 
            await _individualCustomerRepository.GetIndividualCustomerByPesel(pesel, cancellationToken);
        if (individualCustomer == null)
        {
            throw new DomainException("Individual customer with given PESEL was not found");
        }
    }

    private async Task IndividualCustomerWithGivenPeselAlreadyExists(int pesel, CancellationToken cancellationToken)
    {
        IndividualCustomer? individualCustomer =
            await _individualCustomerRepository.GetIndividualCustomerByPesel(pesel, cancellationToken);
        if (individualCustomer != null)
        {
            throw new DomainException("Individual customer with given PESEL already exists in the system");
        }
    }
}