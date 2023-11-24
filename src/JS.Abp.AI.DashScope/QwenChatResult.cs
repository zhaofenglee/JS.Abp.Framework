namespace JS.Abp.AI.DashScope;

public class QwenChatResult
{
    public QwenChatOutput output { get; set; }
    public QwenChatUsage usage { get; set; }
    public string request_id { get; set; }
}


public class QwenChatOutput
{
    public QwenChatChoices[] choices { get; set; }
}

public class QwenChatChoices
{
    public string finish_reason { get; set; }
    public ChatMessages message { get; set; }
}


public class QwenChatUsage
{
    public int total_tokens { get; set; }
    public int output_tokens { get; set; }
    public int input_tokens { get; set; }
}

