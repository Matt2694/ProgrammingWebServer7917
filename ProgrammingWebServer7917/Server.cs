using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingWebServer7917
{
    public class Server
    {
        #region Exercise 1

        private TcpListener listener = null;
        private bool running = true;

        public Server()
        {
            listener = new TcpListener(IPAddress.Any, 80);
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            StreamReader sr = new StreamReader(client.GetStream());
            StreamWriter sw = new StreamWriter(client.GetStream());
            sw.AutoFlush = true;
            string output = "";
            DateTime dateNow;
            string message;
            string header;
            if (client.Connected)
            {
                while (running)
                {
                    string request = sr.ReadLine();
                    dateNow = DateTime.Now;
                    switch (request)
                    {

                        case "GET /date HTTP/1.1":
                            message = dateNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            header = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: " + message.Length;
                            output =  header + "\n\n" + message;
                            sw.WriteLine(output);
                            break;
                        case "GET /time HTTP/1.1":
                            message = dateNow.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
                            header = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: " + message.Length;
                            output = header + "\n\n" + message;
                            sw.WriteLine(output);
                            break;
                        default:
                            sw.WriteLine("404 Page Not Found.");
                            running = false;
                            break;
                    }
                }
            }
        }

        #endregion
    }
}
