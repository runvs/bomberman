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
            return 0.25f;
        }

        internal static int DefaultBombStrengthInTiles()
        {
            return 1;
        }

        internal static double PowerUpSpawnProbability()
        {
            return 1.0f
            ;
        }
    }
}
