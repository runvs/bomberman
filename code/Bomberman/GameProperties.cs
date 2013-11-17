using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class GameProperties
    {
        public static int TileSizeInPixel()
        {
            return 24;
        }

        public static int WorldSizeInTiles()   // assuming a square World
        {
            return 13;
        }

        public static float PlayerMovementTime()
        {
            return 0.15f;
        }


        public static float BombCounterTime()
        {
            return 1.5f;
        }

        internal static float ExplosionTime()
        {
            return 0.35f;
        }

        internal static int DefaultBombStrengthInTiles()
        {
            return 1;
        }
        internal static int DefaultPlayersBombs()
        {
            return 1;
        }

        internal static double PowerUpSpawnProbability()
        {
            return 0.2f;
        }

        internal static SFML.Graphics.Color Player1Color()
        {
            return new SFML.Graphics.Color(0, 190, 255);
        }

        internal static SFML.Graphics.Color Player2Color()
        {
            return new SFML.Graphics.Color(190, 255, 0);
        }

        internal static SFML.Graphics.Color Player3Color()
        {
            return new SFML.Graphics.Color(255, 0, 190);
        }



    }
}
