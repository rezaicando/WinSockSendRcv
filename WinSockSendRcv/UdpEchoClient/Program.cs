using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WinSockSendRcv
{
    class UdpEchoClient
    {
        static void Main(string[] args)
        {
            if ((args.Length < 2) || (args.Length > 3))
            {
                throw new System.ArgumentException("Parameters: <Server> <Port> <Data>");
            }

            //Server name or IP Address
            string server = args[0];

            //Use port argument if entered,otherwise default to 22
            int servPort = (args.Length == 3) ? Int32.Parse(args[1]) : 22;

            //Convert input string to an array of bytes
            byte[] sendPacket = Encoding.ASCII.GetBytes(args[2]);

            //Create a udp client instance
            UdpClient client = new UdpClient();

            try
            {
                //Send the Echo String to the specified host and port
                client.Send(sendPacket, sendPacket.Length, server, servPort);

                Console.WriteLine("sent {0} bytes to the server ...", sendPacket.Length);

                IPEndPoint remoteIPEndpoint = new IPEndPoint(IPAddress.Any, 0);

                //attempt echo reply receive
                byte[] rcvPacket = client.Receive(ref remoteIPEndpoint);

                Console.WriteLine("Received {0} bytes from {1}: {2}", rcvPacket.Length, remoteIPEndpoint,
                    Encoding.ASCII.GetString(rcvPacket, 0, rcvPacket.Length));

            }
            catch (SocketException se)
            {
                Console.WriteLine(se.ErrorCode + ": " + se.Message);
            }
            //close the port 
            client.Close();
        }
    }
}
