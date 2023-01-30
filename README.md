# HuaweiCloudObs
huawei cloud obs .net sdk

# 安装
安装nuget `HuaweiCloudObs`

# 使用
在appsettings.json里添加配置
```json
"HuaweiCloudObs": {
    "EndPoint": "obs.cn-east-3.myhuaweicloud.com",
    "AccessKey": "xxx",
    "SecretKey": "xxx"
  }
```

在Startup.cs的ConfigureService()方法里加入完成注入. 需传入IConfigurationRoot
```CSharp
services.AddHuaweiCloudObs(configuration);
```

需要使用的地方注入对应接口
以Abp的appservice为例,如MyAppService. 直接使用Controller也是差不多,注入后即可使用
```CSharp
public class MyObsAppService : ApplicationService
{
  private readonly IObsObjectApi _objectApi;
  public MyObsAppService(IObsObjectApi api)
  {
    api.Bucket = "MyBucket";//设置bucket,可以放在配置里注入,也可存入数据库后读取
    _objectApi = api;
  }

  public async Task UploadAsync(IFormFile file, string name)
  {
     var bytes = await file.GetAllBytes();
     await _objectApi.PutAsync(name, file);
  }

  public Task DeleteAsync(string name)
  {
    return _objectApi.DeleteAsync(name);
  }

  public async Task DeleteBatchAsync(IEnumerable<string> names)
  {
    var result = await _objectApi.DeleteBatchAsync(new DeleteObjectsRequest
    {
      Objects = name.Select(f => new DeleteObject{ Key = f}).ToList()
    });

    if(result.Errors.Count > 0)
    {
      //存在删除失败对象
      //TODO: 删除失败处理
    }
  }
}
```


# 路线图
## 对象操作
+ [x] [PUT上传](https://support.huaweicloud.com/api-obs/obs_04_0080.html)
+ [ ] [POST上传](https://support.huaweicloud.com/api-obs/obs_04_0081.html)
+ [ ] [复制对象](https://support.huaweicloud.com/api-obs/obs_04_0082.html)
+ [x] [获取对象内容](https://support.huaweicloud.com/api-obs/obs_04_0083.html)
+ [ ] [获取对象元数据](https://support.huaweicloud.com/api-obs/obs_04_0084.html)
+ [x] [删除对象](https://support.huaweicloud.com/api-obs/obs_04_0085.html)
+ [x] [批量删除对象](https://support.huaweicloud.com/api-obs/obs_04_0086.html)
+ [ ] [取回归档存储对象](https://support.huaweicloud.com/api-obs/obs_04_0087.html)
+ [ ] [追加写对象](https://support.huaweicloud.com/api-obs/obs_04_0088.html)
+ [ ] [设置对象ACL](https://support.huaweicloud.com/api-obs/obs_04_0089.html)
+ [ ] [获取对象ACL](https://support.huaweicloud.com/api-obs/obs_04_0090.html)
+ [ ] [修改对象元数据](https://support.huaweicloud.com/api-obs/obs_04_0091.html)
+ [ ] [修改写对象](https://support.huaweicloud.com/api-obs/obs_04_0092.html)
+ [ ] [截断对象](https://support.huaweicloud.com/api-obs/obs_04_0093.html)
+ [ ] [重命名对象](https://support.huaweicloud.com/api-obs/obs_04_0094.html)


## 桶操作
### 基础操作
+ [x] [获取桶列表](https://support.huaweicloud.com/api-obs/obs_04_0020.html) IObsObjectApi.
+ [x] [创建桶](https://support.huaweicloud.com/api-obs/obs_04_0021.html)
+ [x] [列举桶内对象](https://support.huaweicloud.com/api-obs/obs_04_0010.html)
+ [x] [列举桶内对象v2](https://support.huaweicloud.com/api-obs/obs_04_0160.html)
+ [x] [获取桶元数据](https://support.huaweicloud.com/api-obs/obs_04_0023.html)
+ [x] [获取桶区域位置](https://support.huaweicloud.com/api-obs/obs_04_0024.html)
+ [x] [删除桶](https://support.huaweicloud.com/api-obs/obs_04_0025.html)
### 高级操作
+ [ ] [设置桶策略](https://support.huaweicloud.com/api-obs/obs_04_0027.html)
+ [ ] [获取桶策略](https://support.huaweicloud.com/api-obs/obs_04_0028.html)
+ [ ] [删除桶策略](https://support.huaweicloud.com/api-obs/obs_04_0029.html)
+ [ ] [设置桶ACL](https://support.huaweicloud.com/api-obs/obs_04_0030.html)
+ [ ] [获取桶ACL](https://support.huaweicloud.com/api-obs/obs_04_0031.html)
+ [ ] [设置桶日志管理配置](https://support.huaweicloud.com/api-obs/obs_04_0032.html)
+ [ ] [获取桶日志管理配置](https://support.huaweicloud.com/api-obs/obs_04_0033.html)
+ [ ] [设置桶的生命周期配置](https://support.huaweicloud.com/api-obs/obs_04_0034.html)
+ [ ] [获取桶的生命周期配置](https://support.huaweicloud.com/api-obs/obs_04_0035.html)
+ [ ] [删除桶的生命周期配置](https://support.huaweicloud.com/api-obs/obs_04_0036.html)
+ [ ] [设置桶的多版本状态](https://support.huaweicloud.com/api-obs/obs_04_0037.html)
+ [ ] [获取桶的多版本状态](https://support.huaweicloud.com/api-obs/obs_04_0038.html)
+ [ ] [设置桶的消息通知配置](https://support.huaweicloud.com/api-obs/obs_04_0039.html)
+ [ ] [获取桶的消息通知配置](https://support.huaweicloud.com/api-obs/obs_04_0040.html)
+ [ ] [设置桶默认存储类型](https://support.huaweicloud.com/api-obs/obs_04_0044.html)
+ [ ] [获取桶默认存储类型](https://support.huaweicloud.com/api-obs/obs_04_0045.html)
+ [ ] [设置桶的跨区域复制配置](https://support.huaweicloud.com/api-obs/obs_04_0046.html)
+ [ ] [获取桶的跨区域复制配置](https://support.huaweicloud.com/api-obs/obs_04_0047.html)
+ [ ] [删除桶的跨区域复制配置](https://support.huaweicloud.com/api-obs/obs_04_0048.html)
+ [ ] [设置桶标签](https://support.huaweicloud.com/api-obs/obs_04_0049.html)
+ [ ] [获取桶标签](https://support.huaweicloud.com/api-obs/obs_04_0050.html)
+ [ ] [删除桶标签](https://support.huaweicloud.com/api-obs/obs_04_0051.html)
+ [ ] [设置桶配额](https://support.huaweicloud.com/api-obs/obs_04_0052.html)
+ [ ] [获取桶配额](https://support.huaweicloud.com/api-obs/obs_04_0053.html)
+ [ ] [获取桶存量信息](https://support.huaweicloud.com/api-obs/obs_04_0054.html)
+ [ ] [设置桶清单](https://support.huaweicloud.com/api-obs/obs_04_0055.html)
+ [ ] [获取桶清单](https://support.huaweicloud.com/api-obs/obs_04_0056.html)
+ [ ] [列举桶清单](https://support.huaweicloud.com/api-obs/obs_04_0057.html)
+ [ ] [删除桶清单](https://support.huaweicloud.com/api-obs/obs_04_0058.html)
+ [ ] [设置桶的自定义域名](https://support.huaweicloud.com/api-obs/obs_04_0059.html)
+ [ ] [获取桶的自定义域名](https://support.huaweicloud.com/api-obs/obs_04_0060.html)
+ [ ] [删除桶的自定义域名](https://support.huaweicloud.com/api-obs/obs_04_0061.html)
+ [ ] [设置桶的加密配置](https://support.huaweicloud.com/api-obs/obs_04_0062.html)
+ [ ] [获取桶的加密配置](https://support.huaweicloud.com/api-obs/obs_04_0063.html)
+ [ ] [删除桶的加密配置](https://support.huaweicloud.com/api-obs/obs_04_0064.html)
+ [ ] [设置桶归档对象直读策略](https://support.huaweicloud.com/api-obs/obs_04_0065.html)
+ [ ] [获取桶归档对象直读策略](https://support.huaweicloud.com/api-obs/obs_04_0066.html)
+ [ ] [删除桶归档对象直读策略](https://support.huaweicloud.com/api-obs/obs_04_0067.html)
+ [ ] [设置镜像回源规则](https://support.huaweicloud.com/api-obs/obs_04_0119.html)
+ [ ] [获取镜像回源规则](https://support.huaweicloud.com/api-obs/obs_04_0120.html)
+ [ ] [删除镜像回源规则](https://support.huaweicloud.com/api-obs/obs_04_0121.html)
+ [ ] [设置在线解压策略](https://support.huaweicloud.com/api-obs/obs_04_0148.html)
+ [ ] [获取在线解压策略](https://support.huaweicloud.com/api-obs/obs_04_0149.html)
+ [ ] [删除在线解压策略](https://support.huaweicloud.com/api-obs/obs_04_0150.html)
## 