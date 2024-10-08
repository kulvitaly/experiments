namespace Common.Options;

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
    public static None Value { get; } = new();
    public static Option<T> Of<T>() => new None<T>();
}

public sealed class Some<T> : Option<T>
{
    public T Value { get; }

    public Some(T value) => Value = value;

    public override string ToString() => $"Some {Value?.ToString() ?? "<null>"}";
}

public sealed class None<T> : Option<T>
{
    public override string ToString() => "None";
}