using System.Text;
using TextGame.Constants;
using TextGame.Map;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        List<List<Tile>> tiles = new List<List<Tile>>();
        Map map = new Map(tiles);
        GameStats gameStats = new GameStats(0, 0, 0);


        //Příprava mapy
        map.GenerateMap();
        map.GenerateFloor();
        map.CreateRooms();
        map.CreatePathways();

        map.GenerateObjectsInRooms(map.rooms);


        //Vygenerovani objektu v mape
        map.SpawnPlayer();

        map.DrawMap();

        bool gameIsRunning = true;

        while (gameIsRunning)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            Console.WriteLine(Map.player.speed);
            for (int i = Map.player.speed; i > 0; i--)
            {
                // Check which arrow key is pressed
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("Nahoru");
                        Console.ResetColor(); // Reset color to the default

                        map.PlayerMovement("UP");

                        break;
                    case ConsoleKey.DownArrow:
                        map.PlayerMovement("DOWN");


                        Console.Write("Dolu");

                        break;
                    case ConsoleKey.LeftArrow:

                        map.PlayerMovement("LEFT");

                        Console.Write("Doleva");

                        break;
                    case ConsoleKey.RightArrow:

                        map.PlayerMovement("RIGHT");
                        Console.Write("Doprava");

                        break;
                    case ConsoleKey.Spacebar:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Pičo");
                        Console.ResetColor(); // Reset color to the default
                        break;

                    case ConsoleKey.E:
                        Console.WriteLine("Try to use");
                        map.CheckForUsable();
                        break;
                }
            }

            gameStats.gameTurn++;
            Console.WriteLine();
            Console.WriteLine(gameStats.gameTurn);
        }
    }
}