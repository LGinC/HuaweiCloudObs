using HuaweiCloudObs;
using HuaweiCloudObs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HuaweiCloudObsUnitTests
{
    public class ObsObectApiTest
    {
        private readonly IObsObjectApi objectApi;
        private readonly ILogger<ObsObectApiTest> logger;
        const string objectName = "appsettings.json";

        public ObsObectApiTest(IObsObjectApi objectApi, IOptions<HuaweicloudObsOptions> options, ILogger<ObsObectApiTest> logger)
        {
            this.objectApi = objectApi;
            this.objectApi.Bucket = options.Value.DefaultBucket;
            this.logger = logger;
        }

        [Fact]
        public async Task PutTest()
        {
            var result = await objectApi.PutAsync(objectName, File.ReadAllBytes(objectName));
            result.Date.ShouldBeLessThan(DateTimeOffset.UtcNow);
            result.ObsId2.ShouldNotBeNull();
        }

        [Fact]
        public async Task PostStreamTest()
        {
            var result = await objectApi.PostAsync(File.ReadAllBytes(objectName), new PostObjectOptions { Key = objectName, FileName = objectName });
            result.Date.ShouldBeLessThan(DateTimeOffset.UtcNow);
            result.ObsId2.ShouldNotBeNull();
        }

        [Fact]
        public async Task PutStreamTest()
        {
            var result = await objectApi.PutAsync(objectName, File.OpenRead(objectName));
            result.Date.ShouldBeLessThan(DateTimeOffset.UtcNow);
            result.ObsId2.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetBytesTest()
        {
            await objectApi.PutAsync(objectName, File.ReadAllBytes(objectName));
            var result = await objectApi.GetBytesAsync(objectName);
            result.Length.ShouldBeGreaterThan(1);
            logger.LogInformation(Encoding.UTF8.GetString(result));
        }

        [Fact]
        public async Task GetTest()
        {
            var result = await objectApi.GetAsync(objectName);
            result.Length.ShouldBeGreaterThan(1);
            logger.LogInformation(new StreamReader(result).ReadToEnd());
        }

        [Fact]
        public async Task GetObjectResponseTest()
        {
            await objectApi.PutAsync(objectName, File.ReadAllBytes(objectName));
            var response = await objectApi.GetObjectResponseAsync(objectName);
            var str = await response.Content.ReadAsStringAsync();
            str.Length.ShouldBeGreaterThan(1);
            logger.LogInformation(str);
        }

        [Fact]
        public async Task DeleteTest()
        {
            await objectApi.PutAsync(objectName, File.ReadAllBytes(objectName));
            await objectApi.DeleteAsync(objectName);
            try
            {
                var bytes = await objectApi.GetBytesAsync(objectName);
            }
            catch (Exception e)
            {
                e.Message.ShouldBe("NoSuchKey-The specified key does not exist.");
            }
        }

        [Fact]
        public async Task DeleteBatchTest()
        {
            var bytes = File.ReadAllBytes(objectName);
            await objectApi.PutAsync(objectName, bytes);
            await objectApi.PutAsync(objectName + "1", bytes);

            var result = await objectApi.DeleteBatchAsync(new HuaweiCloudObs.Models.DeleteObjectsRequest
            {
                Objects = new System.Collections.Generic.List<HuaweiCloudObs.Models.DeleteObject>
                 {
                     new HuaweiCloudObs.Models.DeleteObject{ Key = objectName},
                     new HuaweiCloudObs.Models.DeleteObject{ Key = objectName + "1"},
                 }
            });

            result.Deletes.Count.ShouldBe(2);
        }

    }
}
