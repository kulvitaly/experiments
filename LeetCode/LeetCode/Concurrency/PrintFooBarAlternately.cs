namespace LeetCode.Concurrency.PrintFooBarAlternately;

public abstract class FooBar(int n)
{
    protected readonly int n = n;

    public abstract void Foo(Action printFoo);

    public abstract void Bar(Action printBar);
}

// Manual reset event slim = 43 ms, 32.45 MB
public class ManualResetEventFooBar(int n) : FooBar(n)
{
    private readonly ManualResetEventSlim _firstDone = new(initialState: false);
    private readonly ManualResetEventSlim _secondDone = new(initialState: true);

    public override void Foo(Action printFoo)
    {

        for (int i = 0; i < n; i++)
        {
            _secondDone.Wait();
            // printFoo() outputs "foo". Do not change or remove this line.
            printFoo();

            _secondDone.Reset();
            _firstDone.Set();
        }
    }

    public override void Bar(Action printBar)
    {
        for (int i = 0; i < n; i++)
        {
            _firstDone.Wait();

            // printBar() outputs "bar". Do not change or remove this line.
            printBar();

            _firstDone.Reset();
            _secondDone.Set();
        }
    }
}

// Auto reset event - 40 ms, 30.87 MB
public class AutoResetEventFooBar(int n) : FooBar(n)
{
    private readonly AutoResetEvent _firstDone = new(initialState: false);
    private readonly AutoResetEvent _secondDone = new(initialState: true);

    public override void Foo(Action printFoo)
    {
        for (int i = 0; i < n; i++)
        {
            _secondDone.WaitOne();
            // printFoo() outputs "foo". Do not change or remove this line.
            printFoo();

            _firstDone.Set();
        }
    }

    public override void Bar(Action printBar)
    {
        for (int i = 0; i < n; i++)
        {
            _firstDone.WaitOne();

            // printBar() outputs "bar". Do not change or remove this line.
            printBar();

            _secondDone.Set();
        }
    }
}

// Spin wait - 32 ms, 32.81 MB
public class SpinWaitFooBar(int n) : FooBar(n)
{
    private volatile int _state = 0;

    public override void Foo(Action printFoo)
    {

        for (int i = 0; i < n; i++)
        {
            SpinWait.SpinUntil(() => _state == 0);
            // printFoo() outputs "foo". Do not change or remove this line.
            printFoo();
            _state = 1;
        }
    }

    public override void Bar(Action printBar)
    {
        for (int i = 0; i < n; i++)
        {
            SpinWait.SpinUntil(() => _state == 1);
            // printBar() outputs "bar". Do not change or remove this line.
            printBar();
            _state = 0;
        }
    }
}

