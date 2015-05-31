using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using War_Square.Sounds;

namespace War_Square.Menus
{
    class MapChoose
    {
        private Texture2D[] texture;
        private Texture2D backGround;
        private SpriteFont font;
        private int chosen = 0;
        private int MAP_COUNT = 2;
        private List<String> option;

        public MapChoose() { }

        public void load(ContentManager content)
        {
            option = new List<string>(MAP_COUNT);
            texture = new Texture2D[MAP_COUNT];
            texture[0] = content.Load<Texture2D>("map1");
            texture[1] = content.Load<Texture2D>("map1");
            backGround = content.Load<Texture2D>("backgroundtest1");
            font = content.Load<SpriteFont>("MyFont");
            option.Add("THE VOID");
            option.Add("ASTRAL BRIDGE");
        }

        public void update(GameTime gametime, Game1 game)
        {
            if (Input.IsPressed(Keys.Right))
            {
                SoundManager.playSound("menuswitch");
                chosen++;
                if (chosen >= option.Count) chosen = chosen - option.Count;
            }
            if (Input.IsPressed(Keys.Left))
            {
                SoundManager.playSound("menuswitch");
                chosen--;
                if (chosen < 0) chosen = option.Count - 1;
            }
            if (Input.IsPressed(Keys.Enter))
            {
                SoundManager.playSound("enterselect");
                switch (chosen)
                {
                    case 0:
                        {
                            Game1.SelectedMap = 0;
                            game.gameState = Game1.GameState.running;
                            game.getMap().InitRectMap();
                        }
                        break;
                    case 1:
                        {
                            Game1.SelectedMap = 1;
                            game.gameState = Game1.GameState.running;
                            game.getMap().InitRectMap();
                        }
                        break;
                    case 2:
                        break;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Vector2(-350, 0), Color.White);

            for (int o = 0; o < MAP_COUNT; o++)
            {
                if (o != chosen){
                    spriteBatch.Draw(texture[o], new Vector2(5 + (o * 400), 300), null, Color.White * 0.6f, 0f, Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
                }
                else { 
                    spriteBatch.Draw(texture[o], new Vector2(5 + (o * 400), 300), null, Color.White, 0f, Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
                }
            }

            for (int i = 0; i < option.Count; i++){
                spriteBatch.DrawString(font, option[i], new Vector2(10 + (i * 400), 400), Color.White * 0.8f, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
            }
        }
    }
}
