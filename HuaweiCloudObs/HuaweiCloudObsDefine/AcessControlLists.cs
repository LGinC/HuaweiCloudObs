using System.Runtime.Serialization;

namespace HuaweiCloudObsDefine
{
    /// <summary>
    /// <see href="https://support.huaweicloud.com/perms-cfg-obs/obs_40_0005.html">访问控制列表</see>
    /// </summary>
    public enum AcessControlLists
    {
        /// <summary>
        /// 私有
        /// <para>桶或对象的所有者拥有完全控制的权限，其他任何人都没有访问权限</para>
        /// </summary>
        [EnumMember(Value = "private")]
        Private,
        /// <summary>
        /// 公共读
        /// <para>设在桶上，所有人可以获取该桶内对象列表、桶内多段任务、桶的元数据、桶的多版本。设在对象上，所有人可以获取该对象内容和元数据。</para>
        /// </summary>
        [EnumMember(Value = "public-read")]
        PublicRead,
        /// <summary>
        /// 公共读写
        /// <para>设在桶上，所有人可以获取该桶内对象列表、桶内多段任务、桶的元数据、桶的多版本、上传对象删除对象、初始化段任务、上传段、合并段、拷贝段、取消多段上传任务。 设在对象上，所有人可以获取该对象内容和元数据。</para>
        /// </summary>
        [EnumMember(Value = "public-read-write")]
        PublicReadWrite,
        /// <summary>
        /// 公共读 投递
        /// <para>设在桶上，所有人可以获取该桶内对象列表、桶内多段任务、桶的元数据、桶的多版本，可以获取该桶内对象的内容和元数据。不能应用在对象上。</para>
        /// </summary>
        [EnumMember(Value = "public-read-delivered")]
        PublicReadDelivered,
        /// <summary>
        /// 公共读写 投递
        /// <para>设在桶上，所有人可以获取该桶内对象列表、桶内多段任务、桶的元数据、桶的多版本、上传对象删除对象、初始化段任务、上传段、合并段、拷贝段、取消多段上传任务，可以获取该桶内对象的内容和元数据。不能应用在对象上。</para>
        /// </summary>
        [EnumMember(Value = "public-read-write-delivered")]
        publicReadWriteDelivered,
        /// <summary>
        /// 设在对象上，桶或对象的所有者拥有完全控制的权限，其他任何人都没有访问权限。
        /// </summary>
        [EnumMember(Value = "bucket-owner-full-control")]
        BicketOwnerFullControl,

    }
}
