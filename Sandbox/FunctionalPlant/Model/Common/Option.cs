namespace Model.Common;

public abstract class Option<T>
{
    public static implicit operator Option<T>(None _) => new None<T>();
    
    public static implicit operator Option<T>(T value) => new Some<T>(value);
}

public static class Option
{
    public static Option<T> Optional<T>(this T? obj) => obj == null ? None.Value : new Some<T>(obj);
}

public class None
{
    public static None Value {get; } = new();
    public static Option<T> Of<T>() => new None<T>();
}

public sealed class Some<T> : Option<T>
{
    public T Content {get;}

    public Some(T content) => Content = content;

    public override string ToString() => $"Some {Content?.ToString() ?? "<null>"}"; 
}

public sealed class None<T> : Option<T>
{
    public override string ToString() => "None";
}