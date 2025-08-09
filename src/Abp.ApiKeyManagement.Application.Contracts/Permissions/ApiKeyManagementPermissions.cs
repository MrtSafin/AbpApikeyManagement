using Volo.Abp.Reflection;

namespace Abp.ApiKeyManagement.Permissions;

public class ApiKeyManagementPermissions
{
    public const string GroupName = "ApiKeyManagement";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ApiKeyManagementPermissions));
    }
}
