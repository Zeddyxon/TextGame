using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.SuperClasses;

namespace TextGame.Characters
{
    public class Enemy : Entity
    {
        public enum Mob
        {
            ZOMBIE,
            SKELETON,
            RAT
        }
        public Enemy(int x, int y, int health, int defense, int attackPower,int attackRange) : base (x, y, health, attackPower, attackRange, defense)
        {
            base.x = x;
            base.y = y;
            base.health = health;
            base.defense = defense;
            base.attackPower = attackPower;
            base.attackRange = attackRange;
            
        }
        public void Move()
        {
            /*AI TO DO
            
            if(player is in the room)
            {

            }

             */
        }
        //Use this when Player is in the range
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
