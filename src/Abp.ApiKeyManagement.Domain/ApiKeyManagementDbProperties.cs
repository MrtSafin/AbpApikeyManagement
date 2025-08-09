namespace Abp.ApiKeyManagement;

public static class ApiKeyManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = "ApiKeyManagement";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "ApiKeyManagement";
}
