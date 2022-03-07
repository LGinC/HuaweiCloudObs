using HuaweiCloudObs.Models;
using HuaweiCloudObs.Models.Buckets;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public interface IObsBucketApi
    {
        /// <summary>
        /// <see href="https://support.huaweicloud.com/api-obs/obs_04_0020.html">获取桶列表</see>
        /// </summary>
        /// <param name="bucketType">桶类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ListAllMyBucketsResult> GetBucketsAsync(string bucketType = "OBJECT", CancellationToken cancellationToken = default);

        /// <summary>
        /// <see href="https://support.huaweicloud.com/api-obs/obs_04_0021.html">创建桶</see>
        /// </summary>
        /// <param name="input">创建桶基本参数</param>
        /// <param name="headers">附加消息头</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateAsync(CreateBucketRequest input, CreateBucketHeaders headers = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <see href="https://support.huaweicloud.com/api-obs/obs_04_0022.html">列举桶内对象</see>
        /// </summary>
        /// <param name="bucket">桶名称</param>
        /// <param name="request">附加参数</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ListBucketResult> GetObjectsAsync(string bucket, GetObjectsRequest request = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <see href="https://support.huaweicloud.com/api-obs/obs_04_0160.html">列举桶内对象V2</see>
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="requestParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ListBucketResult> GetObjectsV2Async(string bucket, GetObjectsV2Request request = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <see href="https://support.huaweicloud.com/api-obs/obs_04_0023.html">获取桶元数据</see>
        /// </summary>
        /// <param name="bucket">桶名称</param>
        /// <param name="request">附加请求头</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BucketMetadata> GetMetadataAsync(string bucket, GetBucketMetadataRequest request = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <see href="https://support.huaweicloud.com/api-obs/obs_04_0024.html">获取桶区域位置</see>
        /// </summary>
        /// <param name="bucket">桶名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetLocationAsync(string bucket, CancellationToken cancellationToken = default);

        /// <summary>
        /// <see href="https://support.huaweicloud.com/api-obs/obs_04_0025.html">删除桶</see>
        /// <para>只有桶的所有者或者拥有桶的删桶policy权限的用户可以执行删除桶的操作，要删除的桶必须是空桶。如果桶中有对象或者有多段任务则认为桶不为空，可以使用列举桶内对象和列举出多段上传任务接口来确认桶是否为空。</para>
        /// <para>如果删除桶时，服务端返回5XX错误或超时，系统需要时间进行桶信息一致性处理，在此期间桶的信息会不准确，过一段时间再查看桶是否删除成功，查询到桶，需要再次发送删除桶消息</para>
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(string bucket, CancellationToken cancellationToken = default);
    }
}
