using HuaweiCloudObs.Models;
using JetBrains.Annotations;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    /// <summary>
    /// 对象操作api
    /// </summary>
    public interface IObsObjectApi
    {
        /// <summary>
        /// 所属桶
        /// </summary>
        string Bucket { set; get; }

        /// <summary>
        /// 上传对象
        /// <para>在桶未开启多版本的情况下，如果在指定的桶内已经有相同的对象键值的对象，用户上传的新对象会覆盖原来的对象</para>
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="data">对象数据</param>
        /// <returns></returns>
        Task<UploadObjectResult> PutAsync([NotNull] string name, [NotNull] byte[] data, SignatureDto signature = null, UploadObjectOptions options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传对象 
        /// <para>在桶未开启多版本的情况下，如果在指定的桶内已经有相同的对象键值的对象，用户上传的新对象会覆盖原来的对象 不会传入md5</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        /// <param name="headers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<UploadObjectResult> PutAsync([NotNull] string name, [NotNull] Stream stream, SignatureDto signature = null, UploadObjectOptions headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传对象
        /// <para>在桶未开启多版本的情况下，如果在指定的桶内已经有相同的对象键值的对象，用户上传的新对象会覆盖原来的对象 不会传入md5</para>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UploadObjectResult> PostAsync([NotNull] byte[] data, [NotNull] PostObjectOptions options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取对象流
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="input">附加参数</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Stream> GetAsync([NotNull] string name, GetObjectRequest input = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取对象内容数组
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="input">附加参数</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<byte[]> GetBytesAsync([NotNull] string name, GetObjectRequest input = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取对象 http响应
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="input">附加参数</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetObjectResponseAsync([NotNull] string name, GetObjectRequest input = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="versionId">版本</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync([NotNull] string name, string versionId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除对象
        /// </summary>
        /// <param name="input">参数</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeleteObjectsResult> DeleteBatchAsync([NotNull] DeleteObjectsRequest input, CancellationToken cancellationToken = default);


    }
}
