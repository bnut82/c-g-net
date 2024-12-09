namespace Challenge.Application.Transformer;

public static class TransformStringByKey
{
    public static string Handle(string any , int key)
    {
        var strings = any.Split(",");
        
        return strings[key];
    }
}