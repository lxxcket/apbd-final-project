using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APBDFinalProject.Exceptions;
using APBDFinalProject.Helpers;
using APBDFinalProject.Models;
using APBDFinalProject.Repositories;
using APBDFinalProject.RequestModels;
using APBDFinalProject.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace APBDFinalProject.Services;

public class SecurityService : ISecurityService
{
    private IAppUserRepository _appUserRepository;
    private IConfiguration _configuration;

    public SecurityService(IAppUserRepository appUserRepository, IConfiguration configuration)
    {
        _appUserRepository = appUserRepository;
        _configuration = configuration;
    }

    public async Task RegisterUser(RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var hashedPasswordAndSalt = SecurityHelper.GetHashedPasswordAndSalt(registerRequest.Password);
        
        var user = new AppUser()
        {
            Email = registerRequest.Email,
            Login = registerRequest.Login,
            Role = registerRequest.Role,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelper.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        await _appUserRepository.AddUser(user, cancellationToken);
    }

    public async Task<(string, string)> LoginUser(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        AppUser user = await _appUserRepository.GetUserByLogin(loginRequest.Login, cancellationToken);
        if (user == null)
            throw new DomainException("User with given login does not exist");
        
        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelper.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
            throw new DomainException("Password is incorrect");


        var userClaims = GetUserClaims(user);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5222",
            audience: "https://localhost:5222",
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _appUserRepository.SaveChangesAsync(cancellationToken);
        return new(new JwtSecurityTokenHandler().WriteToken(token),
            user.RefreshToken);
    }

    public async Task<(string, string)> RefreshUserToken(RefreshTokenRequest refreshToken, CancellationToken cancellationToken)
    {
        var user = await _appUserRepository.GetUserByRefreshToken(refreshToken.RefreshToken, cancellationToken);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }

        var userClaims = GetUserClaims(user);


        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5222",
            audience: "https://localhost:5222",
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _appUserRepository.SaveChangesAsync(cancellationToken);
        return (new JwtSecurityTokenHandler().WriteToken(jwtToken), user.RefreshToken);
    }

    private List<Claim> GetUserClaims(AppUser user)
    {
        return new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Login),
            user.Role == "admin" ? new Claim(ClaimTypes.Role, "admin") : 
                new Claim(ClaimTypes.Role, "user")
        };
    }
}