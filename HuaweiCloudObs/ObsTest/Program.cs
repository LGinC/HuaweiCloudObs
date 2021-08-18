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
Console.WriteLine(buckets?.Owner?.ID);