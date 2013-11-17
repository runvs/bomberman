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
            font = new SFML.Graphics.Font("../gfx/font.ttf");
        }

        private SFML.Graphics.Font font;
        public int WinnerNumber;

        public bool ItsADraw { get; set; }

        public String WinnerName { get; set;}
        

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
                text.Position = new SFML.Window.Vector2f(400 - text.GetGlobalBounds().Width / 2.0f, 220 - text.GetGlobalBounds().Height / 2.0f);

                if (WinnerNumber == 1)
                {
                    text.Color = GameProperties.Player1Color();
                }
                else if (WinnerNumber == 2)
                {
                    text.Color = GameProperties.Player2Color();
                }
                else if (WinnerNumber == 2)
                {
                    text.Color = GameProperties.Player3Color();
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

            
        }


    }
}
