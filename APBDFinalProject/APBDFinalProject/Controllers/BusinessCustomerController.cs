using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace APBDFinalProject.Controllers;


[ApiController]
[Microsoft.AspNetCore.Components.Route("api/{controller}")]
public class BusinessCustomerController
{
    private IBusinessCustomerService _businessCustomerService;

    public BusinessCustomerController(IBusinessCustomerService businessCustomerService)
    {
        _businessCustomerService = businessCustomerService;
    }
    
    
    
    
}