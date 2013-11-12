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
        }

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
                   Statistic stats = myWorld.EndThisRound();
                   ChangeGameState(State.Score);
                }
            }

        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            if (gameState == State.Menu)
            {
                DrawMenu(rw);
            }
            else if (gameState == State.Game)
            {
                myWorld.Draw(rw);
            }
            else if (gameState == State.Credits)
            {
                DrawCredits(rw);
            }
            else if (gameState == State.Score)
            {
                DrawScore(rw);
            }
        }

        private void DrawMenu(SFML.Graphics.RenderWindow rw)
        {
            SFML.Graphics.Text text = new SFML.Graphics.Text();
            rw.Draw(text);
        }

        private void DrawCredits(SFML.Graphics.RenderWindow rw)
        {
            throw new NotImplementedException();
        }

        private void DrawScore(SFML.Graphics.RenderWindow rw)
        {
            throw new NotImplementedException();
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
                ChangeGameState(State.Menu);
            }
        }







    }
}
