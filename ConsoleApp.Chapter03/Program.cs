using BenchmarkDotNet.Running;
using ConsoleApp.Chapter03;

//var summary = BenchmarkRunner.Run<StructVsClassesBenchamarks>();
//var summary = BenchmarkRunner.Run<CollectionBenchmark>();
//var summary = BenchmarkRunner.Run<ListBenchmark>();
var summary = BenchmarkRunner.Run<ContiguousMemoryBenchmark>();

SmallObjectManager manager = new SmallObjectManager();

// Example: Adding a large amount of data in chunks
manager.AddData(GenerateLargeDataSet());

// Retrieve and process the data
foreach (var item in manager.GetData())
{
    // Process each item
    Console.WriteLine(item);
}
    

// Simulate generating a large set of data
static IEnumerable<int> GenerateLargeDataSet()
{
const int largeSize = 100_000;
for (int i = 0; i < largeSize; i++)
{
    yield return i;
}
}
