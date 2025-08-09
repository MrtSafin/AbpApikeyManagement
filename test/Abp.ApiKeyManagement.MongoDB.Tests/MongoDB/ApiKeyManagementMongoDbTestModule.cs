using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Abp.ApiKeyManagement.MongoDB;

[DependsOn(
    typeof(ApiKeyManagementApplicationTestModule),
    typeof(ApiKeyManagementMongoDbModule)
)]
public class ApiKeyManagementMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
