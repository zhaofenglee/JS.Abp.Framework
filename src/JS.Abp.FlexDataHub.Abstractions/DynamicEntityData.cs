namespace JS.Abp.FlexDataHub;

public class DynamicEntityData
{
    public int Row { get; set; }

    public Dictionary<string, object?> Data { get; } = new Dictionary<string, object?>();
}