using ArchLab1Lib;
using Client.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Client.Network
{
    internal class UdpSender
    {
        private CsvController controller;
        private UdpClient client;
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private IPEndPoint serverIp;

        public UdpSender(CsvController controller, IPEndPoint serverIp)
        {
            this.controller = controller;
            this.client = new UdpClient();
            this.serverIp = serverIp;
        }

        public async void StartSending()
        {
            bool isExit = false;
            bool shouldSendRequest;

            do
            {
                controller.ShowCommands();


                var command = controller.ReadCommand();
                if(command != null)
                {
                    var request = controller.ProcessCommand(command, out isExit, out shouldSendRequest);
                    if (shouldSendRequest)
                    {
                        byte[] requsetBytes;
                        using (var stream = new MemoryStream())
                        {
                            binaryFormatter.Serialize(stream, request);
                            requsetBytes = stream.ToArray();
                        }
                        await client.SendAsync(requsetBytes, requsetBytes.Length, serverIp);
                    }
                }
            } while (!isExit);
        }
    }
}
