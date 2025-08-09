using Abp.ApiKeyManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.ApiKeyManagement.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ApiKeyManagementPageModel : AbpPageModel
{
    protected ApiKeyManagementPageModel()
    {
        LocalizationResourceType = typeof(ApiKeyManagementResource);
        ObjectMapperContext = typeof(ApiKeyManagementWebModule);
    }
}
