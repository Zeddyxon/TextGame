using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Map
{
    public class Map
    {
        private List<List<Tile>> map;
        private int width = Constants.Constants.MapWidth;
        private int height = Constants.Constants.MapHeight;

        public Map(List<List<Tile>> map)
        {
            this.map = map;
        }
        public void GenerateMap()
        {
            List<List<Tile>> returnMap = new List<List<Tile>>();
            for (int y = 0; y < height; y++)
            {
                List<Tile> row = new List<Tile>();
                for (int x = 0; x < width; x++)
                {
                    Tile tile = new Tile(x, y, Type.INACCESIBLE);
                    row.Add(tile);
                }
                returnMap.Add(row);

                Console.Write("one row");
            }
            map = returnMap;
        }


        public void CreateRoomInSector(int positionOfSector)
        {
            Random rand = new Random();
            int randomWidth = rand.Next(10, (width/2) - 5);
            int randomHeight = rand.Next(10, (height/2) - 5);
            //Toto vše se bude generovat podmíněně dle umístění místonosti
            int randomX = rand.Next(10, 20);
            int randomY = rand.Next(10, 20);
            int positionOfDoorsX = rand.Next(randomX, randomX + width);
            int positionOfDoorsY = rand.Next(randomY, randomY + height); 
            //
            List<List<Tile>> returnSector = map;
            
            //0 - NW, 1 - NE, 2 - SW, 3 - SE
            switch (positionOfSector)
            {
                case 0:
                    randomX = rand.Next(1, ((width/2) - 3) - randomWidth);
                    randomY = rand.Next(1, ((height / 2) - 3) - randomHeight);
                    break;
                case 1:
                    randomX = rand.Next((width / 2) + 3, (width - 1) - randomWidth);
                    randomY = rand.Next(1, ((height / 2) - 3) - randomHeight);
                    break;
                case 2:
                    randomX = rand.Next(1, ((width / 2) - 3) - randomWidth);
                    randomY = rand.Next((height / 2) + 3, (height - 1) - randomHeight);
                    break;
                case 3:
                    randomX = rand.Next((width / 2) + 3, (width - 1) - randomWidth);
                    randomY = rand.Next((height / 2) + 3, (height - 1) - randomHeight);
                    break;
                default:

                    break;
            }
            Room room = new Room(RoomType.CLASSICROOM, randomWidth, randomHeight, randomX, randomY);
            Console.WriteLine("START");
            switch (room.type)
            { 
                case RoomType.CLASSICROOM:
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (x >= room.TopCornerX && x <= room.TopCornerX + room.width &&
                                y >= room.TopCornerY && y <= room.TopCornerY + room.height)
                            {
                                if (y == room.TopCornerX || x == room.TopCornerY ||
                                   y == room.TopCornerX + room.width || x == room.TopCornerY + room.height)
                                {
                                    Console.WriteLine("WALL");

                                    returnSector[y][x].type = Type.WALL;
                                }
                                else
                                {
                                    Console.WriteLine("GROUND");

                                    returnSector[y][x].type = Type.GROUND;

                                }
                            }
                            //if(x == positionOfDoorsX && y == positionOfDoorsY)
                            else
                            {
                                //Console.WriteLine("INACCESIBLE");

                                returnSector[y][x].type = Type.INACCESIBLE;
                            }

                        }
                        //TEST
                        Console.WriteLine();
                        Console.WriteLine("_________________________________");
                        //TEST
                    }
                    this.map = returnSector;
                    break;
                case RoomType.ENTRACNCE:

                    break;
                default:
                    for (int y = 0; y < map.Count; y++)
                    {
                        Console.WriteLine("DEFAULT");

                        List<Tile> row = map[y]; // Get the inner list (row) at index y
                        for (int x = 0; x < row.Count; x++)
                        {
                            if (x >= room.TopCornerX && x <= room.TopCornerX + room.width &&
                                y >= room.TopCornerY && y <= room.TopCornerY + room.height)
                            {
                                if (y == room.TopCornerX || x == room.TopCornerY ||
                                   y == room.TopCornerX + room.width || x == room.TopCornerY + room.height)
                                {
                                    returnSector[y][x].type = Type.WALL;
                                }
                                returnSector[y][x].type = Type.GROUND;
                            }
                            else
                            {
                                returnSector[y][x].type = Type.INACCESIBLE;
                            }
                        }
                    }
                    this.map = returnSector;
                    break;
            }

        }

        public void GeneratePlayer()
        {

        }


        public void DrawMap()
        {
            List<List<Tile>> tileMap = map;
            //Předělat tak, že se bude generovat pouze ~25 polí okolo hráče
            //Stálo by za to také vytvořit novou Class coloured string, aby jednotilivé chars měli rozdílné barvy
            StringBuilder stringBuilderRow = new StringBuilder();
            foreach(List<Tile> row in tileMap) 
            {
                foreach (Tile tile in row)
                {
                    switch (tile.type)
                    {
                        case Type.WALL:
                            stringBuilderRow.Append("▓▓");
                            break;
                        case Type.GROUND:
                            stringBuilderRow.Append(" .");
                            break;
                        case Type.INACCESIBLE:
                            stringBuilderRow.Append(" ."); //Dle všeho se tento znak neukáže v konzoli ░░
                            break;
                        case Type.DOOR:
                            stringBuilderRow.Append("║║");
                            break;
                        default:
                            stringBuilderRow.Append("  ");
                            break;
                    }
                }
                Console.WriteLine(stringBuilderRow.ToString());
                stringBuilderRow.Clear();
            }
        }
    }
}
