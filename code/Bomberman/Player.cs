using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Player
    {

        public Player(World world)
        {
            myWorld = world;
            IsDead = false;


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

        private SFML.Window.Vector2i positionInTiles = new SFML.Window.Vector2i(0,0);

        public SFML.Window.Vector2i PositionInTiles
        {
            get
            {
                return positionInTiles;
            }
        }

        public void GetInput()
        {
            ResetActionMap();
            if (movementTimer <= 0.0f)
            {
                MapInputToActions();
            }
        }

       

    

        public void Update(float deltaT)
        {
            if (movementTimer >= 0.0f)
            {
                movementTimer -= deltaT;
            }


            MovePlayerToNewPosition();

        }

        private void MovePlayerToNewPosition()
        {
            SFML.Window.Vector2i newPositionInTiles = new SFML.Window.Vector2i((int)positionInTiles.X, (int)positionInTiles.Y);
            if (movingDown)
            {
                newPositionInTiles.Y += 1;
            }
            if (movingUp)
            {
                newPositionInTiles.Y -= 1;
            }

            if (movingRight)
            {
                newPositionInTiles.X += 1;
            }
            if (movingLeft)
            {
                newPositionInTiles.X -= 1;
            }

            CheckNewPositionIsInWorldAndFree(ref newPositionInTiles);



            SFML.Window.Vector2i tempvec = newPositionInTiles;
            positionInTiles = tempvec;

            PositionSprite();
        }

        private void CheckNewPositionIsInWorldAndFree(ref SFML.Window.Vector2i newPositionInTiles)
        {
            if (newPositionInTiles.X < 0)
            {
                newPositionInTiles.X = 0;
            }
            if (newPositionInTiles.Y < 0)
            {
                newPositionInTiles.Y = 0;
            }

            if (newPositionInTiles.X > GameProperties.WorldSizeInTiles() - 1)
            {
                newPositionInTiles.X = GameProperties.WorldSizeInTiles() - 1;
            }
            if (newPositionInTiles.Y > GameProperties.WorldSizeInTiles() - 1)
            {
                newPositionInTiles.Y = GameProperties.WorldSizeInTiles() - 1;
            }

            if (myWorld.IsTileBlocked(newPositionInTiles))
            {
                newPositionInTiles = new SFML.Window.Vector2i((int)(positionInTiles.X), (int)(positionInTiles.Y));
            }

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
            movementTimer = GameProperties.PlayerMovementTime();
            this.movingUp = true;
        }
        private void MoveDownAction()
        {
            movementTimer = GameProperties.PlayerMovementTime();
            this.movingDown = true;
        }
        private void MoveRightAction()
        {
            movementTimer = GameProperties.PlayerMovementTime();
            this.movingRight = true;
        }
        private void MoveLeftAction()
        {
            movementTimer = GameProperties.PlayerMovementTime();
            this.movingLeft = true;
        }


        private void PlaceBombAction()
        {
            SFML.Window.Vector2i pos = new SFML.Window.Vector2i((int)(positionInTiles.X), (int)(positionInTiles.Y));
            myWorld.SpawnBombOnPosition(pos);
        }


        private SFML.Graphics.Texture playerTexture;
        private SFML.Graphics.Sprite playerSprite;

        private void LoadGraphics()
        {
            playerTexture = new SFML.Graphics.Texture("../gfx/player.png");
            playerSprite = new SFML.Graphics.Sprite(playerTexture);
        }

        private float movementTimer = 0.0f; // time til two successive Movement commands
        
        private World myWorld;

        public bool IsDead { get; private set; }

        

        public void Die()
        {
            IsDead = true;
        }


    }
}
