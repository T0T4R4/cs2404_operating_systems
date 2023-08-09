using System;
using System.IO.MemoryMappedFiles;
using System.Text;

class Program
{
    static void Main()
    {
        // Define the size of the shared memory segment
        int size = 1024;

        // Create a new memory-mapped file for the shared memory
        using (var mmf = MemoryMappedFile.CreateNew("SharedMemory", size))
        {
            // Create an accessor to the memory-mapped file
            using (var accessor = mmf.CreateViewAccessor())
            {
                // Data to be written to shared memory
                byte[] buffer = Encoding.ASCII.GetBytes("Hello from C#!");

                // Write the data to the shared memory
                accessor.WriteArray(0, buffer, 0, buffer.Length);

                Console.WriteLine("Data written to shared memory");
            }
        }
    }
}
