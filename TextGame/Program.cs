using System.Text;
using TextGame.Map;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        List<List<Tile>> tiles = new List<List<Tile>>();
        Map map = new Map(tiles);

        //Příprava mapy
        map.GenerateMap();
        map.GenerateFloor();
        map.CreateRooms();
        map.CreatePathways();

        //Vygenerovani objektu v mape
        map.SpawnPlayer();

        map.DrawMap();


        bool gameIsRunning = true;

        while (gameIsRunning)
        {
            if (Console.KeyAvailable)
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