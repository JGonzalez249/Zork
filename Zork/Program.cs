﻿using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        private static string CurrentRoom 
        {

            get
            {
                return Rooms[Location.Row, Location.Column]; //Gives current location of the player
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

                switch (command)
                {
                    case Commands.LOOK:
                       Console.WriteLine( "This is an open field west of a white house, with a boarded front door.");
                        break;

                    case Commands.EAST:
                    case Commands.WEST:
                    case Commands.NORTH:
                    case Commands.SOUTH:

                        if(Move(command) == false)
                        {
                            
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }

            }
        }

        private static bool Move(Commands command)
        {

            bool isValidMove = true;
            switch (command)
            {
                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    break;

                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    break;

                case Commands.SOUTH when Location.Row > 0:
                    Location.Row--;
                    break;

                default:
                    isValidMove = false;
                    break;
            }
            return isValidMove;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static readonly string[,] Rooms = {     //Zork Map
            { "Rocky Trail", "South of House", "Canyon View"},
            { "Forest", "West of House", "Behind House"},
            { "Dense Woods", "North of House","Clearing"},
        
        };

        private static (int Row, int Column) Location = (1, 1); //Player Spawns West of House

    }


}