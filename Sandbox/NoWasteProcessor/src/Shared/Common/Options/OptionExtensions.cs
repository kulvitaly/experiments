namespace Common.Options;

public static class OptionExtensions
{
    public static Option<TResult> Map<T, TResult>(this Option<T> obj, Func<T, TResult> map)
        => obj is Some<T> some ? new Some<TResult>(map(some.Value)) : new None<TResult>();

    public static Option<T> Filter<T>(this Option<T> obj, Func<T, bool> predicate)
        => obj is Some<T> some && !predicate(some.Value) ? new None<T>() : obj;

    public static T Reduce<T>(this Option<T> obj, T substitute)
        => obj is Some<T> some ? some.Value : substitute;

    public static T Reduce<T>(this Option<T> obj, Func<T> substitute)
        => obj is Some<T> some ? some.Value : substitute();
}