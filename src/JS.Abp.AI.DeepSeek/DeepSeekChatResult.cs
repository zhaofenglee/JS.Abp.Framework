using System.Text.Json.Serialization;

namespace JS.Abp.AI.DeepSeek;

public class DeepSeekChatResult
{
    public string Id { get; set; }
    public string Object { get; set; }
    public int Created { get; set; }
    public string Model { get; set; }
    public Choices[] Choices { get; set; }
    public Usage Usage { get; set; }
    [JsonPropertyName("system_fingerprint")]
    public string SystemFingerprint { get; set; }
}

public class Choices
{
    public int Index { get; set; }
    public Message Message { get; set; }
    public object Logprobs { get; set; }
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; }
}

public class Message
{
    public string Role { get; set; }
    public string Content { get; set; }
}

public class Usage
{
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
    [JsonPropertyName("prompt_tokens_details")]
    public PromptTokensDetails PromptTokensDetails { get; set; }
    [JsonPropertyName("prompt_cache_hit_tokens")]
    public int PromptCacheHitTokens { get; set; }
    [JsonPropertyName("prompt_cache_miss_tokens")]
    public int PromptCacheMissTokens { get; set; }
}

public class PromptTokensDetails
{
    [JsonPropertyName("cached_tokens")]
    public int CachedTokens { get; set; }
}

