using Version = APBDFinalProject.Models.Version;

namespace APBDFinalProject.Repositories;

public interface IVersionRepository
{
    Task<Version?> GetVersionWithSoftware(int versionId, CancellationToken cancellationToken);
}