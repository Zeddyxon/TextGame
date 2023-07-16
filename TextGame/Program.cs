using Microsoft.VisualBasic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using TextGame.Constants;
using TextGame.Map;

internal class Program
{
    private static void Main(string[] args)
    {
        List<List<Tile>> tiles = new List<List<Tile>>();
        Map map = new Map(tiles);
        map.GenerateMap();
        map.CreateRoomInSector(1);
        map.DrawMap();

        bool gameIsRunning = true;

        //Map gameMap = new Map(TextGame.Constants.Constants.MapHeight, TextGame.Constants.Constants.MapWidth);
        //gameMap.GenerateMap();

        while (gameIsRunning)
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);


                // Check which arrow key is pressed
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("Nahoru");
                        Console.ResetColor(); // Reset color to the default
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Write("Dolu");

                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Write("Doleva");

                        break;
                    case ConsoleKey.RightArrow:
                        Console.Write("Doprava");

                        break;
                    case ConsoleKey.Spacebar:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Pičo");
                        Console.ResetColor(); // Reset color to the default
                        break;
                }
            }
        }
    }
}