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
            string output = "";
            DateTime dateNow;
            string returnRequest;
            if (client.Connected)
            {
                while (running)
                {
                    string request = sr.ReadLine();
                    dateNow = DateTime.Now;
                    switch (request)
                    {
                        case "GET /date HTTP/1.1":
                            returnRequest = dateNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            output = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: " + returnRequest.Length + "\n\n" + returnRequest;
                            sw.WriteLine(output);
                            sw.Flush();
                            break;
                        case "GET /time HTTP/1.1":
                            returnRequest = dateNow.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
                            output = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: " + returnRequest.Length + "\n\n" + returnRequest;
                            sw.WriteLine(output);
                            sw.Flush();
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
