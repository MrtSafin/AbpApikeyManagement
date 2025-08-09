using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Abp.ApiKeyManagement.MongoDB;

[ConnectionStringName(ApiKeyManagementDbProperties.ConnectionStringName)]
public class ApiKeyManagementMongoDbContext : AbpMongoDbContext, IApiKeyManagementMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureApiKeyManagement();
    }
}
