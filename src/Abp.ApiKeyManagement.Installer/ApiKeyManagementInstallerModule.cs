using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Abp.ApiKeyManagement;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class ApiKeyManagementInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ApiKeyManagementInstallerModule>();
        });
    }
}
