using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IAppUserRepository
{
    Task AddUser(AppUser user, CancellationToken cancellationToken);
    Task<AppUser?> GetUserByLogin(string login, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<AppUser> GetUserByRefreshToken(string refreshTokenRefreshToken, CancellationToken cancellationToken);
}