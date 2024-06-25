using APBDFinalProject.Exceptions;
using APBDFinalProject.RequestModels;
using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace APBDFinalProject.Controllers;


[ApiController]
[Route("api/[controller]")]
public class IndividualCustomerController : ControllerBase
{
    private IIndividualCustomerService _individualCustomerService;

    public IndividualCustomerController(IIndividualCustomerService individualCustomerService)
    {
        _individualCustomerService = individualCustomerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIndividualCustomer(IndividualCustomerRequest individualCustomerRequest,
        CancellationToken cancellationToken)
    {
        int id;
        try
        {
            id = await _individualCustomerService.AddCustomer(individualCustomerRequest, cancellationToken);
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }

        return Ok(id);
    }
    
    [HttpPut("{pesel:long}")]
    public async Task<IActionResult> UpdateIndividualCustomer(long pesel,  IndividualCustomerUpdateRequest individualCustomerRequest,
        CancellationToken cancellationToken)
    {
        int id;
        try
        {
            id = await _individualCustomerService.UpdateCustomer(pesel, individualCustomerRequest, cancellationToken);
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }

        return NoContent();
    }
    [HttpDelete("{pesel:long}")]
    public async Task<IActionResult> DeleteIndividualCustomer(long pesel,
        CancellationToken cancellationToken)
    {
        int id;
        try
        {
            await _individualCustomerService.DeleteCustomer(pesel, cancellationToken);
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }

        return NoContent();
    }
}