using APBDFinalProject.Exceptions;
using APBDFinalProject.Models;
using APBDFinalProject.Repositories;
using APBDFinalProject.RequestModels;
using APBDFinalProject.ResponseModels;
using APBDFinalProject.Services.Abstractions;
using Version = APBDFinalProject.Models.Version;

namespace APBDFinalProject.Services;

public class ContractService : IContractService
{
    private IContractRepository _contractRepository;
    private IDiscountRepository _discountRepository;
    private IVersionRepository _versionRepository;
    private ICustomerRepository _customerRepository;

    public ContractService(IContractRepository contractRepository, 
        IDiscountRepository discountRepository, 
        IVersionRepository versionRepository,
        ICustomerRepository customerRepository)
    {
        _contractRepository = contractRepository;
        _discountRepository = discountRepository;
        _versionRepository = versionRepository;
        _customerRepository = customerRepository;

    }

    public async Task<ContractResponse> CreateContract(ContractRequest request, CancellationToken cancellationToken)
    {
        ContractStartDateInPast(request.StartDate);
        Customer customer = await CustomerExistsAndRetrieve(request.IdCustomer, cancellationToken);
        Version version = await VersionExistsAndRetrieve(request.IdVersion, cancellationToken);
        CustomerHaveUnpaidContractWithSameSoftware(customer.Id, version.Id, cancellationToken);
        decimal discount = await GetBestDiscount(customer, cancellationToken);
        decimal contractFullPrice = CalculateFullPrice(version.Software.YearlyPrice, request.SupportTime, discount);

        Contract contract = await _contractRepository.CreateContract(new Contract()
        {
            IdCustomer = request.IdCustomer,
            IdVersion = request.IdVersion,
            TotalContractPrice = contractFullPrice,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            DaysSpan = request.DaysSpan,
            SupportTime = request.SupportTime,
            IsPaid = false,
            IsSigned = false
        }, cancellationToken);

       return new ContractResponse()
       {
           Id = contract.Id,
           IdCustomer = contract.IdCustomer,
           IdVersion = contract.IdVersion,
           TotalContractPrice = contract.TotalContractPrice,
           StartDate = contract.StartDate,
           EndDate = contract.EndDate,
           DaysSpan = contract.DaysSpan,
           SupportTime = contract.SupportTime,
           IsPaid = contract.IsPaid,
           IsSigned = contract.IsSigned
       };
    }

    private void ContractStartDateInPast(DateTime startDate)
    {
        if (startDate < DateTime.Now)
        {
            throw new DomainException("Start date of the contract can't be in the past");
        }
    }

    private async Task<Customer> CustomerExistsAndRetrieve(int id, CancellationToken cancellationToken)
    {
       Customer? customer = await _customerRepository.GetCustomerById(id,cancellationToken);
       if (customer == null)
       {
           throw new DomainException("Customer with given id was not found");
       }
       return customer;
    }

    private async Task<Version> VersionExistsAndRetrieve(int versionId, CancellationToken cancellationToken)
    {
        Version? version = await _versionRepository.GetVersionWithSoftware(versionId, cancellationToken);
        if (version == null)
        {
            throw new DomainException("Version with given id was not found");
        }

        return version;
    }

    private async void CustomerHaveUnpaidContractWithSameSoftware(int customerId, int versionId, CancellationToken cancellationToken)
    {
        if(await _customerRepository.HasContractWithSoftware(customerId, versionId, cancellationToken))
            throw new DomainException("Customer have unpaid contract regarding same software");
    }

    private async Task<decimal> GetBestDiscount(Customer customer, CancellationToken cancellationToken)
    {
        decimal discount = 0;
        if (await _customerRepository.HadAnyPaidContract(customer.Id, cancellationToken))
        {
            discount = new decimal(0.05);
            
        }

        Discount? savedDiscount = await _discountRepository.GetBestSavedDiscount();
        if (savedDiscount == null)
        {
            return discount;
        }

        return savedDiscount.DiscountPercentage > discount ? savedDiscount.DiscountPercentage : discount;
    }
    private decimal CalculateFullPrice(decimal softwarePrice, int yearsOfSupport, decimal discount)
    {
        decimal fullPrice = softwarePrice + yearsOfSupport * 1000;
        if (!discount.Equals(0))
        {
            fullPrice *= discount;
            return fullPrice;
        }

        return fullPrice;
    }
    
}   