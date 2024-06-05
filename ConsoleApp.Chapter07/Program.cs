////static unsafe void UnsafeMethod()
////{
////    int num = 10;
////    int* p = &num;

////    Console.WriteLine("Value: {0}", num);
////    Console.WriteLine("Address: {0:X}", (int)p);
////}


////UnsafeMethod();

//public struct Point
//{
//    public int X;
//    public int Y;
//}


//unsafe
//{
//    Point point = new Point { X = 10, Y = 20 };
//    Point* ptr = &point;

//    ValidateAndPrintPointer(ptr);

//    Point* nullPtr = null;
//    ValidateAndPrintPointer(nullPtr);

//    public static unsafe void ValidateAndPrintPointer(Point* ptr)
//    {
//        if (ptr == null)
//        {
//            Console.WriteLine("Pointer is null. Cannot dereference.");
//        }
//        else
//        {
//            Console.WriteLine("Pointer is valid.");
//            Console.WriteLine($"X: {ptr->X}, Y: {ptr->Y}");
//        }
//    }
//}

//int[] numbers = { 1, 2, 3, 4, 5 };

//unsafe
//{
//    fixed (int* ptr = numbers)
//    {
//        for (int i = 0; i < numbers.Length; i++)
//        {
//            Console.WriteLine(ptr[i]);
//        }
//    }
//}

//string text = "Hello, World!";

//unsafe
//{
//    fixed (char* ptr = text)
//    {
//        for (int i = 0; i < text.Length; i++)
//        {
//            Console.WriteLine(ptr[i]);
//        }
//    }
//}


//int[] array1 = { 1, 2, 3 };
//int[] array2 = { 4, 5, 6 };

//unsafe
//{
//    fixed (int* ptr1 = array1)
//    fixed (int* ptr2 = array2)
//    {
//        for (int i = 0; i < array1.Length; i++)
//        {
//            Console.WriteLine($"Array1[{i}] = {ptr1[i]}, Array2[{i}] = {ptr2[i]}");
//        }
//    }
//}


using System.Runtime.InteropServices;

// Declare the MessageBox function from user32.dll
//[DllImport("user32.dll", CharSet = CharSet.Auto)]
static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

// Call the MessageBox function
MessageBox(IntPtr.Zero, "Hello, World!", "P/Invoke Example", 0);


public static class SQLite
{
    [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int sqlite3_open(string filename, out IntPtr db);

    [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int sqlite3_close(IntPtr db);

    [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int sqlite3_exec(IntPtr db, string sql, IntPtr callback, IntPtr arg, out IntPtr errmsg);
}

IntPtr db;
IntPtr errmsg;

// Open the database
if (SQLite.sqlite3_open("test.db", out db) != 0)
{
    Console.WriteLine("Failed to open database");
    return;
}

// Execute an SQL statement
string sql = "CREATE TABLE IF NOT EXISTS Test (Id INTEGER PRIMARY KEY, Name TEXT)";
if (SQLite.sqlite3_exec(db, sql, IntPtr.Zero, IntPtr.Zero, out errmsg) != 0)
{
    Console.WriteLine("Failed to execute SQL: " + Marshal.PtrToStringAnsi(errmsg));
    SQLite.sqlite3_close(db);
    return;
}

Console.WriteLine("Table created successfully");

// Close the database
SQLite.sqlite3_close(db);

