# JS.Abp.AI.DashScope
DashScope灵积API接口封装，目前支持有
* 通义千问


### 使用方法
1.添加"JS.Abp.AI.DashScope"包  
2.在你的Module类添加[DependsOn(typeof(AbpAIDashScopeModule))]  
3. appsettings.json中添加配置
```json
{
  "Settings": {
    "Abp.AI.DashScope.ApiKey": "你的ApiKey"
  }
}
```
4.Demo
```csharp
public class MyService : ITransientDependency
    {
        private readonly QwenChatProvider _qwenChatProvider;

        public MyService(QwenChatProvider qwenChatProvider)
        {
            _qwenChatProvider = qwenChatProvider;
        }

        public async Task<string> GetAsync()
        {
                List<ChatMessages> chatMessages = new List<ChatMessages>()
        {
            new ChatMessages()
            {
                Role = "system",
                Content = "You are a helpful assistant."
            },
            new ChatMessages()
            {
                Role = "user",
                Content = "这是一个很简单的测试，如果可以，请返回成功"
            }
        };
            var result = await _qwenChatProvider.CreateCompletion("qwen-max", chatMessages,1500);
            return result;
        }
    }


```