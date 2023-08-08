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

        TcpClient client = new TcpClient();
        client.Connect(serverIP, serverPort);

        NetworkStream stream = client.GetStream();
        string message = "{ \"Employees\": { \"count\": 0}}"; //"<xml><employees count=\"1\"/></xml>";
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);

        client.Close();
    }
}
