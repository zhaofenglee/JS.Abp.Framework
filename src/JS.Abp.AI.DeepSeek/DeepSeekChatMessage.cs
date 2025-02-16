using System.Text.Json.Serialization;

namespace JS.Abp.AI.DeepSeek;

public class DeepSeekChatMessage
{
    [JsonPropertyName("model")]
    public string Model { get; set; }
    
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 1500;
    [JsonPropertyName("messages")]
    public ChatMessages[] Messages { get; set; }

}
