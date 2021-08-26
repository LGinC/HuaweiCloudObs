using HuaweiCloudObs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public abstract class BaseObsApi
    {
        protected HttpClient Client { get; set; }
        protected IOptionsSnapshot<HuaweicloudObsOptions> Options { get; set; }
        protected ILogger Logger {  get; set; }

        protected BaseObsApi(IOptionsSnapshot<HuaweicloudObsOptions> options, IHttpClientFactory factory, ILogger<BaseObsApi> logger)
        {
            Options = options;
            Client = factory.CreateClient(ObsConsts.ClientName);
            Logger = logger;
        }

        protected async Task<object> SendInternalAsync<T>(HttpRequestMessage request, string query = null, SortedDictionary<string, string> queries = null, ResultType resultType = ResultType.Object, CancellationToken cancellationToken = default)
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
                ? Signature.GetSign(options.AccessKey, options.SecretKey, request.Method.ToString(), headers, query)
                : Signature.GetSign(options.AccessKey, options.SecretKey, request.Method.ToString(), headers, queries);
            request.Headers.Add("Authorization", sign);
            var result = await Client.SendAsync(request, cancellationToken);
            //Console.WriteLine(await result.Content.ReadAsStringAsync());
            if (result.IsSuccessStatusCode)
            {
                switch (resultType)
                {
                    case ResultType.Object:
                        using (var stream = await result.Content.ReadAsStreamAsync())
                        {
                            return ObsXmlSerializer.Deserialize<T>(stream);
                        }
                    case ResultType.Task:
                        return default;
                    case ResultType.Stream:
                        return await result.Content.ReadAsStreamAsync();
                    case ResultType.Bytes:
                        return await result.Content.ReadAsByteArrayAsync();
                    case ResultType.HttpResponse:
                        return result;
                    default:
                        break;
                }
            }

            //返回为空
            using var eStream = await result.Content.ReadAsStreamAsync();
            if (eStream.Length <= 0)
            {
                return null;
            }
            var e =  ObsXmlSerializer.Deserialize<ErrorResult>(eStream);
            Logger.LogError("obs请求失败", request);
            Logger.LogError(System.Text.Json.JsonSerializer.Serialize(e));
            throw new InvalidOperationException($"{e.Code}-{e.Message}");
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="query">单个资源请求  和queries 二选一</param>
        /// <param name="queries">多个资源请求</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendAsync<T>(HttpRequestMessage request, string query = null, SortedDictionary<string, string> queries = null, CancellationToken cancellationToken = default)
        {
            return (T)await SendInternalAsync<T>(request, query, queries, ResultType.Object, cancellationToken);
        }

        /// <summary>
        /// 发送请求无返回值
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="query">单个资源请求  和queries 二选一</param>
        /// <param name="queries">多个资源请求</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SendNoReturnAsync(HttpRequestMessage request, string query = null, SortedDictionary<string, string> queries = null, CancellationToken cancellationToken = default)
        {
            return SendInternalAsync<Task>(request, query, queries, ResultType.Task, cancellationToken);
        }

        /// <summary>
        /// 发送请求,返回Stream
        /// </summary>
        /// <param name="request"></param>
        /// <param name="query"></param>
        /// <param name="queries"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Stream> SendAndReturnStreamAsync(HttpRequestMessage request, string query = null, SortedDictionary<string, string> queries = null, CancellationToken cancellationToken = default)
        {
            return (Stream)await SendInternalAsync<Stream>(request, query, queries, ResultType.Stream, cancellationToken);
        }

        /// <summary>
        /// 发送请求,返回Bytes
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="query">单个资源请求  和queries 二选一</param>
        /// <param name="queries">多个资源请求</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<byte[]> SendAndReturnBytesAsync(HttpRequestMessage request, string query = null, SortedDictionary<string, string> queries = null, CancellationToken cancellationToken = default)
        {
            return (byte[])await SendInternalAsync<byte[]>(request, query, queries, ResultType.Bytes, cancellationToken);
        }

        public async Task<HttpResponseMessage> SendAndReturnResponseAsync(HttpRequestMessage request, string query = null, SortedDictionary<string, string> queries = null, CancellationToken cancellationToken = default)
        {
            return (HttpResponseMessage)await SendInternalAsync<HttpResponseMessage>(request, query, queries, ResultType.HttpResponse, cancellationToken);
        }

        /// <summary>
        /// 发送请求 通过请求头返回结果
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="request">请求</param>
        /// <param name="query">单个资源请求  和queries 二选一</param>
        /// <param name="queries">多个资源请求</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendAndReturnByHeaders<T>(HttpRequestMessage request, string query = null, SortedDictionary<string, string> queries = null, CancellationToken cancellationToken = default)
        {
            var response = (HttpResponseMessage)await SendInternalAsync<HttpResponseMessage>(request, query, queries, ResultType.HttpResponse, cancellationToken);
            return response.Headers.GetByHeader<T>();
        }
    }
}
