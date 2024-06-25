using APBDFinalProject.Contexts;
using APBDFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDFinalProject.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private IncomeContext _context;

    public AppUserRepository(IncomeContext context)
    {
        _context = context;
    }

    public async Task AddUser(AppUser user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUser?> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(u => u.Login == login).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<AppUser> GetUserByRefreshToken(string refreshTokenRefreshToken, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenRefreshToken, cancellationToken);
    }
}