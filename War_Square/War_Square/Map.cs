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
        static public List<int[,]> WorldMaps = new List<int[,]>();
        static public List<string> MapList = new List<string>();
        public Texture2D Wall;
        public int[,] mapa1;
        public int[,] mapa2;


        public int[,] loadData(string FilePath, int[,] map)
        {
            string[] fileData = File.ReadAllLines(FilePath);
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
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
			}
            return map;
        }

        public void Load(ContentManager content)
        {
            Wall = content.Load<Texture2D>("Hand Painted Metal_Diffuse");
            MapList.Add("maps/map1.dat");
            MapList.Add("maps/map2.dat");
            mapa1 = loadData(MapList[0], mapa1);
            mapa2 = loadData(MapList[1], mapa2);
            WorldMaps.Add(mapa1);
            WorldMaps.Add(mapa2);
        }

        public void update(GameTime gametime)
        {

        }

        public void secondDraw(SpriteBatch spritebatch)
        {
            foreach (Rectangle rect in Collisions.tilesCollisions)
            {
                spritebatch.Draw(Wall, new Rectangle(rect.X, rect.Y, 20, 20), new Rectangle(230, 500, 50, 50), Color.White);
            }
        }


        public void InitRectMap()
        {
            int[,] aux = WorldMaps[Game1.SelectedMap];
            for (int y = 0; y < WorldMaps[Game1.SelectedMap].GetLength(0); y++)
            {
                for (int x = 0; x < WorldMaps[Game1.SelectedMap].GetLength(1); x++)
                {
                    if (aux[y,x] == 1)
                    {
                        Rectangle rect = new Rectangle(x * 20,y * 20, 20, 20);
                        Collisions.tilesCollisions.Add(rect);
                    }
                }
            }
        }

        public void UpdateMapRect()
        {
            Collisions.tilesCollisions.Clear();
            InitRectMap();
        }

        public void DestroySquare(Vector2 pos)
        {
            mapa1[(int)Math.Round(pos.Y / 20), (int)Math.Round(pos.X / 20)] = 0;
            UpdateMapRect();
        }
    }
}
