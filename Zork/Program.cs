using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zork
{
    class Program
    {

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }
        public static Room CurrentRoom 
        {

            get
            {
                return Rooms[Location.Row, Location.Column]; //Gives current location of the player
            }
        }
     
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            const string defaultRoomsFilename = "Rooms.json";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);
            InitializeRoomDescriptions(roomsFilename);

            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)  //Game Loop
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.LOOK:
                       Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.EAST:
                    case Commands.WEST:
                    case Commands.NORTH:
                    case Commands.SOUTH:

                        if (Move(command) == false)
                        {
                            
                            Console.WriteLine("The way is shut!");
                        }

                        if (previousRoom != CurrentRoom)
                        {
                            Console.WriteLine(CurrentRoom.Description);
                            previousRoom = CurrentRoom;
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
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

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

        private static bool IsDirection(Commands command) => Directions.Contains(command);

        //Zork Map

        private static Room[,] Rooms = {    
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View")},
            { new Room("Forest"), new Room("West of House"), new Room("Behind House")},
            { new Room("Dense Woods"), new Room("North of House"),new Room("Clearing")},
        
        };


        private static void InitializeRoomDescriptions(string roomsFilename) =>
            Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));
        

        private enum Fields
        {
            Name = 0,
            Description
        }

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST,
            Commands.LOOK
        };

        private static (int Row, int Column) Location = (1, 1); //Player Spawns West of House on array

    }
}