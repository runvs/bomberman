using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Tile
    {
        public Tile(SFML.Window.Vector2i pos, TileType tt)
        {
            this.TilePosition = pos;
            this.MyTileType = tt;

            try
            {
                LoadGraphics();
            }
            catch (SFML.LoadingFailedException e)
            {

                System.Console.Out.WriteLine("Error loading player Graphics.");
                System.Console.Out.WriteLine(e.ToString());
            }

            this.tileSprite.Position = new SFML.Window.Vector2f((float)(GameProperties.TileSizeInPixel() * this.TilePosition.X), (float)(GameProperties.TileSizeInPixel() * this.TilePosition.Y));



        }


        public enum TileType
        {
            TileTypeFree,
            TileTypeWall,
            TileTypeBreakable
        }

        public TileType MyTileType { get; set; }


        public void Update(float delatT)
        {

        }
        
        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            rw.Draw(tileSprite);
        }

        public SFML.Window.Vector2i TilePosition { get; set; }

        private SFML.Graphics.Texture tileTexture;
        private SFML.Graphics.Sprite tileSprite;

        private void LoadGraphics()
        {
            if (this.MyTileType == TileType.TileTypeFree)
            {
                tileTexture = new SFML.Graphics.Texture("../gfx/TileFree.png");
            }
            else if (this.MyTileType == TileType.TileTypeWall)
            {
                tileTexture = new SFML.Graphics.Texture("../gfx/TileWall.png");
            }

            tileSprite = new SFML.Graphics.Sprite(tileTexture);

        }


        internal bool IsTileBlocked()
        {
            if (this.MyTileType == TileType.TileTypeFree)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
