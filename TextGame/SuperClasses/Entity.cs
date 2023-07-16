using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.SuperClasses
{
    public class Entity
    {
        public int x { get; set; }
        public int y { get; set; }
        public int health { get; set; }
        public int defense { get; set; }
        public int attackPower { get; set; }
        public int attackRange { get; set; }


        public Entity(int x, int y, int health, int defense, int attackPower, int attackRange)
        {
            this.x = x;
            this.y = y;
            this.health = health;
            this.defense = defense;
            this.attackPower = attackPower;
            this.attackRange = attackRange;
        }
    }
}
