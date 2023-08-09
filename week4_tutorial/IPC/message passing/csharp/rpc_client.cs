using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class Program
{
    static void Main()
    {
        TcpChannel channel = new TcpChannel();
        ChannelServices.RegisterChannel(channel, false);

        MathService mathService = (MathService)Activator.GetObject(
            typeof(MathService), "tcp://server_ip:12345/MathService");

        int result = mathService.Add(5, 7);
        Console.WriteLine("Result from remote service: " + result);
    }
}
