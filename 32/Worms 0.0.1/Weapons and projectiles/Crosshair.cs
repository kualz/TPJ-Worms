﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1.Weapons_and_projectiles
{
    class Crosshair
    {
        private Texture2D cross;

        public Crosshair()
        {

        }
        public void Load(ContentManager content)
        {
            cross = content.Load<Texture2D>("cross");
        }

        public void draw(SpriteBatch spriteBatch, Vector2 mouse)
        {
            spriteBatch.Draw(cross, mouse, Color.White);
        }
    }
}