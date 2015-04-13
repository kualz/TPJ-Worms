using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Worms_0._0._1
{
    class ProjectilesHandler : Weapons
    {
        protected float speed;
        protected Vector2 position;
        protected Texture2D texturas;
        private List<string> texturas1 = new List<string>();
        private Vector2 Direction;

        protected enum AmmoType
        {
            cal22,
            chell,
            nade,
            rocket
        }
        protected AmmoType ammotype;

        public ProjectilesHandler()
        {

        }

        public void load(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void draw(SpriteBatch spriteBatch)
        {

        }
    }
}
