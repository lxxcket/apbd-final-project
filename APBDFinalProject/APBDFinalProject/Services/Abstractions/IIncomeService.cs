using APBDFinalProject.ResponseModels;

namespace APBDFinalProject.Services.Abstractions;

public interface IIncomeService
{
    Task<BusinessIncomeResponse> GetActualTotalBusinessIncome(CancellationToken cancellationToken, string targetCurrency);

    Task<BusinessIncomeResponse> GetPredicitedTotalBusinessIncome(CancellationToken cancellationToken,
        string targetCurrency);
}