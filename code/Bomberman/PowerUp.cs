using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class PowerUp
    {
        public PowerUp(SFML.Window.Vector2i pos)
        {
            ChooseRandomPowerUpType();
            PositionInTiles = pos;
            Picked = false;
        }

        System.Random myRandomGenerator = new Random();

        public bool Picked { get; set; }

        public enum PowerUpType
        {
            MoreBombs,
            StrongerBombs
        }

        public PowerUpType Type { get; private set; }

        private void ChooseRandomPowerUpType()
        {
            if (myRandomGenerator.Next(1) == 0)
            {
                Type = PowerUpType.MoreBombs;
            }
            else
            {
                Type = PowerUpType.StrongerBombs;
            }
            
        }

        public SFML.Window.Vector2i PositionInTiles { get; set; }

    }
}
