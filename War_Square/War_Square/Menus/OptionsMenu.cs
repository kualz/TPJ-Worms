using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using War_Square.WeaponsAndProjectiles;
using War_Square.characters;
using War_Square.Menus;

namespace War_Square.Menus
{
    class OptionsMenu
    {
        private List<string> Options = new List<string>();
        private int selectedOption;
        private SpriteFont spriteFont;
        private Texture2D texture;

        public OptionsMenu()
        { }

        public void Load(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("MyFont");
            texture = content.Load<Texture2D>("1");
            //escolher niveis
            //e stuff do som se for necessario!
            //e mais coisas ta tudo no txt! ate se carregava de la XD
            Options.Add("Volume");
            Options.Add("Back");
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            if (Input.IsPressed(Keys.Down))
            {
                selectedOption++;
                if (selectedOption >= Options.Count)
                    selectedOption = 0;
            }
            if (Input.IsPressed(Keys.Up))
            {
                selectedOption--;
                if (selectedOption < 0)
                    selectedOption = Options.Count;
            }
            if (Input.IsPressed(Keys.Enter))
            {
                //numero de casos necessarios!
                switch (selectedOption)
                {
                    case 0: 
                        break;
                    case 1:
                        game.gameState = Game1.GameState.Menu;
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(80, 80, 190, 150), Color.CadetBlue);
            for (int i = 0; i < Options.Count; i++)
            {
                if (selectedOption != i)
                    spriteBatch.DrawString(spriteFont, Options[i], new Vector2(100, 100 + i * 40), Color.White);
                else spriteBatch.DrawString(spriteFont, Options[i], new Vector2(100, 100 + i * 40), Color.Orange);
            }
        }
    }
}
