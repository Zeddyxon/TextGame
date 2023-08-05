using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Characters;

namespace TextGame.Constants
{
    public class GameStats
    {
        public int floorTunr;
        public int gameTurn;
        public int floor;
        Player player;
        public GameStats(int floorTurn, int gameTurn, int floor)
        {
            this.floorTunr = floorTurn;
            this.gameTurn = gameTurn;
            this.floor = floor;
            this.player = Map.Map.player;
        }

    }
}
