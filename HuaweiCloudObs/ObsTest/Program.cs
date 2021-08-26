using HuaweiCloudObs;
using HuaweiCloudObs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
var services = new ServiceCollection();
services.AddHuaweiCloudObs(config);


var provider = services.BuildServiceProvider();

var bucketApi = provider.GetRequiredService<IObsBucketApi>();
var buckets = await bucketApi.GetBucketsAsync(default);
//Console.WriteLine(buckets.Buckets[0].Name);
var objectApi = provider.GetRequiredService<IObsObjectApi>();
objectApi.Bucket = buckets.Buckets[0].Name;
var fileName = "appsettings.json";
var uploadResult = await objectApi.PutAsync(fileName, File.ReadAllBytes(fileName));
var bytes = await objectApi.GetBytesAsync(fileName, new HuaweiCloudObs.Models.GetObjectRequest { Attname = "attname=over.json", IfModifiedSince = DateTimeOffset.Now.AddDays(-1) });
Console.WriteLine($"json: {System.Text.Encoding.UTF8.GetString(bytes)}");

var r = await objectApi.DeleteBatchAsync(new DeleteObjectsRequest
{
    Objects = new List<DeleteObject> { new DeleteObject { Key = fileName } }
});

Console.WriteLine($"delete: {r.Deletes[0].Key}");
