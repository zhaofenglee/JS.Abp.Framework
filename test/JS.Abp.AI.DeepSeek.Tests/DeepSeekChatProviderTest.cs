using JS.Abp.AI.DashScope;
using JS.Abp.RestSharp;
using NSubstitute;
using Shouldly;
using Volo.Abp.Json;
using Volo.Abp.Testing;
using Xunit;

namespace JS.Abp.AI.DeepSeek.Tests;

public class DeepSeekChatProviderTest: AbpIntegratedTest<AbpAIDeepSeekTestModule>
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

        var chat = CreateDeepSeekChat();
        var result = await chat.CreateCompletion("deepseek-chat", chatMessages,1500);
        result.ShouldNotBeNullOrWhiteSpace();
        result.ShouldContain("成功");
    }


    private  DeepSeekChatProvider CreateDeepSeekChat()
    {
        var config = Substitute.For<IDeepSeekSettingConfiguration>();
        config.GetApiKeyAsync().Returns("sk-");
        config.GetEndpointAsync().Returns("https://api.deepseek.com/chat/completions");
        var restSharp = GetRequiredService<IRestSharpManager>();
        var json = GetRequiredService<IJsonSerializer>();
        var deepSeeChat = new DeepSeekChatProvider(config,restSharp,json);
        return deepSeeChat;
    }
}