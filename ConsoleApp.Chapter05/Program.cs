//////public class BankAccount
//////{
//////    private object balanceLock = new object();
//////    public int Balance { get; private set; }

//////    // Constructor to initialize the bank account with a balance
//////    public BankAccount(int startingBalance)
//////    {
//////        Balance = startingBalance;
//////    }

//////    // Method to deposit money into the account
//////    public void Deposit(int amount)
//////    {
//////        // Locking to ensure only one thread can enter this code block at a time
//////        lock (balanceLock)
//////        {
//////            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} entering deposit.");
//////            int initialBalance = Balance;
//////            Balance += amount;
//////            Console.WriteLine($"Deposited {amount}. Initial balance was {initialBalance}. New balance is {Balance}.");
//////        }
//////    }
//////}

//////class Program
//////{
//////    static void Main(string[] args)
//////    {
//////        BankAccount account = new BankAccount(1000);

//////        // Create multiple threads to simulate deposit
//////        Thread thread1 = new Thread(() => account.Deposit(500));
//////        Thread thread2 = new Thread(() => account.Deposit(300));

//////        thread1.Start();
//////        thread2.Start();

//////        thread1.Join();
//////        thread2.Join();

//////        Console.WriteLine($"Final balance is {account.Balance}.");
//////    }
//////}

////public class BankAccount
////{
////    private int balance;
////    private readonly object balanceLock = new object();

////    // Constructor to initialize the bank account with a balance
////    public BankAccount(int initialBalance)
////    {
////        balance = initialBalance;
////    }

////    // Property to safely access balance
////    public int Balance
////    {
////        get
////        {
////            lock (balanceLock)
////            {
////                return balance;
////            }
////        }
////        private set
////        {
////            lock (balanceLock)
////            {
////                balance = value;
////            }
////        }
////    }

////    // Method to deposit money into the account
////    public void Deposit(int amount)
////    {
////        bool lockTaken = false;
////        try
////        {
////            // Try to enter the lock
////            Monitor.TryEnter(balanceLock, ref lockTaken);
////            if (lockTaken)
////            {
////                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} entering deposit.");
////                int initialBalance = Balance;
////                Balance += amount;
////                Console.WriteLine($"Deposited {amount}. Initial balance was {initialBalance}. New balance is {Balance}.");
////            }
////            else
////            {
////                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} could not enter deposit method.");
////            }
////        }
////        finally
////        {
////            // Ensure the lock is released
////            if (lockTaken)
////            {
////                Monitor.Exit(balanceLock);
////            }
////        }
////    }
////}

////class Program
////{
////    static void Main(string[] args)
////    {
////        BankAccount account = new BankAccount(1000);

////        // Create multiple threads to simulate deposit
////        Thread thread1 = new Thread(() => account.Deposit(500));
////        Thread thread2 = new Thread(() => account.Deposit(300));

////        thread1.Start();
////        thread2.Start();

////        thread1.Join();
////        thread2.Join();

////        Console.WriteLine($"Final balance is {account.Balance}.");
////    }
////}

//public class BankAccount
//{
//    private int balance;
//    private static Mutex mutex = new Mutex();

//    // Constructor to initialize the bank account with a balance
//    public BankAccount(int initialBalance)
//    {
//        balance = initialBalance;
//    }

//    // Property to safely access balance
//    public int Balance
//    {
//        get
//        {
//            mutex.WaitOne(); // Acquire the mutex
//            int temp = balance;
//            mutex.ReleaseMutex(); // Release the mutex
//            return temp;
//        }
//        private set
//        {
//            mutex.WaitOne(); // Acquire the mutex
//            balance = value;
//            mutex.ReleaseMutex(); // Release the mutex
//        }
//    }

//    // Method to deposit money into the account
//    public void Deposit(int amount)
//    {
//        mutex.WaitOne(); // Acquire the mutex before entering critical section
//        try
//        {
//            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} entering deposit.");
//            int initialBalance = Balance;
//            Balance += amount;
//            Console.WriteLine($"Deposited {amount}. Initial balance was {initialBalance}. New balance is {Balance}.");
//        }
//        finally
//        {
//            mutex.ReleaseMutex(); // Always release the mutex in finally block
//        }
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        BankAccount account = new BankAccount(1000);

//        // Create multiple threads to simulate deposit
//        Thread thread1 = new Thread(() => account.Deposit(500));
//        Thread thread2 = new Thread(() => account.Deposit(300));

//        thread1.Start();
//        thread2.Start();

//        thread1.Join();
//        thread2.Join();

//        Console.WriteLine($"Final balance is {account.Balance}.");
//    }
//}

//public class Program
//{
//    public static void Main()
//    {
//        Task<int> task = Task.Run(() => ComputeResult());
//        Console.WriteLine($"Result: {task.Result}");
//    }

//    private static int ComputeResult()
//    {
//        // Simulate some computation
//        return new Random().Next(100);
//    }
//}

//namespace AsyncCounterExample
//{
//    class Program
//    {
//        static async Task Main(string[] args)
//        {
//            AsyncCounter counter = new AsyncCounter();

//            // Create an array of tasks to demonstrate concurrent increments
//            Task[] tasks = new Task[10];
//            for (int i = 0; i < tasks.Length; i++)
//            {
//                tasks[i] = Task.Run(async () =>
//                {
//                    await counter.IncrementAsync();
//                    Console.WriteLine($"Count after increment: {await counter.GetCountAsync()}");
//                });
//            }

//            // Wait for all tasks to complete
//            await Task.WhenAll(tasks);

//            // Final count
//            Console.WriteLine($"Final count: {await counter.GetCountAsync()}");
//        }
//    }

//    public class AsyncCounter
//    {
//        private int _count = 0;
//        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1); // Initial and maximum count is 1

//        public async Task IncrementAsync()
//        {
//            await _semaphore.WaitAsync(); // Asynchronously wait until the semaphore is available
//            try
//            {
//                _count++;
//            }
//            finally
//            {
//                _semaphore.Release(); // Release the semaphore
//            }
//        }

//        public async Task<int> GetCountAsync()
//        {
//            await _semaphore.WaitAsync();
//            try
//            {
//                return _count;
//            }
//            finally
//            {
//                _semaphore.Release();
//            }
//        }
//    }
//}

using System.Collections.Concurrent;

List<Data> dataList = GetDataList();
ConcurrentBag<Result> results = new ConcurrentBag<Result>();

// Create an instance of ParallelOptions
ParallelOptions options = new ParallelOptions();
options.MaxDegreeOfParallelism = 2; // Adjust the degree of parallelism as needed

// Execute the Parallel.ForEach loop with the specified options
Parallel.ForEach(dataList, options, data =>
{
    Result result = ProcessData(data);
    results.Add(result);
});
