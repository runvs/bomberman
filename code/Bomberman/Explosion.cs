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
        private Player myOwner;
        public Player Owner { get { return myOwner; } }
        public Explosion(World world, SFML.Window.Vector2i position, ExplosionDirection dir, int number, int numberMax, Player player)
        {
            myWorld = world;
            myOwner = player;

            PositionInTiles = position;
            Direction = dir;
            ExplosionDone = false;
            explosionTime = GameProperties.ExplosionTime();

            ExplosionNumber = number;
            ExplosionNumberMax = numberMax;
            HasSpawnedOtherExplosions = false;

            try
            {
                LoadGraphics();
            }
            catch (SFML.LoadingFailedException e)
            {

                System.Console.Out.WriteLine("Error loading explosion Graphics.");
                System.Console.Out.WriteLine(e.ToString());
            }

            explosionSprite.Origin = new SFML.Window.Vector2f(0.0f, 0.0f);
            if (Direction == ExplosionDirection.DirDown || Direction == ExplosionDirection.DirUp)
            {
                explosionSprite.Rotation = 90.0f;
                explosionSprite.Origin = new SFML.Window.Vector2f(0.0f, +explosionSprite.GetLocalBounds().Width);// rotate also rotates the coordinate system. Think about this crazy shit
            }

            explosionSprite.Position = new SFML.Window.Vector2f((float)(GameProperties.TileSizeInPixel() * position.X), (float)(GameProperties.TileSizeInPixel() * position.Y));
        }

        public bool HasSpawnedOtherExplosions { get; set; }

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

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            rw.Draw(explosionSprite);
        }

        private float explosionTime;

        public SFML.Window.Vector2i PositionInTiles { get; private set; }

        public bool ExplosionDone { get; private set; }


        SFML.Graphics.Texture explosiontexture;
        SFML.Graphics.Sprite explosionSprite;

        public int ExplosionNumber { get; private set; }
        public int ExplosionNumberMax { get; private set; }

        private void LoadGraphics()
        {
            
            explosiontexture = new SFML.Graphics.Texture("../gfx/explosion.png");
            if (ExplosionNumber == 1)
            {
                explosiontexture = new SFML.Graphics.Texture("../gfx/explosion_base.png");
            }
            explosionSprite = new SFML.Graphics.Sprite(explosiontexture);
        }


    }
}
