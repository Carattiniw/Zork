using System;
using System.Data.Common;

namespace Zork
{
    class Program
    {
        private static string[] Rooms = 
        {
            "Forest", "West of House", "Behind House", "Clearing", "Canyon View"
        };

        private static string CurrentRoom
        {
            get
            {
                return Rooms[Location.Column];
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

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
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
                        outputString = $"You moved {command}.";
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
            bool moveSuccessful = true;

            switch (command)
            {
                case Commands.NORTH:
                case Commands.SOUTH:
                    moveSuccessful = false;
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

        private static (int Column, int Row) Location = (1,0);
    }
}
