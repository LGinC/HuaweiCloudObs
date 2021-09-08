using HuaweiCloudObs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Xunit.DependencyInjection.Logging;

namespace HuaweiCloudObsUnitTests
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    // 注册配置
                    builder.AddJsonFile("appsettings.json");
                });
        }

        public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            // DemystifyException
            services.UseDemystifyExceptionFilter();

            var configuration = hostBuilderContext.Configuration;
            services.AddHuaweiCloudObs(configuration);

            services.AddTransient<ObsBucketApiTests>();
        }

        public void Configure(IServiceProvider provider)
        {
            // add log output to ITestOutputHelper
            XunitTestOutputLoggerProvider.Register(provider);
        }
    }
}
