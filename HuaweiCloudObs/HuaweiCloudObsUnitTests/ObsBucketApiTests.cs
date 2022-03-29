using HuaweiCloudObs;
using HuaweiCloudObs.Models;
using HuaweiCloudObs.Models.Buckets;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.IO;
using System.Linq;
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
            bucket.CreationDate.ShouldNotBe(default);
        }

        [Fact]
        public async Task GetMetadataTest()
        {
            var buckets = await bucketApi.GetBucketsAsync();
            var meta = await bucketApi.GetMetadataAsync(buckets.Buckets[0].Name);
            meta.Location.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetLocationTest()
        {
            var buckets = await bucketApi.GetBucketsAsync();
            var r = await bucketApi.GetLocationAsync(buckets.Buckets[0].Name);
            r.ShouldBe(buckets.Buckets[0].Location);
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

        [Fact]
        public async Task GetObjectsV2Test()
        {
            await objectApi.PutAsync("appsettings.json", File.ReadAllBytes("appsettings.json"));
            await Task.Delay(1000);
            var buckets = await bucketApi.GetBucketsAsync();
            var objs = await bucketApi.GetObjectsV2Async(buckets.Buckets[0].Name);
            objs.Contents.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateAndDeleteTest()
        {
            var name = "test-create-and-delete";
            await bucketApi.CreateAsync(new () { BucketName = name, Location = "cn-east-3" }, new () { Acl = HuaweiCloudObsDefine.AcessControlLists.PublicReadDelivered, StorageClass = HuaweiCloudObs.Models.StorageClass.COLD});
            var buckets = await bucketApi.GetBucketsAsync();
            buckets.Buckets.Any(b=> b.Name == name).ShouldBeTrue();
            await bucketApi.DeleteAsync(name);
            buckets = await bucketApi.GetBucketsAsync();
            buckets.Buckets.Any(b => b.Name == name).ShouldBeFalse();
        }
    }
}
