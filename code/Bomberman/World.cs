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
            foreach (Tile t in tileList)
            {
                t.Draw(rw);
            }

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

            tileList = new System.Collections.Generic.List<Tile>();

            for (int i = 0; i != GameProperties.WorldSizeInTiles(); ++i)
            {
                for (int j = 0; j != GameProperties.WorldSizeInTiles(); ++j)
                {
                    Tile myTile;
                    if (i % 2 == 1 && j % 2 == 1)
                    {
                        myTile = new Tile(new SFML.Window.Vector2i(i, j), Tile.TileType.TileTypeWall);
                    }
                    else
                    {
                        myTile = new Tile(new SFML.Window.Vector2i(i, j), Tile.TileType.TileTypeFree);
                    }
                    
                    tileList.Add(myTile);
                }
            }
        }

        bool IsTileBlocked(SFML.Window.Vector2i pos)
        {
            bool ret = false;

            foreach (Tile t in tileList)
            {
                if (t.TilePosition.X == pos.X && t.TilePosition.Y == pos.Y)
                {
                    ret = t.IsTileBlocked();
                    break;
                }
            }
            return ret;
        }

        System.Collections.Generic.IList<Player> playerList;
        private int numberOfPlayers = 1;

        System.Collections.Generic.IList<Tile> tileList;


    }

}
