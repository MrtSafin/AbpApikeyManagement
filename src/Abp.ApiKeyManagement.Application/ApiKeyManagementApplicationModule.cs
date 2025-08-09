using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Abp.ApiKeyManagement;

[DependsOn(
    typeof(ApiKeyManagementDomainModule),
    typeof(ApiKeyManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class ApiKeyManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<ApiKeyManagementApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ApiKeyManagementApplicationModule>(validate: true);
        });
    }
}
