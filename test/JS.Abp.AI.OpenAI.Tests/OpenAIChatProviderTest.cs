using NSubstitute;
using Shouldly;
using Volo.Abp.Json;
using Volo.Abp.Testing;

namespace JS.Abp.AI.OpenAI.Tests;

public class OpenAIChatProviderTest: AbpIntegratedTest<AbpAIOpenAITestModule>
{
    [Fact]
    public async Task CreateCompletionTest()
    {
        
        List<ChatMessages> chatMessages = new List<ChatMessages>()
        {
            new ChatMessages()
            {
                Role = "user",
                Content = "这是一个很简单的测试，如果可以，请返回成功"
            }
        };
       
        var chat = CreateOpenAIChat();
        var result = await chat.CreateCompletion("gpt-3.5-turbo-16k", chatMessages,2048);
        result.ShouldNotBeNullOrWhiteSpace();
        result.ShouldContain("成功");
        
    }
    
    private  OpenAIChatProvider CreateOpenAIChat()
    {
        var config = Substitute.For<IOpenAISettingConfiguration>();
        config.GetApiKeyAsync().Returns("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");
        config.GetProxyAsync().Returns("http://127.0.0.1:10809");
        var json = GetRequiredService<IJsonSerializer>();
        var openaiChat = new OpenAIChatProvider(config,json);
        return openaiChat;
    }
}