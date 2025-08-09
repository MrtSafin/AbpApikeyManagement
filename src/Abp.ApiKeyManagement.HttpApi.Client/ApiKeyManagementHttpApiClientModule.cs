using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Abp.ApiKeyManagement;

[DependsOn(
    typeof(ApiKeyManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class ApiKeyManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(ApiKeyManagementApplicationContractsModule).Assembly,
            ApiKeyManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ApiKeyManagementHttpApiClientModule>();
        });

    }
}
