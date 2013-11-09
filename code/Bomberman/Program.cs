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

            World myWorld = new World(1);


            while (applicationWindow.IsOpen())
            {

                applicationWindow.DispatchEvents();

                myWorld.GetInput();

                myWorld.Update(0.155f);

                applicationWindow.Clear(new SFML.Graphics.Color(200,10,10));
                myWorld.Draw(applicationWindow);

                applicationWindow.Display();
            }
        }
    }
}
