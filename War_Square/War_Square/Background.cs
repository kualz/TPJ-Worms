using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square
{
    class Background
    {
        Texture2D textura;
        Vector2 position; 

        public Background()
        {
            position = new Vector2(-400, -200);
        }

        public void Load(ContentManager content)
        {
            textura = content.Load<Texture2D>("Backgroundteste.jpg");
        }

        public void Update(GameTime gametime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) position.X+=0.5f;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) position.X-=0.5f;
        }
        
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(textura, position, null ,Color.White, 0f, Vector2.Zero, 2f,SpriteEffects.None, 0f);
        }
    }
}
