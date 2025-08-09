using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.ApiKeyManagement.EntityFrameworkCore;

[ConnectionStringName(ApiKeyManagementDbProperties.ConnectionStringName)]
public class ApiKeyManagementDbContext : AbpDbContext<ApiKeyManagementDbContext>, IApiKeyManagementDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public ApiKeyManagementDbContext(DbContextOptions<ApiKeyManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureApiKeyManagement();
    }
}
