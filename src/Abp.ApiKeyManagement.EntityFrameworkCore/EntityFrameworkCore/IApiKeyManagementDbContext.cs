using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.ApiKeyManagement.EntityFrameworkCore;

[ConnectionStringName(ApiKeyManagementDbProperties.ConnectionStringName)]
public interface IApiKeyManagementDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
