
using ArchLab1.Controller;
using ArchLab1Lib;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client.Network
{
    internal class UdpListener
    {
        private UdpClient udpClient;
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        public UdpListener(int port) 
        {
            this.udpClient = new UdpClient(port);
        }

        
        public async void ListenAsync()
        {
            while (true)
            {
                var serverResponse = udpClient.ReceiveAsync();
                Response response;
                using (var stream = new MemoryStream((await serverResponse).Buffer))
                {
                    response = (Response)binaryFormatter.Deserialize(stream);

                }
                response.strings.ForEach(s => { Console.WriteLine(s); });
                Thread.Sleep(100);
            }
        }
        
    }
}
