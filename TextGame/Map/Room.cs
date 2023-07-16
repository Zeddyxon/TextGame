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
        TRADER,

    }
    internal class Room
    {
        public RoomType type;
        public int width;
        public int height;
        public int TopCornerX;
        public int TopCornerY;
     
        public Room (RoomType type, int width, int height, int TopCornerX, int TopCornerY)
        {
            this.type = type;
            this.width = width;
            this.height = height;
            this.TopCornerX = TopCornerX;
            this.TopCornerY = TopCornerY;
        }
    }


}
