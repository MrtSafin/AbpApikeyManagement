using Volo.Abp.Modularity;

namespace Abp.ApiKeyManagement;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class ApiKeyManagementApplicationTestBase<TStartupModule> : ApiKeyManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
