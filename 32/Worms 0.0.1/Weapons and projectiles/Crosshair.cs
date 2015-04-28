using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1.Weapons_and_projectiles
{
    class Crosshair :IFocusable
    {
        private Texture2D cross;
        private Point mousePos;
        private Vector2 mira;

        public Crosshair()
        {

        }
        public void Load(ContentManager content)
        {
            cross = content.Load<Texture2D>("cross");
        }
        public void update()
        {
            MouseState mState = Mouse.GetState();
            mousePos = mState.Position;
            mira = new Vector2(mousePos.X, mousePos.Y);
        }

        public void draw(SpriteBatch spriteBatch, Vector2 mouse)
        {
            spriteBatch.Draw(cross, mira, Color.White);
        }

        public Vector2 Position
        {
            get { return this.mira; }
        }
        
    }
}
