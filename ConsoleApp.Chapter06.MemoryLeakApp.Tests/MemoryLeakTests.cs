namespace ConsoleApp.Chapter06.MemoryLeakApp.Tests;

public class MemoryLeakTests
{
    [Fact]
    public void TestForMemoryLeak()
    {
        // Arrange
        var leaker = new MemoryLeaker();
        var initialMemory = GetTotalMemoryUsed();

        // Act
        leaker.LeakMemory();

        // Clean up - attempt to force garbage collection for accurate measurement
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var memoryAfterLeak = GetTotalMemoryUsed();

        // Assert
        Assert.True(memoryAfterLeak > initialMemory, "Memory usage did not increase, leak expected.");
    }

    private long GetTotalMemoryUsed()
    {
        // Forces garbage collection and returns the amount of memory currently allocated in bytes.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        return GC.GetTotalMemory(true);
    }
}