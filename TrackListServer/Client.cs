using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;

namespace TrackListServer
{
    public class Client
    {
        private TcpClient TcpClient;
        private string Buffer = "<root>";

        public Client(TcpClient client)
        {
            TcpClient = client;
            InputServer.ClientQueue.Enqueue(this);

            string request = "";
            byte[] buffer = new byte[1024];
            int count;

            while ((count = client.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, count);

                if (request.IndexOf("\r\n\r\n") >= 0 || request.Length > Math.Pow(10, 6))
                {
                    break;
                }
            }

            request = request.Trim("\n\r".ToCharArray());

            string node = request.Split('\n').First().Split(' ')[1].Substring(1);
            InputServer.Queue.Enqueue(node);

            string xml = request.Split('\n').Last();
            
            Console.WriteLine(xml);
            
            XElement root = XElement.Load(new MemoryStream(Encoding.UTF8.GetBytes(xml)));

            foreach (XElement lineElement in root.Elements())
            {
                string line = lineElement.Value;
                InputServer.Queue.Enqueue(line.Trim("\n\r".ToCharArray()));
            }
        }

        public void Close()
        {
            Buffer += "</root>";
            string str = "HTTP/1.1 200 OK\nContent-type: text/plain\nContent-Length:" + Buffer.Length + "\n\n" + Buffer;
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            TcpClient.GetStream().Write(buffer, 0, buffer.Length);
            TcpClient.Close();
        }

        public void AddOutput(string str)
        {
            Buffer += "<line>" + str + "</line>";
        }
    }
}