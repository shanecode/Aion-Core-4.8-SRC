using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ASM31
{

    static public class NetUtils
    {
        static public bool isConnected(string host, int port)
        {
            if (host == null || port == 0)
                return false;
            try
            {
                TcpClient hostToCheck = new TcpClient(host, port);
                if (hostToCheck.Connected)
                    hostToCheck.Close();        // disconnect
                return true;
            }
            catch (SocketException)
            {
                return false;       // not connected
            }
        }
    }
}
