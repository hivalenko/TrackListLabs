using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace TrackListServer
{
    public class HttpServer
    {
        
            private TcpListener Listener;

            public HttpServer(int port)
            {
                Listener = new TcpListener(IPAddress.Any, port);
                Listener.Start();

                while (Program.IsRunning)
                {
                    TcpClient client = Listener.AcceptTcpClient();
                    Thread thread = new Thread(ClientThread);
                    thread.Start(client);
                }
            }

            static void ClientThread(Object infoState)
            {
                var client = new Client((TcpClient)infoState);
            }

            ~HttpServer()
            {
                if (Listener != null)
                {
                    Listener.Stop();;
                }
            }
        
    }
}