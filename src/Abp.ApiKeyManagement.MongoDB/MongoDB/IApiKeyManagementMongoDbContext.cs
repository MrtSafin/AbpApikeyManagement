using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Abp.ApiKeyManagement.MongoDB;

[ConnectionStringName(ApiKeyManagementDbProperties.ConnectionStringName)]
public interface IApiKeyManagementMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
