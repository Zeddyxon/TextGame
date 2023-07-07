using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Characters
{
    public class Player
    {
        public int health { get; set; }
        public int attackPower { get; set; }
        public int defense { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }

        //Weapeon

        public Player(int health, int attackPower, int defense, int positionX, int positionY)
        {
            this.health = health;
            this.attackPower = attackPower;
            this.defense = defense;
            this.positionX = positionX;
            this.positionY = positionY;
        }

        public void Move(int deltaX, int deltaY)
        {
            positionX += deltaX;
            positionY += deltaY;
        }

        public void Attack(Enemy enemy)
        {
            // Calculate damage based on player's attack power and enemy's defense
            int damage = attackPower - enemy.defense;
            if (damage > 0)
            {
                enemy.health -= damage;
            }
        }

    }
}
