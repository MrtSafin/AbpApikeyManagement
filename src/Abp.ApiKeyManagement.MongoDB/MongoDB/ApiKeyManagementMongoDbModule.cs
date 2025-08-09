using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Abp.ApiKeyManagement.MongoDB;

[DependsOn(
    typeof(ApiKeyManagementDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class ApiKeyManagementMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<ApiKeyManagementMongoDbContext>(options =>
        {
            options.AddDefaultRepositories<IApiKeyManagementMongoDbContext>();
            
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
        });
    }
}
