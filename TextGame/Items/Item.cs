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
        public string name;
        public ItemType type;

        public Item(int x, int y, string name, ItemType type) 
        {
            this.x = x;
            this.y = y;
            this.name = name;
            this.type = type;
        }
        
    }
}