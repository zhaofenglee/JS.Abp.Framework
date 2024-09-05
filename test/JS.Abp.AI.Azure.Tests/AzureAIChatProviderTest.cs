using JS.Abp.AI.OpenAI;
using NSubstitute;
using Shouldly;
using Volo.Abp.Json;
using Volo.Abp.Testing;
using Xunit;

namespace JS.Abp.AI.Azure.Tests;

public class AzureAIChatProviderTest: AbpIntegratedTest<AbpAIAzureTestModule>
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
        
        var chat = CreateAzureAIChat();
        var result = await chat.CreateCompletion("gpt-4o-mini", chatMessages,100);
        result.ShouldNotBeNullOrWhiteSpace();
        result.ShouldContain("成功");
        
    }
    
    private  AzureAIChatProvider CreateAzureAIChat()
    {
        var config = Substitute.For<IAzureSettingConfiguration>();
        config.GetEndpointAsync().Returns("https://models.inference.ai.azure.com");
        config.GetApiKeyAsync().Returns("");
        var json = GetRequiredService<IJsonSerializer>();
        var azureAiChat = new AzureAIChatProvider(config,json);
        return azureAiChat;
    }
}