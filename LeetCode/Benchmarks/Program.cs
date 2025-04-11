// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Benchmarks.DynamicProgramming;
using Benchmarks.Graph;


var config = ManualConfig.CreateMinimumViable()
            .WithOption(ConfigOptions.DisableOptimizationsValidator, true)
            .WithSummaryStyle(BenchmarkDotNet.Reports.SummaryStyle.Default)
            .WithOptions(ConfigOptions.JoinSummary)
            //.AddDiagnoser(new EtwProfiler()) // Enable ETW (Event Tracing for Windows)
            //.AddDiagnoser(new MemoryDiagnoser()) // Track memory usage
            .AddJob(Job.Default.WithId("ProfilingJob").WithLaunchCount(1));

BenchmarkRunner.Run<ReconstructItineraryBenchMark>(config);

//var benchmark = new ReconstructItineraryBenchMark();
//
//Console.WriteLine("before start");
//
//benchmark.ImmutableCollectionSolution();
//
//Console.WriteLine("finished");