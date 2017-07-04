using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenRconCORE
{
    class Program
    {
        static GenRCON grcon = new GenRCON();
        static bool connected = false;
        static bool waiting = true;

        internal static void Main(string[] args)
        {
            Initialize(args);

            var task = Task.Run(async () =>
            {
                while (waiting)
                {
                    Thread.Sleep(100);
                }

                while (connected)
                {
                    Console.Write("GenRCON: ");

                    string message = Console.ReadLine();

                    if (message == "genrcon disconnect")
                    {
                        //UseInteractiveMode();
                    }
                    else if (message == "genrcon quit")
                    {
                        break;
                    }
                    else
                    { 
                        string reply = await grcon.Send(message);
                        Console.WriteLine(Environment.NewLine + reply + Environment.NewLine);
                    }
                }

            });

            task.GetAwaiter().GetResult();

            Console.Write(Environment.NewLine + "End of Session, Press any key to continue...");
            ConsoleKeyInfo tempvar = Console.ReadKey();
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

        static async void UseCommandLineArguments(string[] args)
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
            connected = await grcon.Connect(args[address], int.Parse(args[port]), args[password]);

            if (connected)
            {
                Console.WriteLine("Connected to " + args[address] + ":" + args[port]);
            }
            else
            {
                Console.WriteLine("Cannot reach target address!" + Environment.NewLine + "Check your internet connection and/or the target address!");
            }
            waiting = false;
        }

        static async void UseInteractiveMode()
        {
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter Port: ");
            int port = int.Parse(Console.ReadLine());
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            connected = await grcon.Connect(address, port, password);

            if (connected)
            {
                Console.WriteLine("Connected to " + address + ":" + port);
            }
            else
            {
                Console.WriteLine("Cannot reach target address!" + Environment.NewLine + "Check your internet connection and/or the target address!");
            }

            waiting = false;
        }
    }
}