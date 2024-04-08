public class SmallObjectManager
{
    // Each array size is set to a value that won't be allocated on the LOH.
    private const int MaxArraySize = 20_000; // size * sizeof(int) < 85,000 bytes for int
    private List<int[]> arrays = new List<int[]>();

    public void AddData(IEnumerable<int> data)
    {
        int[] currentArray = null;
        int currentIndex = 0;

        foreach (var item in data)
        {
            // Allocate a new array when needed.
            if (currentArray == null || currentIndex >= MaxArraySize)
            {
                currentArray = new int[MaxArraySize];
                arrays.Add(currentArray);
                currentIndex = 0;
            }

            currentArray[currentIndex++] = item;
        }
    }

    public IEnumerable<int> GetData()
    {
        foreach (var array in arrays)
        {
            foreach (var item in array)
            {
                yield return item;
            }
        }
    }
}
