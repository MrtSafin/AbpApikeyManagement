using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Abp.ApiKeyManagement.MongoDB;

public static class ApiKeyManagementMongoDbContextExtensions
{
    public static void ConfigureApiKeyManagement(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
