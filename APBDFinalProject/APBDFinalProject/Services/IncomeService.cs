using APBDFinalProject.Exceptions;
using APBDFinalProject.Helpers;
using APBDFinalProject.Models;
using APBDFinalProject.Repositories;
using APBDFinalProject.ResponseModels;
using APBDFinalProject.Services.Abstractions;

namespace APBDFinalProject.Services;

public class IncomeService : IIncomeService
{
    private IContractRepository _contractRepository;

    public IncomeService(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<BusinessIncomeResponse> GetActualTotalBusinessIncome(CancellationToken cancellationToken, string targetCurrency)
    {
        List<Contract> contracts = await _contractRepository.GetAllPaidContracts(cancellationToken);
        ContractsExist(contracts);

        decimal totalIncome = CountTotalIncome(contracts);

        return GetConvertedIncome(targetCurrency, totalIncome);
    }

    public async Task<BusinessIncomeResponse> GetPredicitedTotalBusinessIncome(CancellationToken cancellationToken, string targetCurrency)
    {
        List<Contract> contracts = await _contractRepository.GetAllContracts(cancellationToken);
        ContractsExist(contracts);
        decimal totalIncome = CountTotalIncome(contracts);

        return GetConvertedIncome(targetCurrency, totalIncome);
    }

    private void ContractsExist(List<Contract> contracts)
    {
        if (contracts.Count == 0)
        {
            throw new DomainException("No contracts was found for that company");
        }
    }

    private decimal CountTotalIncome(List<Contract> contracts)
    {
        decimal totalIncome = 0;
        foreach (var contract in contracts)
        {
            totalIncome += contract.TotalContractPrice;
        }

        return totalIncome;
    }

    private BusinessIncomeResponse GetConvertedIncome(string targetCurrency, decimal totalIncome)
    {
        var currencyConversion = CurrencyConversionHelper.ConvertCurrency(targetCurrency, totalIncome);
        return new BusinessIncomeResponse()
        {
            Income = currencyConversion.Result.conversionResult,
            Currency = currencyConversion.Result.targetCode
        };
    }
}