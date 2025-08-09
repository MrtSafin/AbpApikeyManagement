using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Abp.ApiKeyManagement.Samples;

[Area(ApiKeyManagementRemoteServiceConsts.ModuleName)]
[RemoteService(Name = ApiKeyManagementRemoteServiceConsts.RemoteServiceName)]
[Route("api/api-key-management/example")]
public class ExampleController : ApiKeyManagementController, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public ExampleController(ISampleAppService sampleAppService)
    {
        _sampleAppService = sampleAppService;
    }

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }
}
