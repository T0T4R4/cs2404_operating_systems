// To run this C# program from the windows commandline, first run the following :
//      SET PATH=%PATH%;"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn"
// the run the code by calling :
//      csc.exe monitor_readerwriter.cs
// then run the program :
//      monitor_readerwriter.exe

// This compile directive allows us to switch between the thread-safe and non-thread-safe version of the program.
//#define THREAD_SAFE       // <-- UNCOMMENT This to make this program thread-safe

using System;
using System.Threading;

///  <summary>
///  This class illustrate the use of locks and monitors to implement a solution to the reader-writer problem
///
///  A reader-writer problem is a synchronization problem where a resource is read and written by multiple threads.
/// 
///  The lock is used to ensure that only one thread can access the resource at a time.
///  The monitor is used to ensure that readers can access the resource at the same time, but writers cannot access the resource while readers are reading.
///  The number of readers currently reading the resource is stored in a shared variable (readersCount).
///  The readersCount variable is protected by the lock, so that only one thread can access it at a time.
///  The resourceValue illustrate the resource being read and written by the readers and writers.
///  The same lock is used to protect both the readersCount and the resourceValue.
///  </summary>
class MyReaderWriterMonitor
{
    private static object lockObject = new object();
    private static int readersCount = 0;
    private static int resourceValue = 0;
    private static Random random = new Random();

    public void Reader()
    {
        // More than one reader can read at a time
        // note that only two areas are protected by the lock, the rest is not
        // this is because we want to allow multiple readers to read at the same time

        Thread.Sleep(random.Next(100, 200)); // To simulate some real life non-linear work

        lock (lockObject) // Increment readers count atomically (readerCount is shared between all readers)
        {
            readersCount++;
            Console.WriteLine($"{DateTime.Now:ss.fffffff} Reader {Thread.CurrentThread.ManagedThreadId} acquired lock, about to read, incremented readersCount (resourceValue={resourceValue} ; readersCount={readersCount})");
        }
    
        Console.WriteLine($"{DateTime.Now:ss.fffffff} Reader {Thread.CurrentThread.ManagedThreadId} is reading the resource (resourceValue={resourceValue} ; readersCount={readersCount})");

        lock (lockObject) // Decrement readers count atomically (readerCount is shared between all readers)
        {
            readersCount--;
            Console.WriteLine($"{DateTime.Now:ss.fffffff} Reader {Thread.CurrentThread.ManagedThreadId} acquired lock, finished reading, decremented readersCount (resourceValue={resourceValue} ; readersCount={readersCount})"); 
            if (readersCount == 0)
            {
                Console.WriteLine($"{DateTime.Now:ss.fffffff} Reader {Thread.CurrentThread.ManagedThreadId} is the last reader, notifying writers");
                Monitor.PulseAll(lockObject); // Notify waiting writers
            }

        }

        Console.WriteLine($"{DateTime.Now:ss.fffffff} Reader {Thread.CurrentThread.ManagedThreadId} finished (resourceValue={resourceValue} ; readersCount={readersCount})");

    }

    
    public void Writer()
    {
        Thread.Sleep(random.Next(100, 200)); // To simulate some real life non-linear work
        
        // Ensure that only one writer can write to the shared resource at a time
        lock (lockObject) 
        {
            Console.WriteLine($"{DateTime.Now:ss.fffffff} Writer {Thread.CurrentThread.ManagedThreadId} acquired lock, entering its critical section (readersCount={readersCount})");

            // This loop checks if there are any readers currently accessing the resource. 
            // If there are, the writer should wait until the readers are done reading.
            while (readersCount > 0) 
            {
                Console.WriteLine($"{DateTime.Now:ss.fffffff} Writer {Thread.CurrentThread.ManagedThreadId}, released lock waiting for readers to finish reading (current readersCount={readersCount})");
                
                // Next, release lock and wait for a pulse :
                //   This effectively relinquishes control of the lock and allows other threads 
                //   to access it while the writer is waiting.
                //   This step is important to allow readers to access the resource in the meantime.
                Monitor.Wait(lockObject); 

            }

            resourceValue++; // Simulates a write operation on the resource
            Console.WriteLine($"{DateTime.Now:ss.fffffff} Writer {Thread.CurrentThread.ManagedThreadId} wrote the resource (resourceValue={resourceValue} ; readersCount={readersCount})");

        }

        Console.WriteLine($"{DateTime.Now:ss.fffffff} Writer {Thread.CurrentThread.ManagedThreadId} finished (readersCount={readersCount})");
    }

    public static void Main()
    {   
        MyReaderWriterMonitor monitor = new ReaderWriterExample();

        Thread[] readers = new Thread[8]; // 8 readers
        Thread[] writers = new Thread[2]; // 2 writers

        // Start readers
        for (int i = 0; i < readers.Length; i++)
        {
            readers[i] = new Thread(monitor.Reader);
            readers[i].Start();
        }

        // Start writers
        for (int i = 0; i < writers.Length; i++)
        {
            writers[i] = new Thread(monitor.Writer);
            writers[i].Start();
        }

        // Wait for readers and writers to finish before exiting main thread
        for (int i = 0; i < readers.Length; i++)
        {
            readers[i].Join();
        }

        for (int i = 0; i < writers.Length; i++)
        {
            writers[i].Join();
        }
    }
}

// Output example  (when THREAD_SAFE is defined)
/* 
12.5105834 Reader 9 acquired lock, about to read, incremented readersCount (resourceValue=0 ; readersCount=1)
12.5196354 Reader 9 is reading the resource (resourceValue=0 ; readersCount=1)
12.5196354 Writer 11 acquired lock, entering its critical section (readersCount=1)
12.5196354 Writer 11, released lock waiting for readers to finish reading (current readersCount=1) 
    here the writer relinquishes control of the lock and allows other threads to access it while the writer is waiting

12.5196354 Reader 10 acquired lock, about to read, incremented readersCount (resourceValue=0 ; readersCount=2)
    Reader 9 is still reading the resource, as reader 10 is about to read as well

12.5211419 Reader 10 is reading the resource (resourceValue=0 ; readersCount=2)
12.5211419 Reader 10 acquired lock, finished reading, decremented readersCount (resourceValue=0 ; readersCount=1)
12.5211419 Reader 10 finished (resourceValue=0 ; readersCount=1)
    For whatever reasons, reader 10 finished reading before reader 9

12.5211419 Reader 9 acquired lock, finished reading, decremented readersCount (resourceValue=0 ; readersCount=0)
12.5211419 Reader 9 is the last reader, notifying writers
    Now that reader 9 is the last reader, it notifies the writers that they can access the resource

12.5221519 Reader 9 finished (resourceValue=0 ; readersCount=0)
12.5221519 Writer 11 wrote the resource (resourceValue=1 ; readersCount=0)
    Indeed, the writer was notified that it could access the resource, and it wrote to it

12.5221519 Writer 11 finished (readersCount=0)
*/