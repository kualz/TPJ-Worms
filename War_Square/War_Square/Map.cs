using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace War_Square
{
    class Map
    {
        public const int MAP_WIDTH = 131, MAP_HEIGHT = 36;

        static public List<int[,]> WorldMaps = new List<int[,]>();
        static public List<string> MapList = new List<string>();
        public Texture2D Wall;
        public int[,] mapa1;
        public int[,] mapa2;

        public class Tile
        {
            public Vector2 position;
            public Rectangle collider;
            public Texture2D texture;
            public bool destroy;
        }

        public Tile[,] tiles = new Tile[MAP_HEIGHT, MAP_WIDTH];

        public int[,] loadData(string FilePath, int[,] map)
        {
            string[] fileData;
            try
            {
                fileData = File.ReadAllLines(FilePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Erro ao Carregar " + FilePath);
                throw;
            }
            Console.WriteLine("{0} Carregado com sucesso", FilePath);
            for (int x = 0; x < fileData.Length; x++){
                for (int y = 0; y < fileData[x].Length; y++){
                    if (map == null) map = new int[fileData.Length, fileData[x].Length];
                    int converted = Convert.ToInt32(fileData[x][y]);
                    switch (converted)
                    {
                        case 48:
                            map[x,y] = 0;
                            break;
                        case 49:
                            map[x,y] = 1;
                            break;
                        default:
                            break;
                    }
                }
			}
            Console.Write("");
            return map;
        }

        public void Load(ContentManager content)
        {
            Wall = content.Load<Texture2D>("tile");
            MapList.Add("maps/map1.dat");
            MapList.Add("maps/map2.dat");
            mapa1 = loadData(MapList[0], mapa1);
            mapa2 = loadData(MapList[1], mapa2);
            //alive = new bool[mapa1.GetLength(1), mapa1.GetLength(0)];
            WorldMaps.Add(mapa1);
            WorldMaps.Add(mapa2);
            Console.WriteLine("Map Loaded");
        }

        public void LoadTiles(int mapa)
        {

            for (int y = 0; y < MAP_WIDTH; y++)
            {
                for (int x = 0; x < MAP_HEIGHT; x++)
                {
                    switch (mapa)
                    {
                        case 0:
                            if (mapa1[x, y] == 1)
                            {
                                Tile newTile = new Tile();
                                newTile.position = new Vector2(y * 20, x * 20);
                                newTile.collider = new Rectangle(y * 20, x * 20, 20, 20);
                                newTile.destroy = false;
                                newTile.texture = Wall;
                                Collisions.tilesCollisions.Add(newTile.collider);
                                tiles[x, y] = newTile;
                            }
                            break;

                        case 1:
                            if (mapa2[x, y] == 1)
                            {
                                Tile newTile = new Tile();
                                newTile.position = new Vector2(y * 20, x * 20);
                                newTile.collider = new Rectangle(y * 20, x * 20, 20, 20);
                                newTile.destroy = false;
                                newTile.texture = Wall;
                                Collisions.tilesCollisions.Add(newTile.collider);
                                tiles[x, y] = newTile;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void update(GameTime gametime)
        {
            for (int y = MAP_WIDTH - 1; y >= 0; y--)
            {
                for (int x = MAP_HEIGHT- 1; x >= 0; x--)
                {
                    if (tiles[x,y] != null && tiles[x, y].destroy)
                    {
                        Collisions.tilesCollisions.Remove(tiles[x, y].collider);
                        tiles[x, y] = null;
                    }
                }
            }

            /*for (int i = alive.GetLength(1) - 1; i >= 0; i--)
            {
                for (int j = alive.GetLength(0) - 1; j >= 0; j--)
                {
                    if (!alive[i, j])
                    {
                        //Remove rectangle
                        Collisions.tilesCollisions.Remove();
                    }
                }
            }*/
        }

        public void secondDraw(SpriteBatch spritebatch)
        {
            for (int y = 0; y < MAP_WIDTH; y++)
            {
                for (int x = 0; x < MAP_HEIGHT; x++)
                {
                    Tile tile = tiles[x, y];
                    if(tile != null) spritebatch.Draw(tile.texture, new Rectangle((int)tile.position.X, (int)tile.position.Y, 20 , 20), Color.White);
                }
            }

            /*
            foreach (Rectangle rect in Collisions.tilesCollisions)
            {
                spritebatch.Draw(Wall, new Vector2(rect.X, rect.Y), null, Color.White);
            }int
            //Console.WriteLine("All Tiles Loaded");*/
        }

        public void DestroySquare(Vector2 pos)
        {
            int x = (int)Math.Round(pos.Y / 20);
            int y = (int)Math.Round(pos.X / 20);

            if(tiles[x, y] != null) tiles[x, y].destroy = true;
        }
    }
}
