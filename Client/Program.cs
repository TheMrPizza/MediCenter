using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Client.HttpClients;
using Client.IO.ConsoleIO;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = CreateConfig();
            var client = new MediClient(config);
            var consoleIO = new ConsoleIO();
            var mediCenter = new MediCenter.MediCenter(client, consoleIO);
            mediCenter.Run();
        }

        public static IConfiguration CreateConfig()
        {
            var builder = new ConfigurationBuilder();
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            builder.SetBasePath(projectPath);
            builder.AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
