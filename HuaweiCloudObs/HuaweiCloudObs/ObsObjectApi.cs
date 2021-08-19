using HuaweiCloudObs.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public class ObsObjectApi : BaseObsApi, IObsObjectApi
    {
        public ObsObjectApi(IOptionsSnapshot<HuaweicloudObsOptions> options, IHttpClientFactory factory) : base(options, factory)
        {
        }

        public string Bucket { get; set; }

        private void CheckSetBucket()
        {
            if (string.IsNullOrWhiteSpace(Bucket))
            {
                throw new ArgumentNullException(nameof(Bucket), "请先设置Bucket");
            }
        }

        public Task PutAsync([NotNull]string name, [NotNull] byte[] data, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Put, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            request.Content = new ByteArrayContent(data);
            request.Content.Headers.Add("Content-MD5", Signature.Md5(data));
            return SendAsync<BaseResult>(request, $"/{Bucket}/{name}", cancellationToken: cancellationToken);
        }


        public Task<Stream> GetAsync([NotNull] string name, GetObjectRequest input = null, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            if(input == null)
            {
                return SendAndReturnStreamAsync(request, $"/{Bucket}/{name}", cancellationToken: cancellationToken);
            }

            return null;
        }

        public Task<byte[]> GetBytesAsync([NotNull] string name, GetObjectRequest input = null, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            if (input != null)
            {
                SetHeaders(request, input);
            }

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("Headers");
            //foreach (var h in request.Headers)
            //{
            //    Console.WriteLine($"{h.Key}:{h.Value.FirstOrDefault()}");
            //}
            return SendAndReturnBytesAsync(request, $"/{Bucket}/{name}", cancellationToken: cancellationToken);
        }
    }
}
