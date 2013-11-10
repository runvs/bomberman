using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Explosion
    {
        private World myWorld;
        public Explosion(World world, SFML.Window.Vector2i position, ExplosionDirection dir)
        {
            myWorld = world;
            Position = position;
            Direction = dir;
            ExplosionDone = false;
            explosionTime = GameProperties.ExplosionTime();
        }

        public enum ExplosionDirection
        {
            DirUp,
            DirDown,
            DirLeft,
            DirRight
        }

        public ExplosionDirection Direction { get; set; }


        public void Update(float deltaT)
        {
            explosionTime -= deltaT;
            if( explosionTime <= 0)
            {
                ExplosionDone = true;
            }
        }

        private float explosionTime;

        SFML.Window.Vector2i Position { get; set; }

        public bool ExplosionDone { get; private set; }

    }
}
