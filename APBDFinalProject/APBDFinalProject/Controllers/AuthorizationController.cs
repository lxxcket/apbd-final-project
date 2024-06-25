using APBDFinalProject.RequestModels;
using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBDFinalProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private ISecurityService _securityService;

    public AuthorizationController(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model, CancellationToken cancellationToken)
    {
        await _securityService.RegisterUser(model, cancellationToken);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var info = await _securityService.LoginUser(loginRequest, cancellationToken);
        return Ok(new
        {
            accessToken = info.Item1,
            refreshToken = info.Item2
        });
    }
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken, CancellationToken cancellationToken)
    {
        var result = await _securityService.RefreshUserToken(refreshToken, cancellationToken);
        return Ok(new
        {
            accessToken = result.Item1,
            refreshToken = result.Item2
        });
    }
    
    
}