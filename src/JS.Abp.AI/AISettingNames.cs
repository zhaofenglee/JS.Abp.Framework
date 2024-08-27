namespace JS.Abp.AI;

public class AISettingNames
{
    public static class OpenAI
    {
        public const string BaseDomain = "Abp.AI.OpenAI.BaseDomain";
        
        public const string ApiKey = "Abp.AI.OpenAI.ApiKey";
        
        public const string Proxy = "Abp.AI.OpenAI.Proxy";
        
        public const string Model = "Abp.AI.OpenAI.Model";
    }
    
    public static class DashScope
    {
        public const string ApiKey = "Abp.AI.DashScope.ApiKey";
        public const string Model = "Abp.AI.DashScope.Model";
    }

    public static class AzureAI
    {
        public const string Endpoint  = "Abp.AI.AzureAI.Endpoint";
        public const string ApiKey = "Abp.AI.AzureAI.ApiKey";
        public const string Model = "Abp.AI.AzureAI.Model";
    }
}