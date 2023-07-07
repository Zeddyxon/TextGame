using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Characters;

namespace TextGame.Potions
{
    public class HealtPotionClassic
    {
            public int HealingAmount { get; private set; }

            public HealtPotionClassic(int healingAmount)
            {
                HealingAmount = healingAmount;
            }

            public void Use(Player player)
            {
                player.health += HealingAmount;
            }

    }
}
