using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Define the name for the named pipe
        string pipeName = "MyNamedPipe";

        // Create a named pipe server stream
        using (NamedPipeServerStream serverPipe = new NamedPipeServerStream(pipeName, PipeDirection.InOut))
        {
            Console.WriteLine("Waiting for client connection...");
            
            // Wait asynchronously for a client connection
            await serverPipe.WaitForConnectionAsync();

            Console.WriteLine("Client connected.");

            // Read message from the client
            byte[] buffer = new byte[1024];
            int bytesRead = await serverPipe.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received message: {message}");

            // Process the received message (e.g., perform some action based on the message)

            // Prepare a response to send back to the client
            string response = "Message received and processed.";
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);

            // Send the response to the client
            await serverPipe.WriteAsync(responseBytes, 0, responseBytes.Length);

            Console.WriteLine("Response sent.");
        }
    }
}
