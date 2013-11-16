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


        private int numberOfPlayers = 1;
        System.Collections.Generic.IList<Player> playerList;
        System.Collections.Generic.IList<Bomb> bombList;
        System.Collections.Generic.List<Explosion> explosionList;
        System.Collections.Generic.IList<Tile> tileList;
        System.Collections.Generic.List<PowerUp> powerupList;


        public void GetInput()
        {
            foreach (Player p in playerList)
            {
                if (!p.IsDead)
                {
                    p.GetInput();
                }
            }
        }

        public void Update(float deltaT)
        {
            foreach (Player p in playerList)
            {
                if (!p.IsDead)
                {
                    p.Update(deltaT);
                    CheckPlayerIsInExplosion(p);
                    CheckPlayerPickedUpPowerUp(p);
                }

            }

            foreach (Bomb b in bombList)
            {
                b.Update(deltaT);
            }
            foreach (Explosion e in explosionList)
            {
                e.Update(deltaT);
            }


            CheckBombsForExplosions();

            CheckExplosionsForProgress();

            CheckPlayersAlive();
        }

        private void CheckPlayerPickedUpPowerUp(Player p)
        {
            foreach (PowerUp up in powerupList)
            {
                if (p.PositionInTiles.X == up.PositionInTiles.X && p.PositionInTiles.Y == up.PositionInTiles.Y)
                {
                    p.PickUpPowerUp(up.Type);
                    up.Picked = true;
                }
            }

            powerupList.RemoveAll(up => up.Picked == true);

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

            foreach (PowerUp p in powerupList)
            {
                p.Draw(rw);
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



        private void CheckPlayerIsInExplosion(Player p)
        {
            SFML.Window.Vector2i playerPositionInTiles = p.PositionInTiles;

            foreach (Explosion e in explosionList)
            {
                if (e.PositionInTiles.X == playerPositionInTiles.X && e.PositionInTiles.Y == playerPositionInTiles.Y)
                {
                    p.Die();

                    // update kill counter 
                    break;  // you cannot die twice
                }
            }

        }

        public int NumberOfPlayersAlive {get; private set;}

        private void CheckPlayersAlive()
        {
            // get new number of alive Players
            NumberOfPlayersAlive = 0;
            foreach (Player p in playerList)
            {
                if (!p.IsDead)
                {
                    NumberOfPlayersAlive++;
                }
            }
        }

        

        


        private void InitGame()
        {
            playerList = new System.Collections.Generic.List<Player>();

            for (int i = 1; i != numberOfPlayers+1; ++i)
            {
                playerList.Add(new Player(this,i));
            }



            bombList = new System.Collections.Generic.List<Bomb>();
            explosionList = new System.Collections.Generic.List<Explosion>();
            powerupList = new List<PowerUp>();


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

            if (pos.X < 0 || pos.Y < 0 || pos.X >= GameProperties.WorldSizeInTiles() || pos.Y >= GameProperties.WorldSizeInTiles())
            {
                return true;
            }


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
                    if (b.BombPositionInTiles.X == pos.X && b.BombPositionInTiles.Y == pos.Y)
                    {
                        ret = true;
                        break;
                    }
                }
            }

            return ret;
        }






        public void SpawnBombOnPosition(SFML.Window.Vector2i pos, Player player)
        {
            if (!IsTileBlocked(pos))    // there is a free tile
            {
                Bomb myBomb = new Bomb(this, pos, player);

                bombList.Add(myBomb);
            }
        }

        private void CheckExplosionsForProgress()
        {
            // delete the explosions and spawn new explosions
            if (explosionList.Count() >= 1)
            {
                List<Explosion> temporaryListForNewExplosions = new List<Explosion>();
                explosionList.ForEach(delegate(Explosion e)
                {
                    if (e.HasSpawnedOtherExplosions == false)
                    {
                       e.HasSpawnedOtherExplosions = true;
                        //Explosion e = explosionList[0];
                        //   Explosion e = explosionList[0];
                        SFML.Window.Vector2i newPos = e.PositionInTiles;
                        if (e.Direction == Explosion.ExplosionDirection.DirLeft)
                            newPos.X--;
                        else if (e.Direction == Explosion.ExplosionDirection.DirRight)
                            newPos.X++;
                        else if (e.Direction == Explosion.ExplosionDirection.DirUp)
                            newPos.Y--;
                        if (e.Direction == Explosion.ExplosionDirection.DirDown)
                            newPos.Y++;
                        if (e.ExplosionNumber <= e.ExplosionNumberMax)
                        {
                            if (!this.IsTileBlocked(newPos))    // it is a free tile so possibly spawn a new explosion here
                            {
                                Explosion newExplosion = new Explosion(this, newPos, e.Direction, e.ExplosionNumber + 1, e.ExplosionNumberMax, e.Owner);
                                temporaryListForNewExplosions.Add(newExplosion);
                                foreach (PowerUp up in powerupList)
                                {
                                    if (up.PositionInTiles.X == newPos.X && up.PositionInTiles.Y == newPos.Y)
                                    {
                                        up.Picked = true;
                                    }
                                }

                                powerupList.RemoveAll(up => up.Picked == true);

                            }

                            else // it is no free tile so check if this is a breakable tile
                            {

                                // check if there is a bomb on this tile
                                foreach (Bomb b in bombList)
                                {
                                    if (b.BombPositionInTiles.X == newPos.X && b.BombPositionInTiles.Y == newPos.Y)
                                    {
                                        b.Explode();
                                        break;
                                    }
                                }

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
                                    CheckSpawnPowerUp(freeTile);
                                    tileList.Add(freeTile);

                                }
                            }
                        }
                    }
                });

                
                
                explosionList.RemoveAll(e => e.ExplosionDone == true);  // remove old explosions;
                foreach (Explosion newExplosion in temporaryListForNewExplosions)
                {
                    explosionList.Add(newExplosion);
                }


            }
        }

        System.Random myRandomGenerator = new System.Random();

        private void CheckSpawnPowerUp(Tile freeTile)
        {
            if (myRandomGenerator.NextDouble() <= GameProperties.PowerUpSpawnProbability())
            {
                PowerUp up = new PowerUp(freeTile.TilePosition);
                powerupList.Add(up);
            }

        }

        private void CheckBombsForExplosions()
        {
            //  delete the bombs that are done and spawn explosions
            if (bombList.Count() >= 1)
            {
                if (bombList[0] != null && bombList[0].Exploded == true)
                {
                    // TODO: Get Players Bob Strenght
                    Explosion myExplosion = new Explosion(this, bombList[0].BombPositionInTiles, Explosion.ExplosionDirection.DirDown, 1, bombList[0].Owner.BombStrenght, bombList[0].Owner);
                    explosionList.Add(myExplosion);
                    myExplosion = new Explosion(this, bombList[0].BombPositionInTiles, Explosion.ExplosionDirection.DirUp, 1, bombList[0].Owner.BombStrenght, bombList[0].Owner);
                    explosionList.Add(myExplosion);
                    myExplosion = new Explosion(this, bombList[0].BombPositionInTiles, Explosion.ExplosionDirection.DirLeft, 1, bombList[0].Owner.BombStrenght, bombList[0].Owner);
                    explosionList.Add(myExplosion);
                    myExplosion = new Explosion(this, bombList[0].BombPositionInTiles, Explosion.ExplosionDirection.DirRight, 1, bombList[0].Owner.BombStrenght, bombList[0].Owner);
                    explosionList.Add(myExplosion);

                    bombList.RemoveAt(0);
                }
            }
        }



        public Statistic EndThisRound()
        {
            Statistic ret = new Statistic();

            return ret;
        }
    }


}
