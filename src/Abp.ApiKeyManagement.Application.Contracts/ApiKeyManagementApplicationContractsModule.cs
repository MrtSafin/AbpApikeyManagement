using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Abp.ApiKeyManagement;

[DependsOn(
    typeof(ApiKeyManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ApiKeyManagementApplicationContractsModule : AbpModule
{

}
