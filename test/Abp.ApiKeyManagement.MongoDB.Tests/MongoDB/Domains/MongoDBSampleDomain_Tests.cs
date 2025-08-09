using Abp.ApiKeyManagement.Samples;
using Xunit;

namespace Abp.ApiKeyManagement.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<ApiKeyManagementMongoDbTestModule>
{

}
