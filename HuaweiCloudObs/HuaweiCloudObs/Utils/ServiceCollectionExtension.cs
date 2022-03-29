using HuaweiCloudObs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

namespace HuaweiCloudObs.Utils
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHuaweiCloudObs(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<HuaweicloudObsOptions>(configuration.GetSection(ObsConsts.Configuration));
            var options = serviceCollection.BuildServiceProvider().GetService<IOptions<HuaweicloudObsOptions>>().Value;
            serviceCollection.AddHttpClient(ObsConsts.ClientName)
                .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                .RetryAsync(options.MaxRetry));//重试次数

            serviceCollection.AddTransient<IObsBucketApi, ObsBucketApi>();
            serviceCollection.AddTransient<IObsObjectApi, ObsObjectApi>();
            return serviceCollection;
        }
    }
}
