using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString = "";
                switch (command)
                {
                    case Commands.LOOK:
                        Console.WriteLine($"A rubber mat saying 'Welcome to Zork!' lies by the door.");
                        break;

                    case Commands.EAST:
                        Console.WriteLine($"You moved {command}");
                        break;

                    case Commands.NORTH:
                        Console.WriteLine($"You moved {command}");
                        break;

                    case Commands.SOUTH:
                        Console.WriteLine($"You moved {command}");
                        break;

                    case Commands.WEST:
                        Console.WriteLine($"You moved {command}");
                        break;

                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }

                Console.Write(outputString);
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}