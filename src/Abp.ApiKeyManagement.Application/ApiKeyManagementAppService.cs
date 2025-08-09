using Abp.ApiKeyManagement.Localization;
using Volo.Abp.Application.Services;

namespace Abp.ApiKeyManagement;

public abstract class ApiKeyManagementAppService : ApplicationService
{
    protected ApiKeyManagementAppService()
    {
        LocalizationResource = typeof(ApiKeyManagementResource);
        ObjectMapperContext = typeof(ApiKeyManagementApplicationModule);
    }
}
