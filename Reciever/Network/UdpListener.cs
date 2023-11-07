
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
                if(response.Status == Status.NOT_OK)
                {
                    Console.WriteLine("Wrong request");
                }else response.strings.ForEach(s => { Console.WriteLine(s); });
            }
        }
        
    }
}
