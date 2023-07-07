using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Characters
{
    public class Enemy
    {
        public int health { get; set; }
        public int attackPower { get; set; }
        public int defense { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int type { get; set; }
        public Enemy(int health, int attackPower, int defense, int positionX, int positionY, int type)
        {
            this.health = health;
            this.attackPower = attackPower;
            this.defense = defense;
            this.positionX = positionX;
            this.positionY = positionY;
            this.type = type;
        }
        public void Move()
        {
            //TO DO
        }
        public void Attack(Player player)
        {
            int damage = attackPower - player.defense;
            if (damage > 0)
            {
                player.health -= damage;
            }
        }

    }
}
