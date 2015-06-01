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
        public Texture2D[] Wall;
        public int[,] mapa1;
        public int[,] mapa2;


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
                        case 48: map[x,y] = 0;
                            break;
                        case 49: map[x,y] = 1;
                            break;
                        case 50: map[x, y] = 2;
                            break;
                        case 51: map[x, y] = 3;
                            break;
                        case 52: map[x, y] = 4;
                            break;
                        case 53: map[x, y] = 5;
                            break;
                        case 54: map[x, y] = 6;
                            break;
                        case 55: map[x, y] = 7;
                            break;
                        case 56: map[x, y] = 8;
                            break;
                        case 57: map[x, y] = 9;
                            break;
                        default:
                            break;
                    }
                }
			}
            return map;
        }

        public void Load(ContentManager content)
        {
            Wall = new Texture2D[9];
            Wall[0] = content.Load<Texture2D>("TileCentro");
            Wall[1] = content.Load<Texture2D>("cima");
            Wall[2] = content.Load<Texture2D>("cimadireita");
            Wall[3] = content.Load<Texture2D>("direita");
            Wall[4] = content.Load<Texture2D>("baixoDireitaa");
            Wall[5] = content.Load<Texture2D>("baixo");
            Wall[6] = content.Load<Texture2D>("baixoDireita");
            Wall[7] = content.Load<Texture2D>("esquerda");
            Wall[8] = content.Load<Texture2D>("cimaesquerda");
            MapList.Add("maps/map1.dat");
            MapList.Add("maps/map2.dat");
            mapa1 = loadData(MapList[0], mapa1);
            mapa2 = loadData(MapList[1], mapa2);
            WorldMaps.Add(mapa1);
            WorldMaps.Add(mapa2);
            Console.WriteLine("Map Loaded");
        }

        public void update(GameTime gametime)
        {

        }

        public void secondDraw(SpriteBatch spritebatch)
        {
            int[,] aux = WorldMaps[Game1.SelectedMap];
            foreach (Rectangle rect in Collisions.tilesCollisions){
                if (aux[rect.Y / 20, rect.X / 20] == 1) { spritebatch.Draw(Wall[0], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
                if (aux[rect.Y / 20, rect.X / 20] == 2) { spritebatch.Draw(Wall[1], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
                if (aux[rect.Y / 20, rect.X / 20] == 3) { spritebatch.Draw(Wall[2], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
                if (aux[rect.Y / 20, rect.X / 20] == 4) { spritebatch.Draw(Wall[3], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
                if (aux[rect.Y / 20, rect.X / 20] == 5) { spritebatch.Draw(Wall[4], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
                if (aux[rect.Y / 20, rect.X / 20] == 6) { spritebatch.Draw(Wall[5], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
                if (aux[rect.Y / 20, rect.X / 20] == 7) { spritebatch.Draw(Wall[6], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
                if (aux[rect.Y / 20, rect.X / 20] == 8) { spritebatch.Draw(Wall[7], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
                if (aux[rect.Y / 20, rect.X / 20] == 9) { spritebatch.Draw(Wall[8], new Rectangle(rect.X, rect.Y, 20, 20), null, Color.White); }
            }       
            //Console.WriteLine("All Tiles Loaded");
        }


        public void InitRectMap()
        {
            int[,] aux = WorldMaps[Game1.SelectedMap];
            for (int y = 0; y < WorldMaps[Game1.SelectedMap].GetLength(0); y++)
            {
                for (int x = 0; x < WorldMaps[Game1.SelectedMap].GetLength(1); x++)
                {
                    if (aux[y, x] == 1 || aux[y, x] == 2 || aux[y, x] == 3 || aux[y, x] == 4 || aux[y, x] == 5 || aux[y, x] == 6 || aux[y, x] == 7 || aux[y, x] == 8 || aux[y, x] == 9)
                    {
                        Rectangle rect = new Rectangle(x * 20,y * 20, 20, 20);
                        Collisions.tilesCollisions.Add(rect);
                    }
                }
            }
            Console.WriteLine("Colisoes Mapa: " + Game1.SelectedMap + " - carregadas com sucesso");
        }

        public void UpdateMapRect()
        {
            Collisions.tilesCollisions.Clear();
            InitRectMap();
            Console.WriteLine("Tile Updated");
        }

        public void DestroySquare(Vector2 pos)
        {
            if (Game1.SelectedMap == 1)
                mapa1[(int)Math.Round(pos.Y / 20), (int)Math.Round(pos.X / 20)] = 0;
            if (Game1.SelectedMap == 2)
                mapa2[(int)Math.Round(pos.Y / 20), (int)Math.Round(pos.X / 20)] = 0;
            UpdateMapRect();
        }
    }
}
