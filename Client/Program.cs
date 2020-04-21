using System;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.ConsoleIO;
using Client.MediCenter;

namespace Client
{
    class Program
    {
        static async void Main(string[] args)
        {
            var client = new MediClient();
            var consoleIO = new ConsoleIO();
            var mediCenter = new MediCenter.MediCenter(client, consoleIO);
            mediCenter.Run();
        }
    }
}
