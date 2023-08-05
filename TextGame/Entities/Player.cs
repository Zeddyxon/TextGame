using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Items;
using TextGame.Object;
using TextGame.SuperClasses;

namespace TextGame.Characters
{
    public class Player : Entity
    {
        public int speed;
        public List<Item> inventory = new List<Item>(5)
        {
            new Item(0, 0, "none", ItemType.POTION),
            new Item(0, 0, "none", ItemType.POTION),
            new Item(0, 0, "none", ItemType.POTION),
            new Item(0, 0, "none", ItemType.POTION),
            new Item(0, 0, "none", ItemType.POTION)
        };
       
        public Player(int x, int y, int health, int maxHealth, int attackPower, int attackRange, int defense, int speed) : base (x, y, health, attackPower, attackRange, defense)
        {
            base.x = x;
            base.y = y;
            base.health = health;
            base.maxHealth = maxHealth;
            base.attackPower = attackPower;
            base.defense = defense;
            
            this.speed = speed;
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
