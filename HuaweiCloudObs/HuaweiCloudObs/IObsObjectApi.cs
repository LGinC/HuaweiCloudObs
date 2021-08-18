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
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="data">对象数据</param>
        /// <returns></returns>
        Task PutAsync(string name, byte[] data, CancellationToken cancellationToken = default);
    }
}
