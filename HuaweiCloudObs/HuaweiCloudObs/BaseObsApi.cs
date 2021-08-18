using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public abstract class BaseObsApi
    {
        protected HttpClient Client { get; set; }
        protected IOptionsSnapshot<HuaweicloudObsOptions> Options { get; set; }

        protected BaseObsApi(IOptionsSnapshot<HuaweicloudObsOptions> options, IHttpClientFactory factory)
        {
            Options = options;
            Client = factory.CreateClient(ObsConsts.ClientName);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="query">单个资源请求  和queries 二选一</param>
        /// <param name="queries">多个资源请求</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> SendAsync(HttpRequestMessage request, string query = null, SortedDictionary<string, string> queries = null,  CancellationToken cancellationToken = default)
        {
            request.Headers.Add("Date", DateTimeOffset.UtcNow.ToString("r"));
            var headers = request.Headers.ToDictionary(h => h.Key.ToLower(), h => h.Value);
            //合并content中的请求头
            if (request.Content != null && request.Content.Headers.Any())
            {
                foreach (var h in request.Content.Headers)
                {
                    headers.Add(h.Key.ToLower(), h.Value);
                }
            }
            var options = Options.Value;
            var sign = !string.IsNullOrEmpty(query)
                ? Signature.GetSign(options.AccessKey, options.SecretKey, request.Method.ToString(), request.Headers.ToDictionary(h => h.Key.ToLower(), h => h.Value), query)
                : Signature.GetSign(options.AccessKey, options.SecretKey, request.Method.ToString(), request.Headers.ToDictionary(h => h.Key.ToLower(), h => h.Value), queries);
            request.Headers.Add("Authorization", sign);
            var result = await Client.SendAsync(request, cancellationToken);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
