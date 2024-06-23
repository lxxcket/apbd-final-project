using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace APBDFinalProject.Controllers;


[ApiController]
[Route("api/{controller}")]
public class IndividualCustomerController : ControllerBase
{
    private IIndividualCustomerService _individualCustomerService;

    public IndividualCustomerController(IIndividualCustomerService individualCustomerService)
    {
        _individualCustomerService = individualCustomerService;
    }
    
    
}