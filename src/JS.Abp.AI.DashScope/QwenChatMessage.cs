namespace JS.Abp.AI.DashScope;

public class QwenChatMessage
{
    public string Model { get; set; }
    public int max_tokens { get; set; } = 1500;
    public QwenChatInput Input { get; set; }
    public QwenChatParameters Parameters { get; set; }
   
}

public class QwenChatInput
{
    public ChatMessages[] Messages { get; set; }
}

public class QwenChatParameters
{
    public int Seed { get; set; }
    public string result_format { get; set; }
}

