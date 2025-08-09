using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Abp.ApiKeyManagement.Localization;
using Abp.ApiKeyManagement.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Abp.ApiKeyManagement.Permissions;

namespace Abp.ApiKeyManagement.Web;

[DependsOn(
    typeof(ApiKeyManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class ApiKeyManagementWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(ApiKeyManagementResource), typeof(ApiKeyManagementWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ApiKeyManagementWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ApiKeyManagementMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ApiKeyManagementWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<ApiKeyManagementWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ApiKeyManagementWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
                //Configure authorization.
            });
    }
}
