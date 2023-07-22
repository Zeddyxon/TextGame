using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Map
{
    public enum RoomType
    {
        ENTRACNCE,
        EXIT,
        CLASSICROOM,
        PEACEFULLROOM,
        BOSSROOM,
        TRADER

    }
    public class Room
    {
        public RoomType type;
        public int width;
        public int height;
        public int TopCornerX;
        public int TopCornerY;

        public Tile doorLeftRight;
        public Tile doorUpDown;

        public List<Tile> floorTiles = new List<Tile>();

        public Room (RoomType type, int width, int height, int TopCornerX, int TopCornerY)
        {
            this.type = type;
            this.width = width;
            this.height = height;
            this.TopCornerX = TopCornerX;
            this.TopCornerY = TopCornerY;
        }
        public void GenerateEntrance(Room room)
        {
            Random random = new Random();
            int entranceX = random.Next(TopCornerX + 1, TopCornerX + width - 1);
            int entranceY = random.Next(TopCornerY + 1, TopCornerY + height - 1);

            //Console.WriteLine("Entrance is Y:" + entranceY + " X:" + entranceX);
            foreach (Tile tile in room.floorTiles)
            {
                if (tile.x == entranceX && tile.y == entranceY)
                {
                    tile.objectInTile = new Object.ObjectInTile(Object.TypeOfObject.ENTRANCE);
                }
            }
        }
        public void GenerateExit(Room room)
        {
            Random random = new Random();
            int exitX = random.Next(TopCornerX + 1, TopCornerX + width - 1);
            int exitY = random.Next(TopCornerY + 1, TopCornerY + height - 1);

            //Console.WriteLine("Entrance is Y:" + entranceY + " X:" + entranceX);
            foreach (Tile tile in room.floorTiles)
            {
                if (tile.x == exitX && tile.y == exitY)
                {
                    tile.objectInTile = new Object.ObjectInTile(Object.TypeOfObject.EXIT);
                }
            }
        }
    }
}
