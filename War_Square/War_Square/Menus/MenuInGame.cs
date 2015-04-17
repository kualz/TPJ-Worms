using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.Menus
{
    class MenuInGame
    {
        private Texture2D textura;
        private SpriteFont font;
        private int escolha;
        private List<string> options = new List<string>();


        public void load(ContentManager content)
        {
            textura = content.Load<Texture2D>("1");
            font = content.Load<SpriteFont>("MyFont");

        }

        public void update(GameTime gametime)
        {

        }

        public void draw(SpriteBatch spriteBatch)
        {

        }
    }
}
