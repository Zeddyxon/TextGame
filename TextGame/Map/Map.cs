using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TextGame.Object;

namespace TextGame.Map
{
    public class Map
    {
        private List<List<Tile>> map;
        
        public List<Room> rooms = new List<Room>(4);

        public Map(List<List<Tile>> map)
        {
            this.map = map;
        }
        //Vytvoří typy ROOM pro toto patro a vytvoří 4 instance ROOM
      
        //Přidělí každému Tile X, Y a zhodnotí je jako inaccesible
        public void GenerateMap()
        {
            List<List<Tile>> returnMap = new List<List<Tile>>();
            for (int y = 0; y < Constants.Constants.MAP_HEIGHT; y++)
            {
                List<Tile> row = new List<Tile>();
                for (int x = 0; x < Constants.Constants.MAP_WIDTH; x++)
                {
                    Tile tile = new Tile(x, y, Type.INACCESIBLE);
                    row.Add(tile);
                }
                returnMap.Add(row);
            }
            map = returnMap;
        }
        public void GenerateFloor()
        {
            //returns an array containing all the values of the RoomType, Except ENTRANCE and EXIT
            RoomType[] roomTypes = ((RoomType[])Enum.GetValues(typeof(RoomType)))
                .Where(rt => rt != RoomType.ENTRACNCE && rt != RoomType.EXIT) 
                .ToArray();
            List<Room> retunrRoomTypes = new List<Room>(4)
            {
                    new Room(RoomType.ENTRACNCE, 10, 10, 0, 0),
                    new Room(RoomType.EXIT, 12, 8, 10, 5),
                    new Room(RoomType.CLASSICROOM, 15, 20, 5, 15),
                    new Room(RoomType.PEACEFULLROOM, 8, 12, 20, 10)
            };
            Random random = new Random();
            Random random1 = new Random();
            int positionStart = random.Next(0,4);
            int positionEnd = random1.Next(0, 4);


            //Vytvoření roomTypes pro místnosti na podlaží
            while (positionStart == positionEnd)
            {
                positionEnd = random.Next(0, 4);
            }
            Console.WriteLine(positionStart);
            Console.WriteLine(positionEnd);

            for (int i = 0; i < roomTypes.Count(); i++) 
            {
                if(i == positionStart)
                {
                    retunrRoomTypes[i] = new Room(RoomType.ENTRACNCE, 0, 0, 0, 0);
                }
                if(i == positionEnd)
                {
                    retunrRoomTypes[i] = new Room(RoomType.EXIT, 0, 0, 0, 0);
                }
                if(i !=  positionStart && i != positionEnd)
                {
                    retunrRoomTypes[i] = new Room(roomTypes[random.Next(0, roomTypes.Length)], 0, 0, 0, 0);
                }
                
            }
            rooms = retunrRoomTypes;
        }
        public void CreateRooms()
        {
            Random rand = new Random();
            //Toto vše se bude generovat podmíněně dle umístění místonosti
            int randomX = rand.Next(10, 20);
            int randomY = rand.Next(10, 20);
            int positionOfDoorsX = rand.Next(randomX, randomX + Constants.Constants.MAP_WIDTH);
            int positionOfDoorsY = rand.Next(randomY, randomY + Constants.Constants.MAP_HEIGHT); 
            //
            List<List<Tile>> mapList = map;

            //Generování RANDOM pozice místnosti a její šířky
            //0 - NW, 1 - NE, 2 - SW, 3 - SE
            for (int positionOfSector = 0; positionOfSector < rooms.Count(); positionOfSector++)
            {
                int randomWidth = rand.Next(10, (Constants.Constants.MAP_WIDTH / 2) - 5);
                int randomHeight = rand.Next(10, (Constants.Constants.MAP_HEIGHT / 2) - 5);
                switch (positionOfSector)
                {
                    case 0:
                        randomX = rand.Next(1, ((Constants.Constants.MAP_WIDTH / 2) - 3) - randomWidth);
                        randomY = rand.Next(1, ((Constants.Constants.MAP_HEIGHT / 2) - 3) - randomHeight);

                        break;
                    case 1:
                        randomX = rand.Next((Constants.Constants.MAP_WIDTH / 2) + 3, (Constants.Constants.MAP_WIDTH - 1) - randomWidth);
                        randomY = rand.Next(1, ((Constants.Constants.MAP_HEIGHT / 2) - 3) - randomHeight);
                        break;
                    case 2:
                        randomX = rand.Next(1, ((Constants.Constants.MAP_WIDTH / 2) - 3) - randomWidth);
                        randomY = rand.Next((Constants.Constants.MAP_HEIGHT / 2) + 3, (Constants.Constants.MAP_HEIGHT - 1) - randomHeight);
                        break;
                    case 3:
                        randomX = rand.Next((Constants.Constants.MAP_WIDTH / 2) + 3, (Constants.Constants.MAP_WIDTH - 1) - randomWidth);
                        randomY = rand.Next((Constants.Constants.MAP_HEIGHT / 2) + 3, (Constants.Constants.MAP_HEIGHT - 1) - randomHeight);
                        break;
                    default:
                        continue;
                }

                rooms[positionOfSector].width = randomWidth;
                rooms[positionOfSector].height = randomHeight;
                rooms[positionOfSector].TopCornerX = randomX;
                rooms[positionOfSector].TopCornerY = randomY;
            }
            
            foreach (Room room in rooms)
            {
                //Souřadnice pro dveře NAPRAVO/LEVO
                int randX1, randX2;
                //Souřadnice pro dveře NAHOŘE/DOLE
                int randY1, randY2;

                //Generovani pozici dveri dle jejich sektoru
                //Až toto nebudou čtverce bude se toto muset celé přepsat a tahat si to z objektu (Possible all except corner)

                switch (rooms.IndexOf(room))
                {
                    case 0:
                    case 2:
                        randX1 = room.TopCornerX + room.width;
                        randY1 = rand.Next(room.TopCornerY + 1, room.TopCornerY + room.height - 1);

                        randX2 = rand.Next(room.TopCornerX + 1, room.TopCornerX + room.width - 1);
                        randY2 = (rooms.IndexOf(room) == 0) ? room.TopCornerY + room.height : room.TopCornerY;
                        break;

                    case 1:
                    case 3:
                        randX1 = room.TopCornerX
                            ;
                        randY1 = rand.Next(room.TopCornerY + 1, room.TopCornerY + room.height - 1);

                        randX2 = rand.Next(room.TopCornerX + 1, room.TopCornerX + room.width - 1);
                        randY2 = (rooms.IndexOf(room) == 1) ? room.TopCornerY + room.height : room.TopCornerY;
                        break;

                    default:
                        continue;
                }

                room.doorLeftRight = mapList[randY1][randX1];
                room.doorUpDown = mapList[randY2][randX2];
                mapList[randY1][randX1].type = Type.DOOR;
                mapList[randY2][randX2].type = Type.DOOR;
                
                switch (room.type)
                {
                    case RoomType.CLASSICROOM:
                        for (int y = 0; y < Constants.Constants.MAP_HEIGHT; y++)
                        {
                            List<Tile> tileFloorRow = new List<Tile>();
                            for (int x = 0; x < Constants.Constants.MAP_WIDTH; x++)
                            {
                                if (x >= room.TopCornerX && x <= room.TopCornerX + room.width &&
                                    y >= room.TopCornerY && y <= room.TopCornerY + room.height)
                                {
                                    if (mapList[y][x].type == Type.DOOR)
                                    {
                                        continue;
                                    }
                                    else if (y == room.TopCornerY || x == room.TopCornerX || 
                                       y == room.TopCornerY + room.height || x == room.TopCornerX + room.width)
                                    {
                                        mapList[y][x].type = Type.WALL;
                                    }
                                    else
                                    {
                                        mapList[y][x].type = Type.GROUND;
                                        mapList[y][x].objectInTile = new ObjectInTile(TypeOfObject.NONE);
                                        room.floorTiles.Add(mapList[y][x]);
                                        tileFloorRow.Add(mapList[y][x]);
                                    }
                                }
                            }
                            //Pozustatek toho kdyz jsem si to pole udelal dvourozmerne a mel jsem to v zavislosti na mape a ne na mistnosti
                            //room.floorTiles.Add(tileFloorRow);
                        }
                        break;
                    case RoomType.PEACEFULLROOM:
                        for (int y = 0; y < Constants.Constants.MAP_HEIGHT; y++)
                        {
                            List<Tile> tileFloorRow = new List<Tile>();
                            for (int x = 0; x < Constants.Constants.MAP_WIDTH; x++)
                            {
                                if (x >= room.TopCornerX && x <= room.TopCornerX + room.width &&
                                    y >= room.TopCornerY && y <= room.TopCornerY + room.height)
                                {
                                    if (mapList[y][x].type == Type.DOOR)
                                    {
                                        continue;
                                    }
                                    else if (y == room.TopCornerY || x == room.TopCornerX ||
                                       y == room.TopCornerY + room.height || x == room.TopCornerX + room.width)
                                    {
                                        mapList[y][x].type = Type.WALL;
                                    }
                                    else
                                    {
                                        mapList[y][x].type = Type.GROUND;
                                        mapList[y][x].objectInTile = new ObjectInTile(TypeOfObject.NONE);
                                        room.floorTiles.Add(mapList[y][x]);
                                        tileFloorRow.Add(mapList[y][x]);
                                    }
                                }
                            }
                        }
                        break;
                    case RoomType.ENTRACNCE:
                        for (int y = 0; y < Constants.Constants.MAP_HEIGHT; y++)
                        {
                            List<Tile> tileFloorRow = new List<Tile>();
                            for (int x = 0; x < Constants.Constants.MAP_WIDTH; x++)
                            {
                                if (x >= room.TopCornerX && x <= room.TopCornerX + room.width &&
                                    y >= room.TopCornerY && y <= room.TopCornerY + room.height)
                                {
                                    if (mapList[y][x].type == Type.DOOR)
                                    {
                                        continue;
                                    }
                                    else if (y == room.TopCornerY || x == room.TopCornerX ||
                                       y == room.TopCornerY + room.height || x == room.TopCornerX + room.width)
                                    {
                                        mapList[y][x].type = Type.WALL;
                                    }
                                    else
                                    {
                                        mapList[y][x].type = Type.GROUND;
                                        mapList[y][x].objectInTile = new ObjectInTile(TypeOfObject.NONE);
                                        room.floorTiles.Add(mapList[y][x]);
                                        tileFloorRow.Add(mapList[y][x]);
                                    }
                                }
                            }
                        }
                        room.GenerateEntrance(room);
                        break;
                    case RoomType.EXIT:
                        for (int y = 0; y < Constants.Constants.MAP_HEIGHT; y++)
                        {
                            List<Tile> tileFloorRow = new List<Tile>();
                            for (int x = 0; x < Constants.Constants.MAP_WIDTH; x++)
                            {
                                if (x >= room.TopCornerX && x <= room.TopCornerX + room.width &&
                                    y >= room.TopCornerY && y <= room.TopCornerY + room.height)
                                {
                                    if (mapList[y][x].type == Type.DOOR)
                                    {
                                        continue;
                                    }
                                    else if (y == room.TopCornerY || x == room.TopCornerX ||
                                       y == room.TopCornerY + room.height || x == room.TopCornerX + room.width)
                                    {
                                        mapList[y][x].type = Type.WALL;
                                    }
                                    else
                                    {
                                        mapList[y][x].type = Type.GROUND;
                                        mapList[y][x].objectInTile = new ObjectInTile(TypeOfObject.NONE);
                                        room.floorTiles.Add(mapList[y][x]);
                                        tileFloorRow.Add(mapList[y][x]);
                                    }

                                }
                            }
                        }
                        break;
                    case RoomType.TRADER:
                        for (int y = 0; y < Constants.Constants.MAP_HEIGHT; y++)
                        {
                            List<Tile> tileFloorRow = new List<Tile>();
                            for (int x = 0; x < Constants.Constants.MAP_WIDTH; x++)
                            {
                                if (x >= room.TopCornerX && x <= room.TopCornerX + room.width &&
                                    y >= room.TopCornerY && y <= room.TopCornerY + room.height)
                                {
                                    if (mapList[y][x].type == Type.DOOR)
                                    {
                                        continue;
                                    }
                                    else if (y == room.TopCornerY || x == room.TopCornerX ||
                                       y == room.TopCornerY + room.height || x == room.TopCornerX + room.width)
                                    {
                                        mapList[y][x].type = Type.WALL;
                                    }
                                    else
                                    {
                                        mapList[y][x].type = Type.GROUND;
                                        mapList[y][x].objectInTile = new ObjectInTile(TypeOfObject.NONE);
                                        room.floorTiles.Add(mapList[y][x]);
                                        tileFloorRow.Add(mapList[y][x]);
                                    }
                                }
                            }
                        }
                        break;
                    case RoomType.BOSSROOM:
                        for (int y = 0; y < Constants.Constants.MAP_HEIGHT; y++)
                        {
                            List<Tile> tileFloorRow = new List<Tile>();
                            for (int x = 0; x < Constants.Constants.MAP_WIDTH; x++)
                            {
                                if (x >= room.TopCornerX && x <= room.TopCornerX + room.width &&
                                    y >= room.TopCornerY && y <= room.TopCornerY + room.height)
                                {
                                    if (mapList[y][x].type == Type.DOOR)
                                    {
                                        continue;
                                    }
                                    else if (y == room.TopCornerY || x == room.TopCornerX ||
                                       y == room.TopCornerY + room.height || x == room.TopCornerX + room.width)
                                    {
                                        mapList[y][x].type = Type.WALL;
                                    }
                                    else
                                    {
                                        mapList[y][x].type = Type.GROUND;
                                        mapList[y][x].objectInTile = new ObjectInTile(TypeOfObject.NONE);
                                        room.floorTiles.Add(mapList[y][x]);
                                        tileFloorRow.Add(mapList[y][x]);
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        continue;
                }
            }
            this.map = mapList;
        }
        public void CreatePathways()
        {

        }
        public void GenerateObjectsInRooms(List<RoomType> roomsInThisFloor)
        {
            List<Tile> possibleObjects = new List<Tile>(); //Do tohoto listu se budou ukladat vsechny policka kam je mozne neco umistit a pak se nahodne vyberou
            
        
        }
        public void GeneratePlayer()
        {

        }
        public void DrawMap()
        {
            List<List<Tile>> tileMap = map;
            //Předělat tak, že se bude generovat pouze ~25 polí okolo hráče
            //Stálo by za to také vytvořit novou Class coloured string, aby jednotilivé chars měli rozdílné barvy

            foreach (Room room in rooms)
            {
                Console.WriteLine();
                Console.WriteLine(room.type + " " + room.width + " " + room.height + " " + room.TopCornerX + " " + room.TopCornerY);
            }

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

                            if (tile.objectInTile.typeOfObject == TypeOfObject.NONE)
                            {
                                stringBuilderRow.Append(" .");
                            }
                            else
                            {
                                switch (tile.objectInTile.typeOfObject)
                                {
                                    case TypeOfObject.ENTRANCE:
                                        stringBuilderRow.Append(" H");
                                        break;
                                    case TypeOfObject.EXIT:
                                        stringBuilderRow.Append(">>");
                                        break;
                                    case TypeOfObject.ITEM:

                                        break;
                                    default:
                                        continue;
                                }

                            }
                            break;
                        case Type.INACCESIBLE:
                            stringBuilderRow.Append("??");
                            break;
                        case Type.DOOR:
                            stringBuilderRow.Append("[]");
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
