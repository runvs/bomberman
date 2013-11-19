using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Statistic
    {
        public Statistic()
        {
            ItsADraw = false;
            DestroyedBlocks = new int[2];
            DestroyedBlocks[0] = 0;
            DestroyedBlocks[1] = 0;

            PlacedBombs = new int[2];
            PlacedBombs[0] = 0;
            PlacedBombs[1] = 0;

            PickedPowerUps = new int[2];
            PickedPowerUps[0] = 0;
            PickedPowerUps[1] = 0;

            font = new SFML.Graphics.Font("../gfx/font.ttf");
        }

        private SFML.Graphics.Font font;
        public int WinnerNumber;

        public bool ItsADraw { get; set; }

        public String WinnerName { get; set;}

        public int[] DestroyedBlocks {get; set;}
        public int[] PlacedBombs { get; set; }
        public int[] PickedPowerUps { get; set; }
        

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            SFML.Graphics.Text text = new SFML.Graphics.Text();
            if (!ItsADraw)
            {

                text.DisplayedString = "And the Winner is ";
                text.Font = font;
                text.Scale = new SFML.Window.Vector2f(1.5f, 1.5f);
                text.Position = new SFML.Window.Vector2f(400 - text.GetGlobalBounds().Width / 2.0f, 150 - text.GetGlobalBounds().Height / 2.0f);
                rw.Draw(text);

                text = new SFML.Graphics.Text();
                text.Font = font;
                text.DisplayedString = WinnerName;
                text.Scale = new SFML.Window.Vector2f(1.5f, 1.5f);
                text.Position = new SFML.Window.Vector2f(400 - text.GetGlobalBounds().Width / 2.0f, 206 - text.GetGlobalBounds().Height / 2.0f);

                if (WinnerNumber == 1)
                {
                    text.Color = GameProperties.Player1Color();
                }
                else if (WinnerNumber == 2)
                {
                    text.Color = GameProperties.Player2Color();
                }
                
                rw.Draw(text);

            }
            else
            {
                text.DisplayedString = "It's a Draw!";
                text.Font = font;
                text.Scale = new SFML.Window.Vector2f(1.5f, 1.5f);
                text.Position = new SFML.Window.Vector2f(400 - text.GetGlobalBounds().Width / 2.0f, 150 - text.GetGlobalBounds().Height / 2.0f);
                rw.Draw(text);
            }

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = "Name";
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(20, 350 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = "Player 1";
            text.Color = GameProperties.Player1Color();
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(380 - text.GetGlobalBounds().Width / 2.0f, 350 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = "Player 2";
            text.Color = GameProperties.Player2Color();
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(530 - text.GetGlobalBounds().Width / 2.0f, 350 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);


            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = "Placed Bombs";
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(20, 380 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = PlacedBombs[0].ToString();
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(380 - text.GetGlobalBounds().Width / 2.0f, 380 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = PlacedBombs[1].ToString();
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(530 - text.GetGlobalBounds().Width / 2.0f, 380 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);


            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = "Destroyed Blocks";
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(20, 410 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = DestroyedBlocks[0].ToString();
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(380 - text.GetGlobalBounds().Width / 2.0f, 410 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = DestroyedBlocks[1].ToString();
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(530 - text.GetGlobalBounds().Width / 2.0f, 410 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);


            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = "Picked PowerUps";
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(20, 440 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = PickedPowerUps[0].ToString();
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(380 - text.GetGlobalBounds().Width / 2.0f, 440 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.Font = font;
            text.DisplayedString = PickedPowerUps[1].ToString();
            text.Color = SFML.Graphics.Color.White;
            text.Scale = new SFML.Window.Vector2f(1.0f, 1.0f);
            text.Position = new SFML.Window.Vector2f(530 - text.GetGlobalBounds().Width / 2.0f, 440 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            
        }


    }
}
