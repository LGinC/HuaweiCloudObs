using HuaweiCloudObs;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HuaweiCloudObsUnitTests
{
    public class ObsBucketApiTests
    {
        private readonly IObsBucketApi bucketApi;

        public ObsBucketApiTests(IObsBucketApi bucketApi)
        {
            this.bucketApi = bucketApi;
        }


        [Fact]
        public async Task ListBucketTest()
        {
            var buckets = await bucketApi.GetBucketsAsync();
            buckets.Buckets.Count.ShouldBeGreaterThan(0);
            var bucket = buckets.Buckets[0];
            bucket.CreationDate.ShouldBeLessThan(DateTimeOffset.UtcNow);
        }

        [Fact]
        public async Task GetObjectsTest()
        {
            var buckets = await bucketApi.GetBucketsAsync();
            var objs = await bucketApi.GetObjectsAsync(buckets.Buckets[0].Name);
            objs.Contents.Count.ShouldBeGreaterThan(0);
        }
    }
}
