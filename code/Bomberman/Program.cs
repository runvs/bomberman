using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Program
    {

        static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            SFML.Graphics.RenderWindow window = (SFML.Graphics.RenderWindow)sender;
            window.Close();
        }

        static void OnKeyPress(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == SFML.Window.Keyboard.Key.Escape)
            {
                SFML.Graphics.RenderWindow window = (SFML.Graphics.RenderWindow)sender;
                window.Close();
            }
        }


        static void Main(string[] args)
        {
            SFML.Graphics.RenderWindow applicationWindow = new SFML.Graphics.RenderWindow(new SFML.Window.VideoMode(800,600, 32), "Bomberman");

            applicationWindow.SetFramerateLimit(60);

            // fuddle with resizing the images later on
            applicationWindow.Closed += new EventHandler(OnClose);
            applicationWindow.KeyPressed += new EventHandler<SFML.Window.KeyEventArgs>(OnKeyPress);

            Game myGame = new Game();
            

            int startTime = Environment.TickCount;
            int endTime = startTime;
            float time = 16.7f;

            while (applicationWindow.IsOpen())
            {
                if (startTime != endTime)
                {
                    time = (float)(endTime - startTime) / 1000.0f;
                }
                startTime = Environment.TickCount;

                applicationWindow.DispatchEvents();

                myGame.GetInput();

                myGame.Update(time);

                applicationWindow.Clear(new SFML.Graphics.Color(200,10,10));
                myGame.Draw(applicationWindow);

                applicationWindow.Display();
                endTime = Environment.TickCount;
            }
        }
    }
}
