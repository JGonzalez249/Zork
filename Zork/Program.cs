using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        private static string CurrentRoom 
        {

            get
            {
                return Rooms[LocationColumn]; //Gives current location of the player
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)  //Game Loop
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.EAST:
                    case Commands.WEST:
                    case Commands.NORTH:
                    case Commands.SOUTH:
                        outputString = $"You moved {command}";
                        if(Move(command) == false)
                        {
                            
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static bool Move(Commands command)
        {

            bool isValidMove = true;
            switch (command)
            {
                case Commands.EAST when LocationColumn < Rooms.GetLength(0) - 1:
                    LocationColumn++;
                    break;

                case Commands.WEST when LocationColumn > 0:
                    LocationColumn--;
                    break;


                default:
                    isValidMove = false;
                    break;
            }
            return isValidMove;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static string[] Rooms = { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" }; //Zork Map

        private static int LocationColumn = 1;

    }


}