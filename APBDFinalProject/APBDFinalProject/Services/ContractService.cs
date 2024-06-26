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
        ValidateDate(request.StartDate,request.EndDate, request.DaysSpan);
        Customer customer = await CustomerExistsAndRetrieve(request.IdCustomer, cancellationToken);
        Version version = await VersionExistsAndRetrieve(request.IdVersion, cancellationToken);
        await CustomerHaveUnpaidContractWithSameSoftware(customer.Id, version.Id, cancellationToken);
        await CustomerIsIndividualAndDeleted(customer.Id, cancellationToken);
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
            AmountPaid = 0
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
           AmountPaid = contract.AmountPaid
       };
    }

    private async Task CustomerIsIndividualAndDeleted(int customerId, CancellationToken cancellationToken)
    {
        var res = await _customerRepository.IsIndividualCustomerAndDeleted(customerId, cancellationToken);
        if (res == (true, true))
            throw new DomainException(
                "Individual customer was deleted, you can't create contract for deleted customer");
    }

    private void ContractStartDateInPast(DateTime startDate)
    {
        
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

    private async Task CustomerHaveUnpaidContractWithSameSoftware(int customerId, int versionId, CancellationToken cancellationToken)
    {
        if(await _customerRepository.HasContractWithSoftware(customerId, versionId, cancellationToken))
            throw new DomainException("Customer have unpaid contract regarding same software");
    }

    private async Task<decimal> GetBestDiscount(Customer customer, CancellationToken cancellationToken)
    {
        decimal discount = 0;
        if (await _customerRepository.HadAnyPaidContract(customer.Id, cancellationToken))
        {
            discount = new decimal(5.00);
            
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
            decimal percentageDecimal = discount / 100.0m;

            // Calculate discount amount
            decimal discountAmount = fullPrice * percentageDecimal;
            
            return fullPrice - discountAmount;
        }

        return fullPrice;
    }
    public void ValidateDate(DateTime startDate, DateTime endDate, int daySpan)
    {
        if (startDate < DateTime.Now)
            throw new DomainException("Start date of the contract can't be in the past");
        if (endDate < startDate)
            throw new DomainException("End date cannot be earlier than start date.");
        if (endDate < startDate.AddDays(daySpan))
            throw new DomainException("End date cannot be earlier than start date plus the day span.");
    }
}   