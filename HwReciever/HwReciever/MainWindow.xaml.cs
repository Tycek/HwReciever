using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HwReciever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static TcpListener serverSocket;

        public MainWindow()
        {
            InitializeComponent();
            StartServer();
            
        }

        public void StartServer()
        {

            IPHostEntry ipHostEntry = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostEntry.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8889);
            serverSocket = new TcpListener(ipEndPoint);
            serverSocket.Start();
            Console.WriteLine("Asynchonous server socket is listening at: " + ipEndPoint.Address.ToString());
            WaitForClients();
        }

        private void WaitForClients()
        {
            serverSocket.BeginAcceptTcpClient(new System.AsyncCallback(OnClientConnected), null);
        }

        private void OnClientConnected(IAsyncResult asyncResult)
        {
            try
            {
                TcpClient clientSocket = serverSocket.EndAcceptTcpClient(asyncResult);
                if (clientSocket != null)
                    Console.WriteLine("Received connection request from: " + clientSocket.Client.RemoteEndPoint.ToString());
                HandleClientRequest(clientSocket);
            }
            catch
            {
                throw;
            }
            WaitForClients();
        }

        private void HandleClientRequest(TcpClient clientSocket)
        {
            string result = string.Empty;
            byte[] data = new byte[256];
            var stream = clientSocket.GetStream();
            do
            {
                int bytes = stream.Read(data, 0, data.Length);
                string responseData = Encoding.ASCII.GetString(data, 0, bytes);
                result += responseData;
            }
            while (!result.EndsWith('\0'));

            this.Dispatcher.Invoke(() =>
            {
                TextBox1.Text = result;
            });
            
        }

    }
}
