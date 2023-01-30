using HuaweiCloudObs.Models;
using HuaweiCloudObs.Utils;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public class ObsObjectApi : BaseObsApi, IObsObjectApi
    {
        public ObsObjectApi(IOptionsSnapshot<HuaweicloudObsOptions> options, IHttpClientFactory factory, ILogger<ObsObjectApi> logger) : base(options, factory, logger)
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

        public Task<UploadObjectResult> PutAsync([NotNull] string name, [NotNull] byte[] data, SignatureDto signature = null, UploadObjectOptions headers = null, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Put, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            request.SetHeaders(headers);
            if(signature != null)
            {
                request.SetQueryParam(signature);
            }
            request.Content = new ByteArrayContent(data);
            request.Content.Headers.Add("Content-MD5", Signature.Md5(data));
            return SendAndReturnByHeaders<UploadObjectResult>(request, cancellationToken: cancellationToken);
        }

        public Task<UploadObjectResult> PutAsync([NotNull] string name, [NotNull]Stream stream, SignatureDto signature = null, UploadObjectOptions headers = null, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Put, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            if (signature != null)
            {
                request.SetQueryParam(signature);
            }
            request.SetHeaders(headers);
            request.Content = new StreamContent(stream);
            return SendAndReturnByHeaders<UploadObjectResult>(request, cancellationToken: cancellationToken);
        }

        public Task<UploadObjectResult> PostAsync([NotNull] byte[] data, [NotNull] PostObjectOptions options, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            options.AccessKeyId ??= Options.Value.AccessKey;
            options.Policy ??= new UploadPolicy
            {
                Expiration = DateTimeOffset.UtcNow.AddHours(2),
                Conditions = new System.Collections.ArrayList
                {
                    new { bucket = Bucket },
                    new { key = options.Key }
                }
            };
            HttpRequestMessage request = new(HttpMethod.Post, $"https://{Bucket}.{Options.Value.EndPoint}");
            var formData = FormDataExtension.GetFormData(InternalPostObjectOptions.ConvertFromDTO(options, Options.Value.SecretKey));
            formData.Add(new ByteArrayContent(data), "file", options.FileName);
            request.Content = formData;
            return SendAndReturnByHeaders<UploadObjectResult>(request, cancellationToken: cancellationToken);
        }


        public Task<Stream> GetAsync([NotNull] string name, GetObjectRequest input = null, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            if (input != null)
            {
                request.SetHeaders(input);
            }
            return SendAndReturnStreamAsync(request, cancellationToken: cancellationToken);
        }

        public Task<byte[]> GetBytesAsync([NotNull] string name, GetObjectRequest input = null, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            if (input != null)
            {
                request.SetHeaders(input);
            }
            return SendAndReturnBytesAsync(request, cancellationToken: cancellationToken);
        }

        public Task<HttpResponseMessage> GetObjectResponseAsync([NotNull] string name, GetObjectRequest input = null, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            if (input != null)
            {
                request.SetHeaders(input);
            }
            return SendAndReturnResponseAsync(request, cancellationToken: cancellationToken);
        }

        public Task DeleteAsync([NotNull] string name, string versionId = null, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            HttpRequestMessage request = new(HttpMethod.Delete, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            if (!string.IsNullOrWhiteSpace(versionId))
            {
                request.Headers.Add(nameof(versionId), versionId);
            }

            return SendNoReturnAsync(request, cancellationToken: cancellationToken);
        }

        public Task<DeleteObjectsResult> DeleteBatchAsync([NotNull] DeleteObjectsRequest input, CancellationToken cancellationToken = default)
        {
            CheckSetBucket();
            var s = ObsXmlSerializer.Serialize(input);
            HttpRequestMessage request = new(HttpMethod.Post, $"https://{Bucket}.{Options.Value.EndPoint}/?delete")
            {
                Content = new StringContent(s, Encoding.UTF8, "application/xml")
            };
            request.Headers.Add("Content-SHA256", Signature.Sha256(s));
            return SendAsync<DeleteObjectsResult>(request, cancellationToken: cancellationToken);
        }
    }
}
