using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Abp.ApiKeyManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ApiKeyManagementDomainSharedModule)
)]
public class ApiKeyManagementDomainModule : AbpModule
{

}
