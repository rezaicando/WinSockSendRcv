using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WinSockSendRcv
{
    class UdpEchoServer
    {
        static void Main(string[] args)
        {
            //Test for correct number of args
            if (args.Length > 1)
            {
                throw new ArgumentException("Parameters: <Port>");
            }

            int servPort = (args.Length == 1) ? Int32.Parse(args[0]) : 22;
            UdpClient client = null;
            try
            {
                //Create an instance of UdpClient to listen on Port
                client = new UdpClient(servPort);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.ErrorCode + ": " + se.Message);
                Environment.Exit(se.ErrorCode);
            }

            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //Run receive echo 
            for (; ; )
            {
                try
                {
                    //Receive a byte array with echo datagram packet content
                    byte[] byteBuffer = client.Receive(ref remoteIPEndPoint);
                    Console.Write("Client at " + remoteIPEndPoint + " - ");

                    //Send echo packet back to client
                    client.Send(byteBuffer, byteBuffer.Length, remoteIPEndPoint);
                    Console.WriteLine("echoed {0} bytes.", byteBuffer.Length);
                }
                catch (SocketException se)
                {
                    Console.WriteLine(se.ErrorCode + ": " + se.Message);
                }
            }
        }
    }
}
