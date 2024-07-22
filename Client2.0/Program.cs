using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Client
{
    static async Task Main()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        IPEndPoint ep = new IPEndPoint(ip, 8888);
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            await clientSocket.ConnectAsync(ep);
            IPEndPoint serverEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
            string serverIP = serverEndPoint.Address.ToString();

            string clientMessage = "Привіт, сервере!";
            await clientSocket.SendAsync(Encoding.ASCII.GetBytes(clientMessage), SocketFlags.None);

            byte[] buffer = new byte[1024];
            int receivedLength = await clientSocket.ReceiveAsync(buffer, SocketFlags.None);
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
