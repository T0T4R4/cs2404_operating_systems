using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        IPAddress serverIP = IPAddress.Parse("server_ip");
        int serverPort = 12345;

        TcpListener server = new TcpListener(serverIP, serverPort);
        server.Start();

        Console.WriteLine("Server listening on {0}:{1}", serverIP, serverPort);

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected to: {0}", ((IPEndPoint)client.Client.RemoteEndPoint).Address);

        byte[] buffer = new byte[1024];
        int bytesRead = client.Client.Receive(buffer);
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine("Received: {0}", dataReceived);

        client.Close();
        server.Stop();
    }
}
