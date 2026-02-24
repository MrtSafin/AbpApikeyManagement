using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.ApiKeyManagement.ApiKeys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SimpleStateChecking;

namespace Abp.ApiKeyManagement.PermissionManagement;

[Authorize]
[Dependency(ReplaceServices = false)]
public class PermissionAppService : ApplicationService, IPermissionAppService
{
    protected virtual IPermissionAppService InnerService { get; }
    protected virtual IApiKeyStore ApiKeyStore { get; }
    protected virtual PermissionManagementOptions Options { get; }
    public PermissionAppService(IPermissionAppService innerService, IOptions<PermissionManagementOptions> options, IApiKeyStore apiKeyStore)
    {
        ApiKeyStore = apiKeyStore ?? throw new ArgumentNullException(nameof(apiKeyStore));
        InnerService = innerService;
        Options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<GetPermissionListResultDto> GetAsync(string providerName, string providerKey)
    {
        return await GetInternalAsync(null, providerName, providerKey);
    }

    public async Task<GetPermissionListResultDto> GetByGroupAsync(string groupName, string providerName, string providerKey)
    {
        return await GetInternalAsync(groupName, providerName, providerKey);
    }

    public async Task UpdateAsync(string providerName, string providerKey, UpdatePermissionsDto input)
    {
        await CheckProviderPolicy(providerName, providerKey);
        await InnerService.UpdateAsync(providerName, providerKey, input);
    }

    public Task<GetResourceProviderListResultDto> GetResourceProviderKeyLookupServicesAsync(string resourceName)
    {
        return InnerService.GetResourceProviderKeyLookupServicesAsync(resourceName);
    }
    public Task<SearchProviderKeyListResultDto> SearchResourceProviderKeyAsync(string resourceName, string serviceName, string filter, int page)
    {
        return InnerService.SearchResourceProviderKeyAsync(resourceName, serviceName, filter, page);
    }

    public Task<GetResourcePermissionDefinitionListResultDto> GetResourceDefinitionsAsync(string resourceName)
    {
        return InnerService.GetResourceDefinitionsAsync(resourceName);
    }

    public Task<GetResourcePermissionListResultDto> GetResourceAsync(string resourceName, string resourceKey)
    {
        return InnerService.GetResourceAsync(resourceName, resourceKey);
    }

    public async Task<GetResourcePermissionWithProviderListResultDto>
        GetResourceByProviderAsync(string resourceName, string resourceKey,
                                   string providerName, string providerKey)
    {
        return await GetInternalResourceByProviderAsync(resourceName, resourceKey, providerName, providerKey);
    }

    public async Task UpdateResourceAsync(string resourceName, string resourceKey, UpdateResourcePermissionsDto input)
    {
        await CheckProviderPolicy(input.ProviderName, input.ProviderKey);
        await InnerService.UpdateResourceAsync(resourceName, resourceKey, input);
    }

    public async Task DeleteResourceAsync(string resourceName, string resourceKey, string providerName, string providerKey)
    {
        await CheckProviderPolicy(providerName, providerKey);
        await InnerService.DeleteResourceAsync(resourceName, resourceKey, providerName, providerKey);
    }

    protected virtual async Task CheckProviderPolicy(string providerName, string providerKey)
    {
        var policyName = Options.ProviderPolicies.GetOrDefault(providerName);
        if (policyName.IsNullOrEmpty())
        {
            throw new AbpException($"No policy defined to get/set permissions for the provider '{providerName}'. Use {nameof(PermissionManagementOptions)} to map the policy.");
        }

        await AuthorizationService.CheckAsync(providerKey, policyName);
    }

    protected virtual async Task<GetPermissionListResultDto> GetInternalAsync(string? groupName, string providerName, string providerKey)
    {
        await CheckProviderPolicy(providerName, providerKey);
        
        var result = await InnerService.GetByGroupAsync(groupName, providerName, providerKey);
        if(providerName != ApiKeyPermissionValueProvider.ProviderName || !Guid.TryParse(providerKey, out var id))
        {
            return result;
        }

        var apiKey = await ApiKeyStore.FindByIdAsync(id);
        if (apiKey == null)
        {
            return result;
        }
        
        var userPermissions = await InnerService.GetByGroupAsync(groupName, UserPermissionValueProvider.ProviderName, apiKey.UserId.ToString());
        
        var deletedGroups = new List<PermissionGroupDto>();
        foreach (var permission in result.Groups)
        {
            var userGroup = userPermissions.Groups.Find(g => g.Name == permission.Name);
            if (userGroup == null)
            {
                deletedGroups.Add(permission);
                continue;
            }
            
            var deletedPermissions = new List<PermissionGrantInfoDto>();
            foreach (var p in permission.Permissions)
            {
                var userPermission = userGroup.Permissions.Find(up => up.Name == p.Name);
                if (userPermission is not { IsGranted: true })
                {
                    deletedPermissions.Add(p);
                }
            }
            
            permission.Permissions.RemoveAll(p => deletedPermissions.Contains(p));
            if (permission.Permissions.Count == 0)
            {
                deletedGroups.Add(permission);
            }
        }
        
        result.Groups.RemoveAll(g => deletedGroups.Contains(g));
        
        return result;
    }
    protected virtual async Task<GetResourcePermissionWithProviderListResultDto> GetInternalResourceByProviderAsync(string resourceName, string resourceKey, string providerName, string providerKey)
    {
        var result = await InnerService.GetResourceByProviderAsync(resourceName, resourceKey,
                                                                   providerName, providerKey);

        // Применяем ту же фильтрацию, как в GetInternalAsync
        if (providerName != ApiKeyPermissionValueProvider.ProviderName ||
            !Guid.TryParse(providerKey, out var id))
        {
            return result;
        }

        var apiKey = await ApiKeyStore.FindByIdAsync(id);
        if (apiKey == null)
        {
            return result;
        }

        // Фильтруем разрешения ресурса на основе прав пользователя
        var userPermissions = await InnerService.GetResourceByProviderAsync(
            resourceName, resourceKey,
            UserPermissionValueProvider.ProviderName,
            apiKey.UserId.ToString());

        // Удаляем разрешения, которые есть в API ключе, но нет у пользователя
        FilterResourcePermissions(result, userPermissions);

        return result;
    }

    protected virtual void FilterResourcePermissions(
        GetResourcePermissionWithProviderListResultDto apiKeyResult,
        GetResourcePermissionWithProviderListResultDto userResult)
    {
        // Логика аналогична GetInternalAsync - удаляем разрешения, 
        // которые есть в API ключе, но нет у пользователя

        if (apiKeyResult?.Permissions == null || userResult?.Permissions == null)
        {
            return;
        }

        var deletedPermissions = new List<ResourcePermissionWithProdiverGrantInfoDto>();

        foreach (var permission in apiKeyResult.Permissions)
        {
            // Ищем эквивалентное разрешение у пользователя
            var userPermission = userResult.Permissions.Find(up =>
                up.Name == permission.Name);

            // Если разрешение не выдано пользователю (или отсутствует) - удаляем для API ключа
            if (userPermission is not { IsGranted: true })
            {
                deletedPermissions.Add(permission);
            }
        }

        // Удаляем разрешения, которые не входят в права пользователя
        apiKeyResult.Permissions.RemoveAll(p => deletedPermissions.Contains(p));
    }
}