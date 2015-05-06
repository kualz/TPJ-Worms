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
    class MapChoose
    {
        private Texture2D[] texture;
        private SpriteFont font;
        private int chosen = 0;
        private List<String> option = new List<string>();

        public MapChoose() { }

        public void load(ContentManager content)
        {
            texture = new Texture2D[2];
            texture[0] = content.Load<Texture2D>("1");
            texture[1] = content.Load<Texture2D>("1");
            font = content.Load<SpriteFont>("MyFont");
            option.Add("Map1");
            option.Add("Map2");
            option.Add("Map3");
        }

        public void update(GameTime gametime, Game1 game)
        {
            if (Input.IsPressed(Keys.Right))
            {
                chosen++;
                if (chosen >= option.Count) chosen = chosen - option.Count;
            }
            if (Input.IsPressed(Keys.Left))
            {
                chosen--;
                if (chosen < 0) chosen = option.Count - 1;
            }
            if (Input.IsPressed(Keys.Enter))
            {
                switch (chosen)
                {
                    case 0:
                        {
                            Game1.SelectedMap = 0;
                            game.gameState = Game1.GameState.running;
                        }
                        break;
                    case 1:
                        {
                            Game1.SelectedMap = 1;
                            game.gameState = Game1.GameState.running;
                        }
                        break;
                    case 2:
                        break;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, option[0], new Vector2(90 , 250), Color.White);
            spriteBatch.DrawString(font, option[1], new Vector2(180, 250), Color.White);

            for (int i = 0; i < option.Count; i++)
            {
                if (i != chosen)
                    spriteBatch.Draw(texture[1], new Vector2(90 + i * 100, 150), new Rectangle(0, 0, 60, 80), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                else spriteBatch.Draw(texture[1], new Vector2(90 + i * 100, 150), new Rectangle(0, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
