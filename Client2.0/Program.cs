using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        IPEndPoint ep = new IPEndPoint(ip, 8888);
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            clientSocket.Connect(ep);
            IPEndPoint serverEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
            string serverIP = serverEndPoint.Address.ToString();

            string clientMessage = "Привіт, сервере!";
            clientSocket.Send(Encoding.ASCII.GetBytes(clientMessage));

            byte[] buffer = new byte[1024];
            int receivedLength = clientSocket.Receive(buffer);
            string serverResponse = Encoding.ASCII.GetString(buffer, 0, receivedLength);

            Console.WriteLine($"о {DateTime.Now.ToString("HH:mm")} від [{serverIP}] отримано рядок: {serverResponse}");

            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        catch (SocketException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
