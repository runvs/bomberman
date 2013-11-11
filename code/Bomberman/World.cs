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

            foreach (Bomb b in bombList)
            {
                b.Update(deltaT);
            }
            foreach (Explosion e in explosionList)
            {
                e.Update(deltaT);
            }


            //  delete the bombs that are done and spawn explosions
            if (bombList.Count() >= 1)
            {
                if(bombList[0] != null && bombList[0].Exploded == true)
                {
                    // TODO: Get Players Bob Strenght
                    Explosion myExplosion = new Explosion(this, bombList[0].BombPosition, Explosion.ExplosionDirection.DirDown, 1, 3);
                    explosionList.Add(myExplosion);
                    myExplosion = new Explosion(this, bombList[0].BombPosition, Explosion.ExplosionDirection.DirUp, 1, 3);
                    explosionList.Add(myExplosion);
                    myExplosion = new Explosion(this, bombList[0].BombPosition, Explosion.ExplosionDirection.DirLeft, 1, 3);
                    explosionList.Add(myExplosion);
                    myExplosion = new Explosion(this, bombList[0].BombPosition, Explosion.ExplosionDirection.DirRight, 1, 3);
                    explosionList.Add(myExplosion);
                    
                    bombList.RemoveAt(0);
                }
            }

            // delete the explosions and spawn new explosions
            if (explosionList.Count() >= 1)
            {
                if (explosionList[0] != null && explosionList[0].ExplosionDone == true)
                {
                    Explosion e = explosionList[0];
                    if (e.Direction == Explosion.ExplosionDirection.DirLeft)
                    {

                        SFML.Window.Vector2i newPos = e.Position;
                        newPos.X--;
                        if (!this.IsTileBlocked(newPos))    // it is a free tile so possibly spawn a new explosion here
                        {
                            if (e.ExplosionNumber < e.ExplosionNumberMax)
                            {
                                Explosion newExplosion = new Explosion(this, newPos, e.Direction, e.ExplosionNumber + 1, e.ExplosionNumberMax);
                                explosionList.Add(newExplosion);
                            }
                        }
                        else // it is no free tile so check if this is a breakable tile
                        {
                            Tile myBreakableAndSoonBrokenTile = null;
                            foreach (Tile t in tileList)
                            {
                                if (t.TilePosition.X == newPos.X && t.TilePosition.Y == newPos.Y)
                                {
                                    if (t.MyTileType == Tile.TileType.TileTypeBreakable)
                                    {
                                        myBreakableAndSoonBrokenTile = t;
                                        break;
                                    }
                                }
                            }
                            if (myBreakableAndSoonBrokenTile != null)
                            {
                                // remove the old breakable Tile && Spawn a new free Tile
                                tileList.Remove(myBreakableAndSoonBrokenTile);

                                Tile freeTile = new Tile(myBreakableAndSoonBrokenTile.TilePosition, Tile.TileType.TileTypeFree);
                                tileList.Add(freeTile);

                            }
                        }
                    }
                    explosionList.RemoveAt(0);
                }
            }

        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            foreach (Tile t in tileList)
            {
                t.Draw(rw);
            }

            foreach (Bomb b in bombList)
            {
                b.Draw(rw);
            }

            foreach (Player p in playerList)
            {
                p.Draw(rw);
            }
            foreach (Explosion e in explosionList)
            {
                e.Draw(rw);
            }



        }


        private void InitGame()
        {
            playerList = new System.Collections.Generic.List<Player>();

            for (int i = 0; i != numberOfPlayers; ++i)
            {
                playerList.Add(new Player(this));
            }



            bombList = new System.Collections.Generic.List<Bomb>();
            explosionList = new System.Collections.Generic.List<Explosion>();



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
                        // TODO Change default Layout to some random Pattern
                        if (i != 0 && i != GameProperties.WorldSizeInTiles() -1 && j != 0 && j != GameProperties.WorldSizeInTiles() -1 )
                        {
                            myTile = new Tile(new SFML.Window.Vector2i(i, j), Tile.TileType.TileTypeBreakable);
                        }
                        else
                        {
                            myTile = new Tile(new SFML.Window.Vector2i(i, j), Tile.TileType.TileTypeFree);
                        }
                    }
                    
                    tileList.Add(myTile);
                }
            }
        }

        public bool IsTileBlocked(SFML.Window.Vector2i pos)
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

            if (ret == false)   // we have still a free tile
            {
                foreach (Bomb b in bombList)
                {
                    if (b.BombPosition.X == pos.X && b.BombPosition.Y == pos.Y)
                    {
                        ret = true;
                        break;
                    }
                }
            }

            return ret;
        }


        private int numberOfPlayers = 1;
        System.Collections.Generic.IList<Player> playerList;
        System.Collections.Generic.IList<Bomb> bombList;
        System.Collections.Generic.IList<Explosion> explosionList;
        

        System.Collections.Generic.IList<Tile> tileList;



        public void SpawnBombOnPosition(SFML.Window.Vector2i pos)
        {
            if (!IsTileBlocked(pos))    // there is a free tile
            {
                Bomb myBomb = new Bomb(this, pos);

                bombList.Add(myBomb);
            }
        }


    }


}
