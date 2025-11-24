using Abp.ApiKeyManagement.ApiKeys;
using Abp.ApiKeyManagement.Web.Pages.ApiKeyManagement;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Abp.ApiKeyManagement.Web;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[MapExtraProperties]
public partial class CreateApiKeyViewModelToCreateApiKeyDtoMapper : MapperBase<CreateModal.CreateApiKeyViewModel, CreateApiKeyDto>
{
    public override partial CreateApiKeyDto Map(CreateModal.CreateApiKeyViewModel source);

    public override partial void Map(CreateModal.CreateApiKeyViewModel source, CreateApiKeyDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[MapExtraProperties]
public partial class EditApiKeyViewModelToUpdateApiKeyDtoMapper : MapperBase<EditModal.EditApiKeyViewModel, UpdateApiKeyDto>
{
    public override partial UpdateApiKeyDto Map(EditModal.EditApiKeyViewModel source);

    public override partial void Map(EditModal.EditApiKeyViewModel source, UpdateApiKeyDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[MapExtraProperties]
public partial class ApiKeyDtoToEditApiKeyViewModelMapper : MapperBase<ApiKeyDto, EditModal.EditApiKeyViewModel>
{
    public override partial EditModal.EditApiKeyViewModel Map(ApiKeyDto source);

    public override partial void Map(ApiKeyDto source, EditModal.EditApiKeyViewModel destination);
}