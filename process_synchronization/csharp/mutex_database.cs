// To run this C# program from the windows commandline, first run the following :
//      SET PATH=%PATH%;"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn"
// the run the code by calling :
//      csc.exe mutex_database.cs
// then run the program :
//      mutex_database.exe

// This compile directive allows us to switch between the thread-safe and non-thread-safe version of the program.
//#define THREAD_SAFE       // <-- UNCOMMENT This to make this program thread-safe

using System;
using System.Threading;

/// <summary>
///  This class illustrates a thread-safe implementation of a database access using Mutexes.
///  
///  As threads want to access the database, they have to acquire a lock.
///  This ensures that only one thread can access the database at a time.
///  But more importantly, it ensures that only one thread can write to the database at a time.
///  This is important because we don't want to have two threads writing to the database at the same time.
///  If we don't use a mutex, we could have a thread writing to the database while another thread is reading from it.
///  This could lead to a corrupted database.
/// </summary>
class DatabaseAccess
{
    #if THREAD_SAFE
    private static Mutex dbMutex = new Mutex();
    #endif

    public void WriteRecords()
    {
        #if THREAD_SAFE
        dbMutex.WaitOne();
        #endif 

        // Start of the critical section
        Console.WriteLine($"{DateTime.Now:ss.fffffff} Thread {Thread.CurrentThread.ManagedThreadId} is accessing the database.");
        for(var i=0; i<5; i++) {
            Console.WriteLine($"{DateTime.Now:ss.fffffff} Thread {Thread.CurrentThread.ManagedThreadId} is writing to the database.");
            Thread.Sleep(500); // Simulate database access
        }
        Console.WriteLine($"{DateTime.Now:ss.fffffff} Thread {Thread.CurrentThread.ManagedThreadId} has finished with the database.");
        // end of the critical section

        #if THREAD_SAFE
        dbMutex.ReleaseMutex();
        #endif

    }
}

class Program
{
    static void Main()
    {
        DatabaseAccess dbAccess = new DatabaseAccess();

        Thread[] threads = new Thread[5];

        for (int i = 0; i < 5; i++)
        {
            threads[i] = new Thread(dbAccess.WriteRecords);
            threads[i].Start();
        }

        for (int i = 0; i < 5; i++)
        {
            threads[i].Join();
        }

        Console.ReadLine();
    }
}

// Output example (when THREAD_SAFE is defined)
/*
58.7093367 Thread 3 is accessing the database.
58.7163657 Thread 3 is writing to the database.
59.2177394 Thread 3 is writing to the database.
59.7188033 Thread 3 is writing to the database.
00.2199788 Thread 3 is writing to the database.
00.7207863 Thread 3 is writing to the database.
01.2213665 Thread 3 has finished with the database.
01.2213665 Thread 4 is accessing the database.
01.2213665 Thread 4 is writing to the database.
01.7223309 Thread 4 is writing to the database.
02.2232923 Thread 4 is writing to the database.
02.7235996 Thread 4 is writing to the database.
03.2245440 Thread 4 is writing to the database.
03.7258557 Thread 4 has finished with the database.
03.7258557 Thread 5 is accessing the database.
03.7258557 Thread 5 is writing to the database.
04.2268285 Thread 5 is writing to the database.
04.7275821 Thread 5 is writing to the database.
05.2282426 Thread 5 is writing to the database.
05.7293537 Thread 5 is writing to the database.
06.2303489 Thread 5 has finished with the database.
06.2303489 Thread 6 is accessing the database.
06.2303489 Thread 6 is writing to the database.
06.7308335 Thread 6 is writing to the database.
07.2318072 Thread 6 is writing to the database.
07.7328251 Thread 6 is writing to the database.
08.2336085 Thread 6 is writing to the database.
08.7344042 Thread 6 has finished with the database.
08.7344042 Thread 7 is accessing the database.
08.7344042 Thread 7 is writing to the database.
09.2349375 Thread 7 is writing to the database.
09.7359148 Thread 7 is writing to the database.
10.2364077 Thread 7 is writing to the database.
10.7374307 Thread 7 is writing to the database.
11.2384557 Thread 7 has finished with the database.
*/

// Output example (when THREAD_SAFE is NOT defined)
/*
28.4193563 Thread 6 is accessing the database.
28.4293565 Thread 6 is writing to the database.
28.4203749 Thread 4 is accessing the database.
28.4293565 Thread 4 is writing to the database.
28.4193563 Thread 7 is accessing the database.
28.4293565 Thread 7 is writing to the database.
28.4193563 Thread 3 is accessing the database.
28.4193563 Thread 5 is accessing the database.
28.4303610 Thread 5 is writing to the database.
28.4303610 Thread 3 is writing to the database.
28.9306433 Thread 4 is writing to the database.
28.9306433 Thread 6 is writing to the database.
28.9316540 Thread 3 is writing to the database.
28.9316540 Thread 5 is writing to the database.
28.9306433 Thread 7 is writing to the database.
29.4318370 Thread 4 is writing to the database.
29.4318370 Thread 6 is writing to the database.
29.4328495 Thread 3 is writing to the database.
29.4328495 Thread 5 is writing to the database.
29.4328495 Thread 7 is writing to the database.
29.9323825 Thread 4 is writing to the database.
29.9323825 Thread 6 is writing to the database.
29.9333723 Thread 3 is writing to the database.
29.9333723 Thread 7 is writing to the database.
29.9333723 Thread 5 is writing to the database.
30.4329811 Thread 6 is writing to the database.
30.4329811 Thread 4 is writing to the database.
30.4339797 Thread 5 is writing to the database.
30.4339797 Thread 7 is writing to the database.
30.4339797 Thread 3 is writing to the database.
30.9332814 Thread 6 has finished with the database.
30.9342736 Thread 4 has finished with the database.
30.9342736 Thread 5 has finished with the database.
30.9352724 Thread 3 has finished with the database.
30.9342736 Thread 7 has finished with the database.
*/