using HuaweiCloudObs;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace HuaweiCloudObsUnitTests
{
    public class ObsBucketApiTests
    {
        private readonly IObsBucketApi bucketApi;
        private readonly IObsObjectApi objectApi;

        public ObsBucketApiTests(IObsBucketApi bucketApi, IObsObjectApi objectApi, IOptions<HuaweicloudObsOptions> options)
        {
            this.bucketApi = bucketApi;
            this.objectApi = objectApi;
            this.objectApi.Bucket = options.Value.DefaultBucket;
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
            await objectApi.PutAsync("appsettings.json", File.ReadAllBytes("appsettings.json"));
            await Task.Delay(1000);
            var buckets = await bucketApi.GetBucketsAsync();
            var objs = await bucketApi.GetObjectsAsync(buckets.Buckets[0].Name);
            objs.Contents.Count.ShouldBeGreaterThan(0);
        }
    }
}
