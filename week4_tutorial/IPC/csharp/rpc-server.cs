using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

public class MathService : MarshalByRefObject
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

class Program
{
    static void Main()
    {
        TcpChannel channel = new TcpChannel(12345); // listening on local port
        ChannelServices.RegisterChannel(channel, false);

        RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(MathService), "MathService",
            WellKnownObjectMode.Singleton);

        Console.WriteLine("Server running. Press Enter to exit.");
        Console.ReadLine();
    }
}
