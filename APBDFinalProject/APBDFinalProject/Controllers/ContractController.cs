using APBDFinalProject.Exceptions;
using APBDFinalProject.RequestModels;
using APBDFinalProject.ResponseModels;
using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace APBDFinalProject.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ContractController : ControllerBase
{
    private IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract(ContractRequest contractRequest,
        CancellationToken cancellationToken)
    {
        ContractResponse contract
             = await _contractService.CreateContract(contractRequest, cancellationToken);

        return Ok(contract);
    }
    
}