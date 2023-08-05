using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TextGame.Characters;
using TextGame.Object;

namespace TextGame.Map
{
    public class Map
    {
        private List<List<Tile>> map;

        public List<Room> rooms = new List<Room>(4);
        public static Player player = new Player(0, 0, 5, 5, 3, 1, 1, 1);
        //public static List<Object.ObjectInTile> = new List<Object.ObjectInTile>;

        public Map(List<List<Tile>> map)
        {
            this.map = map;
        }
        //Vytvoří typy ROOM pro toto patro a vytvoří 4 instance ROOM

        //Generovani mistnosti
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
            int positionStart = random.Next(0, 4);
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
                if (i == positionStart)
                {
                    retunrRoomTypes[i] = new Room(RoomType.ENTRACNCE, 0, 0, 0, 0);
                }
                if (i == positionEnd)
                {
                    retunrRoomTypes[i] = new Room(RoomType.EXIT, 0, 0, 0, 0);
                }
                if (i != positionStart && i != positionEnd)
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
            int randomX;// = rand.Next(10, 20);
            int randomY;// = rand.Next(10, 20);
            //int positionOfDoorsX = rand.Next(randomX, randomX + Constants.Constants.MAP_WIDTH);
            //int positionOfDoorsY = rand.Next(randomY, randomY + Constants.Constants.MAP_HEIGHT); 
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
                        randX1 = room.TopCornerX;
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
                        room.GenerateExit(room);
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
            List<List<Tile>> mapList = map;
            //vygenerovani cestiček ke středu mapy
            for (int i = 0; i < rooms.Count; i++)
            {
                Room room = rooms[i];
                int staticDoorX;
                int staticDoorY;
                int untilLeftRight = Constants.Constants.MAP_WIDTH / 2;
                int untilUpDown = Constants.Constants.MAP_HEIGHT / 2;

                switch (i)
                {
                    case 0:
                        staticDoorX = room.doorUpDown.x;
                        staticDoorY = room.doorLeftRight.y;

                        for (int x = room.doorLeftRight.x + 1; x < untilLeftRight; x++)
                        {
                            mapList[staticDoorY + 1][x].type = Type.WALL;
                            mapList[staticDoorY][x].type = Type.GROUND;
                            mapList[staticDoorY - 1][x].type = Type.WALL;
                        }
                        for (int y = room.doorUpDown.y + 1; y < untilUpDown; y++)
                        {
                            mapList[y][staticDoorX - 1].type = Type.WALL;
                            mapList[y][staticDoorX].type = Type.GROUND;
                            mapList[y][staticDoorX + 1].type = Type.WALL;
                        }
                        break;

                    case 1:
                        staticDoorX = room.doorUpDown.x;
                        staticDoorY = room.doorLeftRight.y;

                        for (int x = room.doorLeftRight.x - 1; x > untilLeftRight; x--)
                        {
                            mapList[staticDoorY + 1][x].type = Type.WALL;
                            mapList[staticDoorY][x].type = Type.GROUND;
                            mapList[staticDoorY - 1][x].type = Type.WALL;
                        }
                        for (int y = room.doorUpDown.y + 1; y < untilUpDown; y++)
                        {
                            mapList[y][staticDoorX - 1].type = Type.WALL;
                            mapList[y][staticDoorX].type = Type.GROUND;
                            mapList[y][staticDoorX + 1].type = Type.WALL;
                        }
                        break;

                    case 2:
                        staticDoorX = room.doorUpDown.x;
                        staticDoorY = room.doorLeftRight.y;

                        for (int x = room.doorLeftRight.x + 1; x < untilLeftRight; x++)
                        {
                            mapList[staticDoorY + 1][x].type = Type.WALL;
                            mapList[staticDoorY][x].type = Type.GROUND;
                            mapList[staticDoorY - 1][x].type = Type.WALL;
                        }
                        for (int y = room.doorUpDown.y - 1; y > untilUpDown; y--)
                        {
                            mapList[y][staticDoorX - 1].type = Type.WALL;
                            mapList[y][staticDoorX].type = Type.GROUND;
                            mapList[y][staticDoorX + 1].type = Type.WALL;
                        }
                        break;

                    case 3:
                        staticDoorX = room.doorUpDown.x;
                        staticDoorY = room.doorLeftRight.y;

                        for (int x = room.doorLeftRight.x - 1; x > untilLeftRight; x--)
                        {
                            mapList[staticDoorY + 1][x].type = Type.WALL;
                            mapList[staticDoorY][x].type = Type.GROUND;
                            mapList[staticDoorY - 1][x].type = Type.WALL;
                        }
                        for (int y = room.doorUpDown.y - 1; y > untilUpDown; y--)
                        {
                            mapList[y][staticDoorX - 1].type = Type.WALL;
                            mapList[y][staticDoorX].type = Type.GROUND;
                            mapList[y][staticDoorX + 1].type = Type.WALL;
                        }
                        break;
                    default:
                        continue;
                }
            }
            //Spojeni cesticek uprostred mapy
            for (int i = 0; i < rooms.Count; i++)
            {
                int lefterDoor;
                int righterDoor;
                int higherDoor;
                int lowerDoor;

                switch (i)
                {
                    case 0:
                    case 2:
                        higherDoor = (rooms[i].doorLeftRight.y < rooms[i + 1].doorLeftRight.y) ? rooms[i].doorLeftRight.y : rooms[i + 1].doorLeftRight.y;
                        lowerDoor = (rooms[i].doorLeftRight.y > rooms[i + 1].doorLeftRight.y) ? rooms[i].doorLeftRight.y : rooms[i + 1].doorLeftRight.y;

                        for (int theY = higherDoor; theY <= lowerDoor; theY++)
                        {
                            mapList[theY][Constants.Constants.MAP_WIDTH / 2].type = Type.GROUND;

                            int searchStartX = (Constants.Constants.MAP_WIDTH / 2) - 1;
                            int searchStartY = theY - 1;
                            int searchZoneSquare = 3;
                            for (int y = searchStartY; y < searchStartY + searchZoneSquare; y++)
                            {
                                for (int x = searchStartX; x < searchStartX + searchZoneSquare; x++)
                                {
                                    if (mapList[y][x].type == Type.INACCESIBLE)
                                    {
                                        mapList[y][x].type = Type.WALL;
                                    }
                                }
                            }
                        }
                        if (i == 0)
                        {
                            lefterDoor = (rooms[i].doorUpDown.x < rooms[i + 2].doorUpDown.x) ? rooms[i].doorUpDown.x : rooms[i + 2].doorUpDown.x;
                            righterDoor = (rooms[i].doorUpDown.x > rooms[i + 2].doorUpDown.x) ? rooms[i].doorUpDown.x : rooms[i + 2].doorUpDown.x;

                            for (int theX = lefterDoor; theX <= righterDoor; theX++)
                            {
                                mapList[Constants.Constants.MAP_HEIGHT / 2][theX].type = Type.GROUND;

                                int searchStartX = theX - 1;
                                int searchStartY = (Constants.Constants.MAP_HEIGHT / 2) - 1;
                                int searchZoneSquare = 3;
                                for (int y = searchStartY; y < searchStartY + searchZoneSquare; y++)
                                {
                                    for (int x = searchStartX; x < searchStartX + searchZoneSquare; x++)
                                    {
                                        if (mapList[y][x].type == Type.INACCESIBLE)
                                        {
                                            mapList[y][x].type = Type.WALL;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 1:
                        lefterDoor = (rooms[i].doorUpDown.x < rooms[i + 2].doorUpDown.x) ? rooms[i].doorUpDown.x : rooms[i + 2].doorUpDown.x;
                        righterDoor = (rooms[i].doorUpDown.x > rooms[i + 2].doorUpDown.x) ? rooms[i].doorUpDown.x : rooms[i + 2].doorUpDown.x;
                        for (int theX = lefterDoor; theX <= righterDoor; theX++)
                        {
                            mapList[Constants.Constants.MAP_HEIGHT / 2][theX].type = Type.GROUND;

                            int searchStartX = theX - 1;
                            int searchStartY = (Constants.Constants.MAP_HEIGHT / 2) - 1;
                            int searchZoneSquare = 3;
                            for (int y = searchStartY; y < searchStartY + searchZoneSquare; y++)
                            {
                                for (int x = searchStartX; x < searchStartX + searchZoneSquare; x++)
                                {
                                    if (mapList[y][x].type == Type.INACCESIBLE)
                                    {
                                        mapList[y][x].type = Type.WALL;
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        continue;
                }
            }
        }
        public void GenerateObjectsInRooms(List<Room> roomsInThisFloor)
        {
            var possibleObjects = new Dictionary<int, (Tile, Room)>();
            //List<Tile> possibleObjects = new List<Tile>(); //Do tohoto listu se budou ukladat vsechny policka kam je mozne neco umistit a pak se nahodne vyberou
            int indexOfTile = 0;
            foreach (Room room in roomsInThisFloor)
            {
                for(int i = 0; i < room.floorTiles.Count; i++)
                {
                    possibleObjects.Add(indexOfTile , (room.floorTiles[i],room));
                    indexOfTile++;
                }
            }
            Console.WriteLine("Possible tiles to place items: " + possibleObjects.Count);

            Random random = new Random();
            int numberOfItemsOnFloor = random.Next(Constants.Constants.SPAWNRATE / (possibleObjects.Count / 8));
            Console.WriteLine("numberOfItemsOnFloor: " + numberOfItemsOnFloor);

            List<int> randomItemPositionList = new List<int>();
            for (int i = 0; i <= numberOfItemsOnFloor;i++)
            {
                int randomPosition = random.Next(0, possibleObjects.Count);
                while(randomItemPositionList.Contains(randomPosition)) //Taky: .Any(num => num == randomPosition)
                {
                    randomPosition = random.Next(0, possibleObjects.Count);
                }
                randomItemPositionList.Add(randomPosition);
            }
        }
        //Generovani hrace na mape
        public void SpawnPlayer()
        {
            foreach (List<Tile> tileRow in map)
            {
                foreach (Tile tile in tileRow)
                {
                    if (tile.objectInTile is not null && tile.objectInTile.typeOfObject == TypeOfObject.ENTRANCE)
                    {
                        player.x = tile.x;
                        player.y = tile.y;
                        map[player.y][player.x].isPlayer = true;
                    }
                }
            }
        }
        public void PlayerMovement(string direction)
        {
            switch (direction)
            {
                case "UP":
                    if (map[player.y - 1][player.x].type == Type.GROUND || map[player.y - 1][player.x].type == Type.DOOR)
                    {
                        map[player.y][player.x].isPlayer = false;
                        player.y--;
                        map[player.y][player.x].isPlayer = true;
                        Console.Clear();
                        DrawMap();
                    }
                    break;
                case "DOWN":
                    if (map[player.y + 1][player.x].type == Type.GROUND || map[player.y + 1][player.x].type == Type.DOOR)
                    {
                        map[player.y][player.x].isPlayer = false;
                        player.y++;
                        map[player.y][player.x].isPlayer = true;
                        Console.Clear();
                        DrawMap();
                    }
                    break;
                case "LEFT":
                    if (map[player.y][player.x - 1].type == Type.GROUND || map[player.y][player.x - 1].type == Type.DOOR)
                    {
                        map[player.y][player.x].isPlayer = false;
                        player.x--;
                        map[player.y][player.x].isPlayer = true;
                        Console.Clear();
                        DrawMap();
                    }
                    break;
                case "RIGHT":
                    if (map[player.y][player.x + 1].type == Type.GROUND || map[player.y][player.x + 1].type == Type.DOOR)
                    {
                        map[player.y][player.x].isPlayer = false;
                        player.x++;
                        map[player.y][player.x].isPlayer = true;
                        Console.Clear();
                        DrawMap();
                    }
                    break;
            }

        }
        public void CheckForUsable()
        {
            static void PlaceItemInSlot(int itemNumber)
            {
                // Add the item to the player's inventory or perform any other logic you need
                Console.WriteLine($"Placing item {itemNumber} into the inventory...");
                
                // + logika toho dávat věci do inventáře

            }

            //vytvoření Dictionary s odpovidajicima akcema dle zmacknuti tlacitka, muze tu byt pridana taky mechanika toho ze se neco rozbije
            var itemActions = new Dictionary<int, Action<int>>();
            for (int i = 1; i < player.inventory.Count; i++)
            {
                itemActions.Add(i, PlaceItemInSlot); 
            }

            List<Tile> objectsForUsage = new List<Tile>(); 
            for (int y = player.y - 1; y <= player.y + 1; y++)
            {
                for(int x = player.x - 1; x <= player.x + 1; x++)
                {
                    if (map[y][x].objectInTile != null && (map[y][x].objectInTile.typeOfObject == TypeOfObject.EXIT || map[y][x].objectInTile.typeOfObject == TypeOfObject.EXIT))
                    {
                        objectsForUsage.Add(map[y][x]);
                    }

                }
            }


            if (objectsForUsage.Any(tile => tile.objectInTile.typeOfObject == TypeOfObject.EXIT))
            {
                Console.WriteLine("Objekty v okolí: " + objectsForUsage.Count);

                foreach (Tile item in objectsForUsage)
                {
                    Console.Write("In Inventory: ");
                    for (int i = 0; i < player.inventory.Count; i++)
                    {
                        Console.Write(i + ") " + player.inventory[i].name + ", ");
                    }
                    Console.WriteLine();

                    int itemNumber = objectsForUsage.IndexOf(item);
                    Console.Write("In which position would you like to put your item number:" + itemNumber);
                    for (int i = 0; i < player.inventory.Count; i++)
                    {
                        Console.Write(i + "), ");
                    }
                    Console.WriteLine();



                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (int.TryParse(keyInfo.KeyChar.ToString(), out int selectedPosition))
                    {
                        if (selectedPosition >= 0 && selectedPosition < player.inventory.Count)
                        {
                            // Get the corresponding action for the selected item
                            if (itemActions.TryGetValue(selectedPosition + 1, out Action<int> action))
                            {
                                action(itemNumber); // Execute the action passing the item number
                            }
                            else
                            {
                                Console.WriteLine("Invalid selection. Please choose a valid item number.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection. Please choose a valid position number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                    
                }
                objectsForUsage.Clear();
            }
            else
            {

            }
        }
        public void DrawMap()
        {
            //StringBuilder stringBuilder = new StringBuilder();

            int differenceY = ((player.y - Constants.Constants.MAP_drawY) <= 0)
            ? player.y - Constants.Constants.MAP_drawY : 
                              ((player.y + Constants.Constants.MAP_drawY) >= Constants.Constants.MAP_HEIGHT)
            ?  (player.y + Constants.Constants.MAP_drawY) - Constants.Constants.MAP_HEIGHT
            : 0;

            int differenceX = ((player.x - Constants.Constants.MAP_drawX) <= 0)
            ? player.x - Constants.Constants.MAP_drawX : 
                              ((player.x + Constants.Constants.MAP_drawX) >= Constants.Constants.MAP_WIDTH)
            ? (player.x + Constants.Constants.MAP_drawX) - Constants.Constants.MAP_WIDTH
            : 0;

            //Konstanty pro ulehceni pocitani
            int drawY = Constants.Constants.MAP_drawY;
            int drawX = Constants.Constants.MAP_drawX;

            Console.WriteLine("DifferenceY: " + differenceY + ", DrawY:" + drawY);
            Console.WriteLine("DifferenceX: " + differenceX + ", DrawX" + drawX);
            Console.WriteLine("PlayerX: " + player.x + "PlayerY: " + player.y);

            //Prochazim od hrace mínus vykreslovaci polomer ktery se meni dle vzdalenost az po hrace + vykreslovaci polomer a plus rozdil
            for (int y = player.y - (drawY + differenceY); y < player.y + (drawY - differenceY); y++)
            {
                for (int x = player.x - (drawX + differenceX); x < player.x + (drawX - differenceX); x++)
                {
                    switch (map[y][x].type)
                    {
                        case Type.WALL:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("▓▓");

                            break;
                        case Type.GROUND:
                            //Console.BackgroundColor = ConsoleColor.Green;
                            if (map[y][x].isPlayer)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(" ☺");
                            }
                            else if (map[y][x].objectInTile is null || map[y][x].objectInTile.typeOfObject == TypeOfObject.NONE)
                            {
                                Console.Write(" .");
                            }
                            else
                            {
                                switch (map[y][x].objectInTile.typeOfObject)
                                {
                                    case TypeOfObject.ENTRANCE:
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write(" H");
                                        break;
                                    case TypeOfObject.EXIT:
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write(">>");
                                        break;
                                    case TypeOfObject.ITEM:

                                        break;
                                    default:
                                        continue;
                                }

                            }
                            break;
                        case Type.INACCESIBLE:
                            Console.Write("??");
                            break;
                        case Type.DOOR:
                            if (map[y][x].isPlayer)
                            {
                                Console.Write(" ☺");
                            }
                            else
                            {
                                Console.Write("[]");
                            }
                            break;
                        default:
                            Console.Write("  ");
                            break;

                    }
                    Console.ResetColor(); // Reset color to the default
                }
                Console.WriteLine();
            }

        }

    }
}
/*//Prochazim od hrace mínus vykreslovaci polomer ktery se meni dle vzdalenost az po hrace + vykreslovaci polomer a plus rozdil
for (int y = player.y - (drawY + differenceY); y < player.y + (drawY - differenceY); y++)
{
    StringBuilder stringBuilderRow = new StringBuilder();
    for (int x = player.x - (drawX + differenceX); x < player.x + (drawX - differenceX); x++)
    {
        switch (map[y][x].type)
        {
            case Type.WALL:
                stringBuilderRow.Append("▓▓");
                break;
            case Type.GROUND:
                if (map[y][x].isPlayer)
                {
                    stringBuilderRow.Append(" ☺");
                }
                else if (map[y][x].objectInTile is null || map[y][x].objectInTile.typeOfObject == TypeOfObject.NONE)
                {
                    stringBuilderRow.Append(" .");
                }
                else
                {
                    switch (map[y][x].objectInTile.typeOfObject)
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
                if (map[y][x].isPlayer)
                {
                    stringBuilderRow.Append(" ☺");
                }
                else
                {
                    stringBuilderRow.Append("[]");
                }
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

//}
//List<List<Tile>> tileMap = map;

//Předělat tak, že se bude generovat pouze ~25 polí okolo hráče
//Stálo by za to také vytvořit novou Class coloured string, aby jednotilivé chars měli rozdílné barvy

/*            
foreach (Room room in rooms)
{
    Console.WriteLine();
    Console.WriteLine(room.type + " " + room.width + " " + room.height + " " + room.TopCornerX + " " + room.TopCornerY);
}
/*

StringBuilder stringBuilderRow = new StringBuilder();
foreach (List<Tile> row in map)
{
    foreach (Tile tile in row)
    {

        switch (tile.type)
        {
            case Type.WALL:
                stringBuilderRow.Append("▓▓");
                break;
            case Type.GROUND:
                if (tile.isPlayer)
                {
                    stringBuilderRow.Append(" ☺");
                }
                else if (tile.objectInTile is null || tile.objectInTile.typeOfObject == TypeOfObject.NONE)
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
                if (tile.isPlayer)
                {
                    stringBuilderRow.Append(" ☺");
                }
                else
                {
                    stringBuilderRow.Append("[]");
                }
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
}//*/
