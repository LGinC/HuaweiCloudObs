using HuaweiCloudObs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
var services = new ServiceCollection();
services.AddHuaweiCloudObs(config);


var provider = services.BuildServiceProvider();

var bucketApi = provider.GetRequiredService<IObsBucketApi>();
var buckets = await bucketApi.GetBucketsAsync(default);
Console.WriteLine(buckets.Buckets[0].Name);
Console.WriteLine(buckets.Buckets[0].CreationDate.LocalDateTime);
var objectApi = provider.GetRequiredService<IObsObjectApi>();
objectApi.Bucket = buckets.Buckets[0].Name;
//await objectApi.PutAsync("appsettings.json", File.ReadAllBytes("appsettings.json"));
var bytes = await objectApi.GetBytesAsync("appsettings.json", new HuaweiCloudObs.Models.GetObjectRequest { Attname = "attname=over.json", IfModifiedSince = DateTimeOffset.Now.AddDays(-1)  });
Console.WriteLine(System.Text.Encoding.UTF8.GetString(bytes));