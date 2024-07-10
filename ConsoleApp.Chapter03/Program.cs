using BenchmarkDotNet.Running;
using ConsoleApp.Chapter03;

BenchmarkRunner.Run<StructVsClassesBenchamarks>();
BenchmarkRunner.Run<CollectionBenchmark>();
BenchmarkRunner.Run<ListBenchmark>();
BenchmarkRunner.Run<ContiguousMemoryBenchmark>();

