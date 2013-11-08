using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Player
    {

        public Player()
        {
            try
            {
                LoadGraphics();
            }
            catch (SFML.LoadingFailedException e)
            {

                System.Console.Out.WriteLine("Error loading player Graphics.");
                System.Console.Out.WriteLine(e.ToString());


            }
        }



        private bool movingRight;
        private bool movingLeft;
        private bool movingUp;
        private bool movingDown;

        private SFML.Window.Vector2u positionInTiles = new SFML.Window.Vector2u(0,0);

        public void GetInput()
        {
            ResetActionMap();
            MapInputToActions();
        }

       

    

        public void Update(float deltaT)
        {
            SFML.Window.Vector2i newPositioninTiles = new SFML.Window.Vector2i( (int) positionInTiles.X, (int)positionInTiles.Y);
            if (movingDown)
            {
                newPositioninTiles.Y += 1;
            }
            if (movingUp)
            {
                newPositioninTiles.Y -= 1;
            }

            if (movingRight)
            {
                newPositioninTiles.X += 1;
            }
            if (movingLeft)
            {
                newPositioninTiles.X -= 1;
            }

            if (newPositioninTiles.X < 0)
            {
                newPositioninTiles.X = 0;
            }
            if (newPositioninTiles.Y < 0)
            {
                newPositioninTiles.Y = 0;
            }

          // TODO Check for upper limit
            // TODO Check if Tile is free

            SFML.Window.Vector2u tempvec = new SFML.Window.Vector2u((uint)newPositioninTiles.X, (uint)newPositioninTiles.Y);
            positionInTiles = tempvec;

            PositionSprite();

        }

        private void PositionSprite()
        {
            this.playerSprite.Position = new SFML.Window.Vector2f((float)(positionInTiles.X * GameProperties.TileSizeInPixel()), (float)(positionInTiles.Y * GameProperties.TileSizeInPixel()));

        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            rw.Draw(this.playerSprite);
        }

        private void ResetActionMap()
        {
            movingRight = false;
            movingLeft = false;
            movingUp = false;
            movingDown = false;
        }

        private void MapInputToActions()
        {
            // TODO more players/keys later
            // TODO Input Timer every 50 ms or so

            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Left))
            {
                MoveLeftAction();
            }
            else if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Right))
            {
                MoveRightAction();
            }
            else if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Down))
            {
                MoveDownAction();
            }
            else if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Up))
            {
                MoveUpAction();
            }

            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Space))
            {
                PlaceBombAction();
            }
        }

        private void MoveUpAction()
        {
            this.movingUp = true;
        }
        private void MoveDownAction()
        {
            this.movingDown = true;
        }
        private void MoveRightAction()
        {
            this.movingRight = true;
        }
        private void MoveLeftAction()
        {
            this.movingLeft = true;
        }


        private void PlaceBombAction()
        {

        }


        private SFML.Graphics.Texture playerTexture;
        private SFML.Graphics.Sprite playerSprite;

        private void LoadGraphics()
        {
            playerTexture = new SFML.Graphics.Texture("../gfx/player.png");
            playerSprite = new SFML.Graphics.Sprite(playerTexture);
        }


    }
}
