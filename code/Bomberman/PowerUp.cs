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

            System.Console.Out.WriteLine("up spawned");

            LoadGraphics();
        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            rw.Draw(spritePowerUp);
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
            if (myRandomGenerator.Next(2) == 0)
            {
                Type = PowerUpType.MoreBombs;
            }
            else
            {
                Type = PowerUpType.StrongerBombs;
            }
            
        }

        public SFML.Window.Vector2i PositionInTiles { get; set; }

        private SFML.Graphics.Texture texturePowerUp;
        private SFML.Graphics.Sprite spritePowerUp;

        private void LoadGraphics()
        {
            if (Type == PowerUpType.MoreBombs)
            {
                texturePowerUp = new SFML.Graphics.Texture("../gfx/up_morebombs.png");
            }
            else if (Type == PowerUpType.StrongerBombs)
            {
                texturePowerUp = new SFML.Graphics.Texture("../gfx/up_morebombs.png");
            }

            spritePowerUp = new SFML.Graphics.Sprite(texturePowerUp);

            spritePowerUp.Position = new SFML.Window.Vector2f((float)(PositionInTiles.X) * GameProperties.TileSizeInPixel(), (float)(PositionInTiles.Y) * GameProperties.TileSizeInPixel());

        }

    }
}
