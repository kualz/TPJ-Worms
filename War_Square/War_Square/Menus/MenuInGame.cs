using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private int escolha = 0;
        private List<string> options = new List<string>();


        public void load(ContentManager content)
        {
            textura = content.Load<Texture2D>("1");
            font = content.Load<SpriteFont>("MyFont");
            options.Add("Continuar");
            options.Add("Sair");
        }

        public void update(GameTime gametime, Game1 game)
        {
            if(Input.IsPressed(Keys.Up))
            {
                escolha--;
                if (escolha < 0)
                    escolha = options.Count - 1;
            }
            if (Input.IsPressed(Keys.Down))
            {
                escolha++;
                if (escolha >= options.Count)
                    escolha = 0;
            }
            if(Input.IsPressed(Keys.Enter))
            {
                switch(escolha)
                {
                    case 0:
                        game.gameState = Game1.GameState.running;
                        break;
                    case 1:
                        game.Exit();
                        break;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, new Rectangle(80, 90, 159, 150), Color.BlueViolet);
            for (int i = 0; i < options.Count; i++)
            {
                if (i != escolha)
                    spriteBatch.DrawString(font, options[i], new Vector2(100, 100 + i * 40), Color.White);
                else spriteBatch.DrawString(font, options[i], new Vector2(100, 100 + i * 40), Color.OrangeRed);
            }
        }
    }
}
