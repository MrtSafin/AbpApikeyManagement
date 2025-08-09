using Volo.Abp.Modularity;

namespace Abp.ApiKeyManagement;

[DependsOn(
    typeof(ApiKeyManagementApplicationModule),
    typeof(ApiKeyManagementDomainTestModule)
    )]
public class ApiKeyManagementApplicationTestModule : AbpModule
{

}
