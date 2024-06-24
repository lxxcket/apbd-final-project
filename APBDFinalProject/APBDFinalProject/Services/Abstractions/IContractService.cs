using APBDFinalProject.RequestModels;
using APBDFinalProject.ResponseModels;

namespace APBDFinalProject.Services.Abstractions;

public interface IContractService
{
    Task<ContractResponse> CreateContract(ContractRequest request, CancellationToken cancellationToken);
}