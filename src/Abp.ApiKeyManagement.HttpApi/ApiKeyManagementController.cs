using Abp.ApiKeyManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.ApiKeyManagement;

public abstract class ApiKeyManagementController : AbpControllerBase
{
    protected ApiKeyManagementController()
    {
        LocalizationResource = typeof(ApiKeyManagementResource);
    }
}
