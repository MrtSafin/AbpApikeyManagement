using Abp.ApiKeyManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.ApiKeyManagement.Permissions;

public class ApiKeyManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ApiKeyManagementPermissions.GroupName, L("Permission:ApiKeyManagement"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ApiKeyManagementResource>(name);
    }
}
