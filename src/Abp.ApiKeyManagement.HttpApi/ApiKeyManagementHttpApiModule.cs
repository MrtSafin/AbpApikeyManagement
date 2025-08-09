using Localization.Resources.AbpUi;
using Abp.ApiKeyManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.ApiKeyManagement;

[DependsOn(
    typeof(ApiKeyManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class ApiKeyManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ApiKeyManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ApiKeyManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
