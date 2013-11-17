using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Player
    {

        public Player(World world, int number)
        {
            myWorld = world;
            IsDead = false;

            playerNumber = number;
            

            SetPlayerNumberDependendProperties();

            BombStrenght = GameProperties.DefaultBombStrengthInTiles();
            PlayersBombsMax = GameProperties.DefaultPlayersBombs();
            

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

        private void SetPlayerNumberDependendProperties()
        {
            PlayerName = "Player" + playerNumber.ToString();

            if (playerNumber == 1)
            {
                PositionInTiles = new SFML.Window.Vector2i(0, 0);
            }
            else if (playerNumber == 2)
            {
                PositionInTiles = new SFML.Window.Vector2i(GameProperties.WorldSizeInTiles(), 0);
            }
            else if (playerNumber == 3)
            {
                PositionInTiles = new SFML.Window.Vector2i(0, GameProperties.WorldSizeInTiles());
            }
            else if (playerNumber == 4)
            {
                PositionInTiles = new SFML.Window.Vector2i(GameProperties.WorldSizeInTiles(), GameProperties.WorldSizeInTiles());
            }

        }



        private bool movingRight;
        private bool movingLeft;
        private bool movingUp;
        private bool movingDown;



        public SFML.Window.Vector2i PositionInTiles
        {
            get;
            private set;
        }

        public string PlayerName { get; private set; }

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
            SFML.Window.Vector2i newPositionInTiles = new SFML.Window.Vector2i((int)PositionInTiles.X, (int)PositionInTiles.Y);
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
            PositionInTiles = tempvec;

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
                newPositionInTiles = PositionInTiles;
            }

        }

        private void PositionSprite()
        {
            this.playerSprite.Position = new SFML.Window.Vector2f((float)(PositionInTiles.X * GameProperties.TileSizeInPixel()), (float)(PositionInTiles.Y * GameProperties.TileSizeInPixel()));

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

        SFML.Window.Keyboard.Key MoveLeftKey()
        {
            SFML.Window.Keyboard.Key actionKey = SFML.Window.Keyboard.Key.Left;
            if (playerNumber == 2)
            {
                actionKey = SFML.Window.Keyboard.Key.A;
            }
            else if (playerNumber == 3)
            {
                actionKey = SFML.Window.Keyboard.Key.Numpad4;
            }

            return actionKey;
        }

        SFML.Window.Keyboard.Key MoveRightKey()
        {
            SFML.Window.Keyboard.Key actionKey = SFML.Window.Keyboard.Key.Right;
            if (playerNumber == 2)
            {
                actionKey = SFML.Window.Keyboard.Key.D;
            }
            else if (playerNumber == 3)
            {
                actionKey = SFML.Window.Keyboard.Key.Numpad6;
            }

            return actionKey;
        }

        SFML.Window.Keyboard.Key MoveDownKey()
        {
            SFML.Window.Keyboard.Key actionKey = SFML.Window.Keyboard.Key.Down;
            if (playerNumber == 2)
            {
                actionKey = SFML.Window.Keyboard.Key.S;
            }
            else if (playerNumber == 3)
            {
                actionKey = SFML.Window.Keyboard.Key.Numpad5;
            }

            return actionKey;
        }

        SFML.Window.Keyboard.Key MoveUpKey()
        {
            SFML.Window.Keyboard.Key actionKey = SFML.Window.Keyboard.Key.Up;
            if (playerNumber == 2)
            {
                actionKey = SFML.Window.Keyboard.Key.W;
            }
            else if (playerNumber == 3)
            {
                actionKey = SFML.Window.Keyboard.Key.Numpad8;
            }

            return actionKey;
        }

        SFML.Window.Keyboard.Key BombKey()
        {
            SFML.Window.Keyboard.Key actionKey = SFML.Window.Keyboard.Key.RControl;
            if (playerNumber == 2)
            {
                actionKey = SFML.Window.Keyboard.Key.LShift;
            }
            else if (playerNumber == 3)
            {
                actionKey = SFML.Window.Keyboard.Key.Numpad0;
            }

            return actionKey;
        }


        private void MapInputToActions()
        {
            // TODO more players/keys later

            if (SFML.Window.Keyboard.IsKeyPressed(MoveLeftKey()))
            {
                MoveLeftAction();
            }
            else if (SFML.Window.Keyboard.IsKeyPressed(MoveRightKey()))
            {
                MoveRightAction();
            }
            else if (SFML.Window.Keyboard.IsKeyPressed(MoveDownKey()))
            {
                MoveDownAction();
            }
            else if (SFML.Window.Keyboard.IsKeyPressed(MoveUpKey()))
            {
                MoveUpAction();
            }

            if (SFML.Window.Keyboard.IsKeyPressed(BombKey()))
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
            SFML.Window.Vector2i pos = PositionInTiles;
            myWorld.SpawnBombOnPosition(pos, this);
        }


        private SFML.Graphics.Texture playerTexture;
        private SFML.Graphics.Sprite playerSprite;

        public int BombStrenght { get; private set; }

        private void LoadGraphics()
        {
            SFML.Graphics.Image playerImage = new SFML.Graphics.Image("../gfx/player.png");

            SFML.Graphics.Color playerColor = SFML.Graphics.Color.White;

            if (playerNumber == 1)
            {
                playerColor = GameProperties.Player1Color();
                
            }
            else if (playerNumber == 2)
            {
                playerColor = GameProperties.Player2Color();

            }
            else if (playerNumber == 3)
            {
                playerColor = GameProperties.Player3Color();
            }

            for (uint i = 0; i < playerImage.Size.X; ++i)
            {
                for (uint j = 0; j < playerImage.Size.Y; ++j)
                {
                    if (playerImage.GetPixel(i, j).Equals(SFML.Graphics.Color.White))
                    {
                        playerImage.SetPixel(i, j, playerColor);
                    }
                }
            }


            

            playerTexture = new SFML.Graphics.Texture(playerImage);

            playerSprite = new SFML.Graphics.Sprite(playerTexture);
        }

        private float movementTimer = 0.0f; // time til two successive Movement commands
        
        private World myWorld;
        public int playerNumber;

        public bool IsDead { get; private set; }

        

        public void Die()
        {
            IsDead = true;
        }


        public int PlayersBombsMax { get; private set; }


        internal void PickUpPowerUp(PowerUp.PowerUpType powerUpType)
        {
            if (powerUpType == PowerUp.PowerUpType.MoreBombs)
            {
                PlayersBombsMax++;
            }
            else if (powerUpType == PowerUp.PowerUpType.StrongerBombs)
            {
                this.BombStrenght++;
            }
        }
    }
}
