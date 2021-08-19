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
