using Volo.Abp.Modularity;

namespace Abp.ApiKeyManagement;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class ApiKeyManagementDomainTestBase<TStartupModule> : ApiKeyManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
