using APBDFinalProject.RequestModels;

namespace APBDFinalProject.Services.Abstractions;

public interface ISecurityService
{
    Task RegisterUser(RegisterRequest registerRequest, CancellationToken cancellationToken);
    Task<(string, string)> LoginUser(LoginRequest loginRequest, CancellationToken cancellationToken);
    Task<(string, string)> RefreshUserToken(RefreshTokenRequest refreshToken, CancellationToken cancellationToken);
}