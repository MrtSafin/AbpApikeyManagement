using Abp.ApiKeyManagement.MongoDB;
using Abp.ApiKeyManagement.Samples;
using Xunit;

namespace Abp.ApiKeyManagement.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<ApiKeyManagementMongoDbTestModule>
{

}
