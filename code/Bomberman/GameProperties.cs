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

    }
}
