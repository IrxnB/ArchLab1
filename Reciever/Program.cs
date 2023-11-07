using Client.Controller;
using Client.Network;
using Reciever.View;
using System.Net;

namespace Reciever
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listener = new UdpListener(5556);
            listener.ListenAsync();


            var view = new ClientView();
            var controller = new CsvController(view);
            var addres = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            var sender = new UdpSender(controller, addres);

            sender.StartSending();
        }
    }
}