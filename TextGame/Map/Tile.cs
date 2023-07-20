using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Characters;
using TextGame.Items;
using TextGame.Object;

namespace TextGame.Map
{
    public enum Type { 
    WALL,
    DOOR,
    GROUND,
    OBJECT,
    INACCESIBLE
    }
    public class Tile
    {
        public int x { get; set; }
        public int y { get; set; }
        public Type type { get; set; }

        public Player? player { get; set; }
        public Enemy? enemy { get; set; }
        public ObjectInTile? objectInTile { get; set; } 

        public Tile(int x, int y, Type type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
