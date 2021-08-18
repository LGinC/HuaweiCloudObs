using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace HuaweiCloudObs
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHuaweiCloudObs(this IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            serviceCollection.Configure<HuaweicloudObsOptions>(configuration.GetSection(ObsConsts.Configuration));
            serviceCollection.AddHttpClient(ObsConsts.ClientName)
                .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                .RetryAsync(3));//重试3次

            serviceCollection.AddTransient<IObsBucketApi, ObsBucketApi>();
            serviceCollection.AddTransient<IObsObjectApi, ObsObjectApi>();
            return serviceCollection;
        }
    }
}
