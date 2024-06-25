using APBDFinalProject.Exceptions;
using APBDFinalProject.ResponseModels;
using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace APBDFinalProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomeController : ControllerBase
{
    private IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetActualTotalIncomeForBusiness(CancellationToken cancellationToken, [FromQuery] string currency = "PLN", [FromQuery] bool predictedIncome = false)
    {
        BusinessIncomeResponse incomeResponse
         = predictedIncome
                ? await _incomeService.GetPredicitedTotalBusinessIncome(cancellationToken, currency)
                : await _incomeService.GetActualTotalBusinessIncome(cancellationToken, currency);
        return Ok(incomeResponse);
    }
}