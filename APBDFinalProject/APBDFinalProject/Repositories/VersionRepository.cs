using APBDFinalProject.Contexts;
using Microsoft.EntityFrameworkCore;
using Version = APBDFinalProject.Models.Version;

namespace APBDFinalProject.Repositories;

public class VersionRepository : IVersionRepository
{
    private IncomeContext _context;

    public VersionRepository(IncomeContext context)
    {
        _context = context;
    }
    
    public async Task<Version?> GetVersionWithSoftware(int versionId, CancellationToken cancellationToken)
    {
        return await _context.Versions
            .Include(c => c.Software)
            .FirstOrDefaultAsync(v => v.Id == versionId, cancellationToken);
    }
}