using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square
{
    class Map
    {
        static int SelectedMap;
        static public List<byte[,]> WorldMaps = new List<byte[,]>();
        public Texture2D Wall;
        public byte[,] mapa1 = {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1,1,1,1,1,1,1,1,1}, 
                               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,1}, 
                               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,1},
                               { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,1},
                               { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,1}, 
                               { 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,1},
                               { 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,1},
                               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1,1,1,1,1,1,1,1,1}};


        public void Load(ContentManager content)
        {
            WorldMaps.Add(mapa1);
            Wall = content.Load<Texture2D>("Hand Painted Metal_Diffuse");
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
            for (int y = 0; y < mapa1.GetLength(0); y++)
            {
                for (int x = 0; x < mapa1.GetLength(1); x++)
                {
                    if (mapa1[y, x] == 1)
                    {
                        Rectangle rect = new Rectangle(300 + x * 20, 300 + y * 20, 20, 20);
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
