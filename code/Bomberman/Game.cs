using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Game
    {

        public Game()
        {
            gameState = State.Menu;
            font = new SFML.Graphics.Font("../gfx/font.ttf");
        }

        private SFML.Graphics.Font font;

        World myWorld;


        private enum State
        {
            Menu,
            Game,
            Score,
            Credits
        }

        private State gameState;

        public void GetInput()
        {
            if (timeTilNextInput < 0.0f)
            {
                if (gameState == State.Menu)
                {
                    GetInputMenu();
                }
                else if (gameState == State.Game)
                {
                    GetInputGame();
                }
                else if (gameState == State.Credits || gameState == State.Score)
                {
                    GetInputCreditsScore();
                }
            }
        }

        Statistic GameStats;

        public void Update(float deltaT)
        {
            if (timeTilNextInput >= 0.0f)
            {
                timeTilNextInput -= deltaT;
            }

            if (gameState == State.Game)
            {
                myWorld.Update(deltaT);
                if (myWorld.NumberOfPlayersAlive <= 1)
                {
                    GameStats = myWorld.EndThisRound();
                   ChangeGameState(State.Score);
                }
            }

        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            rw.Clear();
            if (gameState == State.Menu)
            {
                DrawMenu(rw);
            }
            else if (gameState == State.Game)
            {
                SFML.Graphics.View myView = new SFML.Graphics.View(new SFML.Window.Vector2f(400, 300), new SFML.Window.Vector2f(800, 600));
                //myView.Center = new SFML.Window.Vector2f(0.0f, 0.0f);
                myView.Zoom(0.55f);
                myView.Move(new SFML.Window.Vector2f(-190, -144));
                rw.SetView(myView);

                myWorld.Draw(rw);
            }
            else if (gameState == State.Credits)
            {
                DrawCredits(rw);
            }
            else if (gameState == State.Score)
            {
                rw.SetView(rw.DefaultView);
                DrawScore(rw);
                
            }
        }

        private void DrawMenu(SFML.Graphics.RenderWindow rw)
        {
            
            SFML.Graphics.Text text = new SFML.Graphics.Text();
            text.DisplayedString = "Bomberman";
            
            text.Font = font;
            text.Scale = new SFML.Window.Vector2f(2,2);
            text.Position = new SFML.Window.Vector2f(400 - text.GetGlobalBounds().Width / 2.0f, 150 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

            text = new SFML.Graphics.Text();
            text.DisplayedString = "Start [Return]";
            text.Font = font;
            text.Position = new SFML.Window.Vector2f(400 - text.GetGlobalBounds().Width / 2.0f, 250 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);


            text = new SFML.Graphics.Text();
            text.DisplayedString = "[C]redits";
            text.Font = font;
            text.Scale = new SFML.Window.Vector2f(0.75f, 0.75f);
            text.Position = new SFML.Window.Vector2f(30, 550 - text.GetGlobalBounds().Height / 2.0f);
            rw.Draw(text);

        }

        private void DrawCredits(SFML.Graphics.RenderWindow rw)
        {
            

            SFML.Graphics.Text CreditsText = new SFML.Graphics.Text("Bomberman", font);
            CreditsText.Scale = new SFML.Window.Vector2f(1.5f, 1.5f);
            CreditsText.Position = new SFML.Window.Vector2f(400 - (float)(CreditsText.GetGlobalBounds().Width / 2.0), 20);
            rw.Draw(CreditsText);

            CreditsText = new SFML.Graphics.Text("A Game by", font);
            CreditsText.Scale = new SFML.Window.Vector2f(.75f, 0.75f);
            CreditsText.Position = new SFML.Window.Vector2f(400 - (float)(CreditsText.GetGlobalBounds().Width / 2.0), 100);
            rw.Draw(CreditsText);

            CreditsText = new SFML.Graphics.Text("Simon Weis", font);
            CreditsText.Scale = new SFML.Window.Vector2f(1, 1);
            CreditsText.Position = new SFML.Window.Vector2f(400 - (float)(CreditsText.GetGlobalBounds().Width / 2.0), 135);
            rw.Draw(CreditsText);

            CreditsText = new SFML.Graphics.Text("Visual Studio 2010 \t C# \t SFML.Net 2.1 ", font);
            CreditsText.Scale = new SFML.Window.Vector2f(0.75f, 0.75f);
            CreditsText.Position = new SFML.Window.Vector2f(400 - (float)(CreditsText.GetGlobalBounds().Width / 2.0), 170);
            rw.Draw(CreditsText);

            CreditsText = new SFML.Graphics.Text("aseprite \t LMMS", font);
            CreditsText.Scale = new SFML.Window.Vector2f(0.75f, 0.75f);
            CreditsText.Position = new SFML.Window.Vector2f(400 - (float)(CreditsText.GetGlobalBounds().Width / 2.0), 200);
            rw.Draw(CreditsText);

            CreditsText = new SFML.Graphics.Text("Thanks to", font);
            CreditsText.Scale = new SFML.Window.Vector2f(0.75f, 0.75f);
            CreditsText.Position = new SFML.Window.Vector2f(400 - (float)(CreditsText.GetGlobalBounds().Width / 2.0), 350);
            rw.Draw(CreditsText);

            CreditsText = new SFML.Graphics.Text("Families & Friends for their great support", font);
            CreditsText.Scale = new SFML.Window.Vector2f(0.75f, 0.75f);
            CreditsText.Position = new SFML.Window.Vector2f(400 - (float)(CreditsText.GetGlobalBounds().Width / 2.0), 375);
            rw.Draw(CreditsText);

            CreditsText = new SFML.Graphics.Text("Created Nov 2013", font);
            CreditsText.Scale = new SFML.Window.Vector2f(0.75f, 0.75f);
            CreditsText.Position = new SFML.Window.Vector2f(400 - (float)(CreditsText.GetGlobalBounds().Width / 2.0), 500);
            rw.Draw(CreditsText);


        }

        private void DrawScore(SFML.Graphics.RenderWindow rw)
        {
            GameStats.Draw(rw);
        }



        float timeTilNextInput = 0.0f;

        private void ChangeGameState(State newState, float inputdeadTime = 0.75f)
        {
            this.gameState = newState;
            timeTilNextInput = inputdeadTime;
            
        }

        private void GetInputMenu()
        {
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Return))
            {
                StartGame();
            }

            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.C))
            {
                ChangeGameState(State.Credits);
            }

        }


        private int numberOfPlayers = 2;

        private void StartGame()
        {
            myWorld = new World(numberOfPlayers);
            ChangeGameState(State.Game, 0.1f);
        }

        private void GetInputGame()
        {
            myWorld.GetInput();
        }

        private void GetInputCreditsScore()
        {
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Escape))
            {
                ChangeGameState(State.Menu, 1.0f);
            }
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Return))
            {
                ChangeGameState(State.Menu, 0.5f);
            }
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.C))
            {
                ChangeGameState(State.Menu, 0.5f);
            }
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Space))
            {
                ChangeGameState(State.Menu, 0.5f);
            }

        }







    }
}
