using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.SuperClasses;

namespace TextGame.Characters
{
    public class Player : Entity
    {
        private int speed;
        public Player(int x, int y, int health, int attackPower, int attackRange, int defense, int speed) : base (x, y, health, attackPower, attackRange, defense)
        {
            base.x = x;
            base.y = y;
            base.health = health;
            base.attackPower = attackPower;
            base.defense = defense;

            this.speed = speed;
        }

        public void Move()
        {
            // Wait for arrow key press
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);


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
