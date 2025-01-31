using LeetCode.Concurrency.PrintFooBarAlternately;
using System.Collections.Concurrent;

namespace LeetCode.Tests.Concurrency;

public class PrintFooBarAlternatelyTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public async Task FooBar_ManualResetEvent_ReturnsExpected(int n)
        => await RunFooBar(new ManualResetEventFooBar(n), n);

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public async Task FooBar_AutoResetEvent_ReturnsExpected(int n)
        => await RunFooBar(new AutoResetEventFooBar(n), n);

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public async Task FooBar_SpinWait_ReturnsExpected(int n)
        => await RunFooBar(new SpinWaitFooBar(n), n);

    private async Task RunFooBar(FooBar fooBar, int n)
    {
        var expected = string.Concat(Enumerable.Repeat("foobar", n));
        var queue = new ConcurrentQueue<string>();

        var barTask = Task.Run(() => fooBar.Bar(() => queue.Enqueue("bar")));
        var fooTask = Task.Run(() => fooBar.Foo(() => queue.Enqueue("foo")));

        await Task.WhenAll(fooTask, barTask);

        string.Concat(queue).Should().Be(string.Concat(Enumerable.Repeat("foobar", n)));
    }
}
