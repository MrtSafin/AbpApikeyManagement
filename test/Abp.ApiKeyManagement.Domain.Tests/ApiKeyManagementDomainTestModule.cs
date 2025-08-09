using Volo.Abp.Modularity;

namespace Abp.ApiKeyManagement;

[DependsOn(
    typeof(ApiKeyManagementDomainModule),
    typeof(ApiKeyManagementTestBaseModule)
)]
public class ApiKeyManagementDomainTestModule : AbpModule
{

}
