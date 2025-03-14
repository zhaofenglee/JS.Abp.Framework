﻿using System.Net;
using Betalgo.Ranul.OpenAI;
using Betalgo.Ranul.OpenAI.Managers;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace JS.Abp.AI.OpenAI;

public class OpenAIChatProvider:ChatProviderBase,ITransientDependency
{
    protected IOpenAISettingConfiguration OpenAIConfiguration { get; }
    public IJsonSerializer JsonSerializer { get; }
    
    public OpenAIChatProvider(IOpenAISettingConfiguration openAIConfiguration,IJsonSerializer jsonSerializer)
    {
        OpenAIConfiguration = openAIConfiguration;
        JsonSerializer = jsonSerializer;
    }

    
    public override async Task<string> CreateCompletion(string modelId, List<ChatMessages> chatMessages, int maxTokens = 1024, bool returnAllResults = false,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var openAiService = await BuildOpenAIServiceAsync();
        List<ChatMessage> chatMessage = new List<ChatMessage>();
        foreach (var message in chatMessages)
        {
            if (message.Role.ToLower() == "user")
            {
                chatMessage.Add(ChatMessage.FromUser(message.Content));
            }
            else
            {
                chatMessage.Add(ChatMessage.FromSystem(message.Content));
            }
        }
        var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = chatMessage,
            MaxTokens = maxTokens,
            Model = modelId,//Models.ChatGpt3_5Turbo0301
        });
        if (completionResult.Successful)
        {
            return completionResult.Choices.First().Message.Content;
        }
        else
        {
            if (completionResult.Error == null)
            {
                throw new UserFriendlyException("Unknown Error");
            }
            else
            {
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
                throw new UserFriendlyException($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        }
    }
    
    private async Task<OpenAIService> BuildOpenAIServiceAsync()
    {
        var baseDomain = await OpenAIConfiguration.GetBaseDomainAsync();
        var apiKey = await OpenAIConfiguration.GetApiKeyAsync();
        var proxy = await OpenAIConfiguration.GetProxyAsync();
        if (proxy.IsNullOrWhiteSpace())
        {
            return new OpenAIService(new OpenAIOptions()
            {
                ApiKey =  apiKey,
                BaseDomain = baseDomain,
            },new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(240)
            });
        }
        else
        {
            var handler = new HttpClientHandler()
            {
                Proxy = new WebProxy(proxy),
                UseProxy = true
            };
        
            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(240)
            };
            return new OpenAIService(new OpenAIOptions()
            {
                ApiKey =  apiKey,
                BaseDomain = baseDomain,
            },client);
        }
       
    }
}