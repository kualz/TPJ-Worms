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
    class MenuCharacterChoose
    {
        private List<String> options = new List<string>();
        private SpriteFont font;
        private Texture2D textura;
        private Texture2D squareSelected;
        private int chooseOption = 0;

        public MenuCharacterChoose() { }

        public void load(ContentManager content)
        {
            font = content.Load<SpriteFont>("MyFont");
            options.Add("Phaktumn");
            options.Add("Kualz");
            options.Add("Klipper");
            options.Add("Saber");
            options.Add("Zjeh");
            //imagens dos characters!
            squareSelected = content.Load<Texture2D>("1");
            textura = content.Load<Texture2D>("character");
        }

        public void Update(GameTime gametime, Game1 game)
        {
            if(Input.IsPressed(Keys.Right))
            {
                chooseOption++;
                if (chooseOption >= options.Count)
                    chooseOption = chooseOption - options.Count;
            }
            if(Input.IsPressed(Keys.Left))
            {
                chooseOption--;
                if (chooseOption <= 0)
                    chooseOption = options.Count;
            }
            if(Input.IsPressed(Keys.Enter))
            {
                switch(chooseOption)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, options[0], new Vector2(90, 25), Color.White);
            spriteBatch.DrawString(font, options[1], new Vector2(200, 25), Color.White);
            spriteBatch.DrawString(font, options[2], new Vector2(300, 25), Color.White);
            spriteBatch.DrawString(font, options[3], new Vector2(400, 25), Color.White);
            spriteBatch.DrawString(font, options[4], new Vector2(500, 25), Color.White);
            for (int i = 0; i < options.Count; i++)
            {
                if (i != chooseOption)
                    spriteBatch.Draw(textura, new Vector2(90 + i * 100, 55), new Rectangle(0, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                else
                {
                    spriteBatch.Draw(squareSelected, new Rectangle(90 + i * 100, 55, 60, 75), Color.BlueViolet);
                    spriteBatch.Draw(textura, new Vector2(90 + i * 100, 49), new Rectangle(0, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
