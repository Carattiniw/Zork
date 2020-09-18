using System;
using System.Data.Common;

namespace Zork
{
    class Program
    {
        private static string[,] Rooms = 
        {
            { "Rocky Trail", "South of House", "Canyon View" },
            { "Forest", "West of House", "Behind House" },
            { "Dense Woods", "North of House", "Clearing" }
        };

        private static string CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                //string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        //outputString = "Thank you for playing!";
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        //outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        Console.WriteLine("A rubber mat saying 'Welcome to Zork!' lies by the door.");
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                            continue;
                        }
                        //outputString = $"You moved {command}.";

                        //Tried removing the above line to match Zork 2.2 output,
                        //but it broke the rest of the outputStrings for some reason.
                        //Replaced all outputString with Console.Writelines.
                        break;

                    default:
                        //outputString = "Unknown command.";
                        Console.WriteLine("Unknown command.");
                        break;
                }

                //Console.WriteLine(outputString);
            }
        }

        private static bool Move(Commands command)
        {
            bool moveSuccessful = true;

            switch (command)
            {
                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    break;
                case Commands.SOUTH when Location.Row > 0:
                    Location.Row--;
                    break;
                case Commands.EAST when Location.Column < Rooms.GetLength(0) - 1:
                    Location.Column++;
                    break;
                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    moveSuccessful = false;
                    break;
            }

            return moveSuccessful;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static (int Row, int Column) Location = (1,1); //sets the current location to West of House and feeds it to CurrentRoom
    }
}
