using System;
using System.Net;
using CoreRCON;
using CoreRCON.Parsers.Standard;
using System.Threading.Tasks;

namespace GenRconCORE
{
    class Program
    {
        static GenRCON grcon = new GenRCON();

        internal static void Main(string[] args)
        {
            Initialize(args);
            var task = Task.Run(async () =>
            {
                while (true)
                {
                    Console.Write("GenRCON: ");
                   string message = Console.ReadLine();
                    if (message == "genrcon quit")
                    {
                        break;
                    }
                    string reply = await grcon.Send(message);
                    Console.WriteLine(Environment.NewLine + reply + Environment.NewLine);
                }
            });

            task.GetAwaiter().GetResult();
        }
        
        static void Initialize(string[] args)
        {
            Console.WriteLine("GenRCON .NET CORE edition V1.0" + Environment.NewLine + "Made by Berk (SAS41) A." + Environment.NewLine);

            if (args.Length == 6)
            {
                UseCommandLineArguments(args);
            }
            else
            {
                UseInteractiveMode();
            }
        }

        static void UseCommandLineArguments(string[] args)
        {

            int address = 0, port = 0, password = 0;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case ("-address"): address = i + 1; break;
                    case ("-a"): address = i + 1; break;
                    case ("-port"): port = i + 1; break;
                    case ("-p"): port = i + 1; break;
                    case ("-password"): password = i + 1; break;
                    case ("-pw"): password = i + 1; break;
                }
            }

            grcon.ResolveHostname(args[address], int.Parse(args[port]), args[password]);

            Console.WriteLine("Connected to "+ args[address] + ":" + args[port]);
        }

        static void UseInteractiveMode()
        {
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter Port: ");
            int port = int.Parse(Console.ReadLine());
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            grcon.ResolveHostname(address, port, password);

            Console.WriteLine("Connected to " + address + ":" + port);
        }
    }
}