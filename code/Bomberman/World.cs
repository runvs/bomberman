using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class World
    {

        public World(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;

            InitGame();
        }


        public void GetInput()
        {
            foreach (Player p in playerList)
            {
                p.GetInput();
            }
        }

        public void Update(float deltaT)
        {
            foreach (Player p in playerList)
            {
                p.Update(deltaT);
            }
        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            foreach (Player p in playerList)
            {
                p.Draw(rw);
            }
        }


        private void InitGame()
        {
            playerList = new System.Collections.Generic.List<Player>();

            for (int i = 0; i != numberOfPlayers; ++i)
            {
                playerList.Add(new Player());
            }

            

        }

        System.Collections.Generic.IList<Player> playerList;

        private int numberOfPlayers = 1;

    }

}
