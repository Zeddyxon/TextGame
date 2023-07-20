using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Items
{
    public enum ItemType
    {
        POTION,
        WEAPEON,
        ARMOUR
    }
    public class Item
    {
        public int x;
        public int y;
        public ItemType type;

        public Item(int x, int y, ItemType type) 
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
