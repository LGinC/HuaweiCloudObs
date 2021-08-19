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
以Abp的appservice为例,如MyAppService
```CSharp
public class MyAppService : ApplicationService
{
  private readonly IObsObjectApi _objectApi;
  public MyAppService(IObsObjectApi api)
  {
    api.Bucket = "MyBucket";//设置bucket,可以放在配置里注入,也可存入数据库后读取
    _objectApi = api;
  }

  public async Task UploadAsync(IFormFile file)
  {
     await using (var stream = excel.OpenReadStream());
  }
}
```
