using System.Linq;
using Abp.ApiKeyManagement.ApiKeys;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Abp.ApiKeyManagement;

public class ApiKeyPrefixMapper
{
    public static string Map(string prefix) => new string(prefix.Take(5).ToArray()) + "...";
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target, AutoUserMappings = false)]
[MapExtraProperties]
public partial class ApiKeyToApiKeyDtoMapper : MapperBase<ApiKey, ApiKeyDto>
{
    [MapProperty(nameof(ApiKey.Prefix), nameof(ApiKeyDto.Prefix), Use = nameof(MapPrefix))]
    public override partial ApiKeyDto Map(ApiKey source);

    [MapProperty(nameof(ApiKey.Prefix), nameof(ApiKeyDto.Prefix), Use = nameof(MapPrefix))]
    public override partial void Map(ApiKey source, ApiKeyDto destination);
    
    protected virtual string MapPrefix(string prefix) => ApiKeyPrefixMapper.Map(prefix);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[MapExtraProperties]
public partial class ApiKeyToApiKeyInfoMapper : MapperBase<ApiKey, ApiKeyInfo>
{
    public override partial ApiKeyInfo Map(ApiKey source);
    public override partial void Map(ApiKey source, ApiKeyInfo destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target, AutoUserMappings = false)]
[MapExtraProperties]
public partial class ApiKeyToApiKeyCreateResultDtoMapper : MapperBase<ApiKey, ApiKeyCreateResultDto>
{
    [MapProperty(nameof(ApiKey.Prefix), nameof(ApiKeyCreateResultDto.Prefix), Use = nameof(MapPrefix))]
    [MapperIgnoreTarget(nameof(ApiKeyCreateResultDto.Key))]
    public override partial ApiKeyCreateResultDto Map(ApiKey source);

    [MapProperty(nameof(ApiKey.Prefix), nameof(ApiKeyCreateResultDto.Prefix), Use = nameof(MapPrefix))]
    [MapperIgnoreTarget(nameof(ApiKeyCreateResultDto.Key))]
    public override partial void Map(ApiKey source, ApiKeyCreateResultDto destination);
    
    protected virtual string MapPrefix(string prefix) => ApiKeyPrefixMapper.Map(prefix);
}