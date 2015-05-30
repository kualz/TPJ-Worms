using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using War_Square.characters;
using War_Square.WeaponsAndProjectiles;

namespace War_Square.Menus
{
    class CharacterChangeMenu
    {
        private Texture2D background, NEXT;
        private SpriteFont font;
        private List<String> options = new List<string>();
        private float opacity = 0;

        public CharacterChangeMenu() { }

        public void load(ContentManager content)
        {
            background = content.Load<Texture2D>("1");
            NEXT = content.Load<Texture2D>("NEXT");
            font = content.Load<SpriteFont>("MyFont");
            options.Add("PRESS ENTER");
            options.Add("WHEN READY");
        }

        public void update(GameTime gameTime, hud Interface, Game1 game)
        {
            opacity += 0.01f;
            if (opacity >= 1f) opacity = 1f;
            if (Input.IsPressed(Keys.Enter)){
                CharactersHandler.ChangeActive();
                magzzz.setAllMag();
                Interface.ResetlRoundTime();
                Collisions.bulletsTagged.Clear();
                game.gameState = Game1.GameState.running;
                game.cameraX = (int)CharactersHandler.getActiveCharacter().CharacterPosition().X;
            }
        }

        public void draw(SpriteBatch spriteBatch, Camera2D cam)    
        {
            spriteBatch.Draw(background, new Rectangle((int)cam.Position.X - 720, (int)cam.Position.Y - 500, 1500, 1000), Color.Black * 0.75f);
            spriteBatch.DrawString(font, options[0], new Vector2(cam.Position.X - 200, cam.Position.Y), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, options[1], new Vector2(cam.Position.X - 200, cam.Position.Y + 50), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, CharactersHandler.getNextPlayer().returnName(), new Vector2(cam.Position.X - 100, cam.Position.Y - 50), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(NEXT, new Vector2(cam.Position.X - 350, cam.Position.Y - 300), Color.White * opacity);
        }
    }
}
