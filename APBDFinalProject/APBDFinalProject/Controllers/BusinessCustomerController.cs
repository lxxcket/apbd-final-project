using APBDFinalProject.Exceptions;
using APBDFinalProject.RequestModels;
using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;


namespace APBDFinalProject.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BusinessCustomerController : ControllerBase
{
    private IBusinessCustomerService _businessCustomerService;

    public BusinessCustomerController(IBusinessCustomerService businessCustomerService)
    {
        _businessCustomerService = businessCustomerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBusinessCustomer(BusinessCustomerRequest businessCustomerRequest, CancellationToken cancellationToken)
    {
        int id;
        try
        {
            id = await _businessCustomerService.AddCustomer(businessCustomerRequest, cancellationToken);
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }

        return Ok(id);
    }

    [HttpPut("{krs:long}")]
    public async Task<IActionResult> UpdateBusinessCustomer(int krs, BusinessCustomerUpdateRequest businessCustomerUpdateRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            await _businessCustomerService.UpdateCustomer(krs, businessCustomerUpdateRequest, cancellationToken);
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }

        return NoContent();
    }
    
}