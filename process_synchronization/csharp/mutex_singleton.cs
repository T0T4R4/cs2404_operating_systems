// To run this C# program from the windows commandline, first run the following :
//      SET PATH=%PATH%;"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn"
// the run the code by calling :
//      csc.exe mutex_singleton.cs
// then run the program :
//      mutex_singleton.exe

// This compile directive allows us to switch between the thread-safe and non-thread-safe version of the program.
//#define THREAD_SAFE       // <-- UNCOMMENT This to make this program thread-safe

using System;
using System.Threading;

/// <summary>
/// This class illustrates a thread-safe implementation of the Singleton pattern.
/// 
/// What is a Singleton ?
///     A singleton is a design pattern used in software engineering. It ensures that a class has only one instance and 
///     provides a global point of access to that instance. This is useful when you want to control the instantiation of 
///     a class and ensure that there's only one instance throughout the lifetime of the application. It's commonly used 
///     for scenarios where a single point of control or coordination is required, such as managing configuration settings, 
///     database connections, or resource pooling. 
///     
/// How do we make it thread-safe ?
///     As threads want to access the Singleton, they have to acquire a lock on the instanceMutex.
///     This ensures that only one thread can access the Singleton at a time.
///     But more importantly, it ensures that only one thread can create the Singleton.
///     
/// </summary>
public sealed class Singleton
{
    private static Singleton _instance;
    
    #if THREAD_SAFE
    private static Mutex instanceMutex = new Mutex();
    #endif
    
    private Singleton() { 
        this.CreatedBy = Thread.CurrentThread.ManagedThreadId;
        this.CreatedDate = DateTime.Now;
    }

    public DateTime? CreatedDate;

    public int CreatedBy { get; private set; }

    public static Singleton Instance
    {
        get
        {
            #if THREAD_SAFE
            instanceMutex.WaitOne();
            #endif 

            if (_instance == null)
            {
                _instance = new Singleton();
                
                Console.WriteLine($"{DateTime.Now:ss.fffffff} Thread {Thread.CurrentThread.ManagedThreadId} initialized the Singleton");
            } else {
                Console.WriteLine($"{DateTime.Now:ss.fffffff} Thread {Thread.CurrentThread.ManagedThreadId} : Singleton already created by {_instance.CreatedBy} on {_instance.CreatedDate:mm:ss.fffffff}");
            }

            #if THREAD_SAFE
            instanceMutex.ReleaseMutex();
            #endif

            return _instance;
        }
    }

//     // This version is NOT thread safe, why ?
//     public static Singleton Instance
//     {
//         get
//         {
//             if (_instance == null)
//             {
//                 instanceMutex.WaitOne();
//                 try
//                 {
//                     _instance = new Singleton();
//                      Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} initialized the Singleton");
//                 }
//                 finally
//                 {
//                     instanceMutex.ReleaseMutex();
//                 }
//             } else {
//                 Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} : Singleton already created");
//             }
//             return _instance;
//         }
//     }
// }
}

class Program
{
    static void Main()
    {
        Random random = new Random();
        Thread[] threads = new Thread[20];

        for (int i = 0; i < 20; i++)
        {
            // note: here we use a lambda expression to define the thread's entrypoint function
            //       This is an anonymous function which will be called when the thread starts and will initialize the Singleton
            threads[i] = new Thread(() =>
            {
                Console.WriteLine($"{DateTime.Now:ss.fffffff} Thread {Thread.CurrentThread.ManagedThreadId} is starting");
                Thread.Sleep(random.Next(100, 500)); // Simulate some work
                Singleton singleton = Singleton.Instance;
                // Do something with the singleton
            });
            threads[i].Start();
        }

        for (int i = 0; i < 20; i++)
        {
            threads[i].Join();
        }
    }
}

// Output example  (when THREAD_SAFE is defined)
/*
First, they are all starting in random order
56.3994513 Thread 3 is starting
56.4004504 Thread 11 is starting
56.3994513 Thread 4 is starting
56.4024505 Thread 18 is starting
56.4004504 Thread 8 is starting
56.4004504 Thread 9 is starting
56.4024505 Thread 19 is starting
56.4004504 Thread 7 is starting
56.4014533 Thread 15 is starting
56.4024505 Thread 20 is starting
56.4014533 Thread 13 is starting
56.4004504 Thread 6 is starting
56.4024505 Thread 21 is starting
56.4014533 Thread 16 is starting
56.4004504 Thread 10 is starting
56.3994513 Thread 5 is starting
56.4024505 Thread 22 is starting
56.4014533 Thread 17 is starting
56.4014533 Thread 12 is starting
56.4014533 Thread 14 is starting
// as it happens (life? universe? everything?), the 18th thread to start is the one which is allowed to access the Singleton first
// this can be cause by a lot of factors, including the fact that the thread scheduler is not deterministic
// it could also be caused by the fact that the thread scheduler is not fair, and that some threads are given priority over others
// we have studied this during the CPU scheduling lesson
56.5324150 Thread 17 initialized the Singleton
// from then it's simple, all the other threads are able to access the Singleton, and witness that it was already created
56.5334100 Thread 16 : Singleton already created by 17 on 22:56.5324150
56.5824946 Thread 18 : Singleton already created by 17 on 22:56.5324150
56.5864851 Thread 8 : Singleton already created by 17 on 22:56.5324150
56.6114846 Thread 4 : Singleton already created by 17 on 22:56.5324150
56.6651061 Thread 12 : Singleton already created by 17 on 22:56.5324150
56.6830979 Thread 20 : Singleton already created by 17 on 22:56.5324150
56.6860956 Thread 19 : Singleton already created by 17 on 22:56.5324150
56.7110951 Thread 15 : Singleton already created by 17 on 22:56.5324150
56.7151056 Thread 13 : Singleton already created by 17 on 22:56.5324150
56.7260957 Thread 5 : Singleton already created by 17 on 22:56.5324150
56.7291000 Thread 11 : Singleton already created by 17 on 22:56.5324150
56.7441042 Thread 9 : Singleton already created by 17 on 22:56.5324150
56.7884118 Thread 6 : Singleton already created by 17 on 22:56.5324150
56.8264112 Thread 7 : Singleton already created by 17 on 22:56.5324150
56.8728907 Thread 14 : Singleton already created by 17 on 22:56.5324150
*/