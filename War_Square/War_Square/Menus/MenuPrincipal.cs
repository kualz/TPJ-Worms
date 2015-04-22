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
    class MenuPrincipal
    {
        public List<string> Options = new List<string>();
        private int selectedOption = 0;
        private SpriteFont spriteFont;
        private Texture2D texture;

        public MenuPrincipal()
        { }

        public void load(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("MyFont");
            Options.Add("Start New Game");
            Options.Add("Exit");
            texture = content.Load<Texture2D>("1");
        }

        public void update(GameTime gameTime, Game1 game)
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
                    selectedOption = Options.Count - 1;
            }
            if (Input.IsDown(Keys.Enter))
            {
                switch (selectedOption)
                {
                    case 0:
                        game.gameState = Game1.GameState.CharacterChoose;
                        break;
                    case 1: game.Exit();
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
                    spriteBatch.DrawString(spriteFont, Options[i], new Vector2(100, 100 + i * 40), Color.Black);
                else spriteBatch.DrawString(spriteFont, Options[i], new Vector2(100, 100 + i * 40), Color.Orange);
            }
        }
    }
}
