// To run this C# program from the windows commandline, first run the following :
//      SET PATH=%PATH%;"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn"
// the run the code by calling :
//      csc.exe semaphore_elevator.cs
// then run the program :
//      semaphore_elevator.exe

// This compile directive allows us to switch between the thread-safe and non-thread-safe version of the program.
//#define THREAD_SAFE       // <-- UNCOMMENT This to make this program thread-safe

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// This program illustrates a thread-safe implementation of an elevator.
/// A semaphore is used to limit the number of people in the elevator to 3.
/// Each person is represented in the program by a Thread. 
/// 
/// </summary>
class Program
{
    #if THREAD_SAFE
    static SemaphoreSlim elevatorSemaphore = new SemaphoreSlim(3); // Maximum capacity of 2 people
    #endif

    static void Main(string[] args)
    {
        StartThreads();
    }

    static void StartThreads()
    {
        Thread[] persons = new Thread[7];

        for (int i = 0; i < 7; i++)
        {
            persons[i] = new Thread(UseElevator);
            persons[i].Start();
        }

        for (int i = 0; i < 7; i++)
        {
            persons[i].Join();
        }
    }

    static void UseElevator()
    {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"{DateTime.Now:ss.fffffff} Person {threadId} is waiting for the elevator.");

        #if THREAD_SAFE
        elevatorSemaphore.Wait(); // Try to enter the elevator (acquire semaphore)
        #endif

        Console.WriteLine($"{DateTime.Now:ss.fffffff} Person {threadId} is in the elevator.");
        Thread.Sleep(TimeSpan.FromSeconds(3)); // Simulate elevator ride

        #if THREAD_SAFE
        elevatorSemaphore.Release(); // Exit the elevator (release semaphore)
        #endif
        Console.WriteLine($"{DateTime.Now:ss.fffffff} Person {threadId} has left the elevator.");

    }
}

// Output example  (when THREAD_SAFE is defined)
/*

35.9205647 Person 9 is waiting for the elevator.
35.9205647 Person 5 is waiting for the elevator.
35.9295520 Person 9 is in the elevator.
35.9205647 Person 7 is waiting for the elevator.
35.9295520 Person 7 is in the elevator.
35.9295520 Person 5 is in the elevator.
// from here, the elevator is full and we have to wait for someone to leave before we can enter

35.9205647 Person 4 is waiting for the elevator.
35.9205647 Person 6 is waiting for the elevator.
35.9205647 Person 8 is waiting for the elevator.
35.9205647 Person 3 is waiting for the elevator.
38.9307711 Person 5 has left the elevator.
// at least ! now someone can get in ...
38.9307711 Person 4 is in the elevator.
38.9307711 Person 9 has left the elevator.
38.9307711 Person 3 is in the elevator.
38.9307711 Person 8 is in the elevator.
// elevator is full again !
38.9307711 Person 7 has left the elevator.
41.9319287 Person 6 is in the elevator.
41.9319287 Person 3 has left the elevator.
41.9319287 Person 4 has left the elevator.
41.9329225 Person 8 has left the elevator.
44.9321708 Person 6 has left the elevator.

*/