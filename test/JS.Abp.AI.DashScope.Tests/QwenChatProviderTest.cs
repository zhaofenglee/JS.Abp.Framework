using JS.Abp.RestSharp;
using Shouldly;
using NSubstitute;
using Volo.Abp.Json;
using Volo.Abp.Testing;

namespace JS.Abp.AI.DashScope.Tests;

public class QwenChatProviderTest: AbpIntegratedTest<AbpAIDashScopeTestModule>
{
    [Fact]
    public async Task CreateCompletionTest()
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

        // var chat = CreateQwenChat();
        // var result = await chat.CreateCompletion("qwen-max", chatMessages,1500);
        // result.ShouldNotBeNullOrWhiteSpace();
        // result.ShouldContain("成功");
    }


    private  QwenChatProvider CreateQwenChat()
    {
        var config = Substitute.For<IDashScopeSettingConfiguration>();
        config.GetApiKeyAsync().Returns("sk-");
        var restSharp = GetRequiredService<IRestSharpManager>();
        var json = GetRequiredService<IJsonSerializer>();
        var qwenChat = new QwenChatProvider(config,restSharp,json);
        return qwenChat;
    }
}