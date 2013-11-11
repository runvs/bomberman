using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Bomb
    {
        public Bomb (World world, SFML.Window.Vector2i position)
        {
            myWorld = world;
            timeUntilExplosion = GameProperties.BombCounterTime();
            BombPositionInTiles = position;

            Exploded = false;

            try
            {
                LoadGraphics();
            }
            catch (SFML.LoadingFailedException e)
            {

                System.Console.Out.WriteLine("Error loading bomb Graphics.");
                System.Console.Out.WriteLine(e.ToString());
            }

            bombSprite.Position = new SFML.Window.Vector2f((float)(GameProperties.TileSizeInPixel() * position.X), (float)(GameProperties.TileSizeInPixel() * position.Y));

        }

        public void Update(float deltaT)
        {
            timeUntilExplosion -= deltaT;

            if (timeUntilExplosion <= 0)
            {
                Explode();
            }
        }

        private void Explode ( )
        {
            Exploded = true;
        }

        float timeUntilExplosion;

        public SFML.Window.Vector2i BombPositionInTiles { get; set; }

        public bool Exploded { get; private set; }

        World myWorld;
        private SFML.Graphics.Sprite bombSprite;
        private SFML.Graphics.Texture bombTexture;


        private void LoadGraphics()
        {
            bombTexture = new SFML.Graphics.Texture("../gfx/bomb.png");
            bombSprite = new SFML.Graphics.Sprite(bombTexture);
        }


        internal void Draw(SFML.Graphics.RenderWindow rw)
        {
            rw.Draw(bombSprite);
        }
    }
}
