using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using War_Square.characters;

namespace War_Square.Menus
{
    class WinMenu
    {
        private List<string> Options = new List<string>();
        private SpriteFont spriteFont;
        private Texture2D backGround, WIN, NER;
        private Vector2 WINpos, NERpos;
        private float deltaTime;
        private bool inAnimation = true;

        public WinMenu() { }

        public void load(ContentManager content, Camera2D cam)
        {
            spriteFont = content.Load<SpriteFont>("MyFont");
            backGround = content.Load<Texture2D>("1");
            WIN = content.Load<Texture2D>("WIN");
            NER = content.Load<Texture2D>("NER");
            Options.Add("PRESS ENTER");
        }

        public void loadInGame(Camera2D cam)
        {
            NERpos = new Vector2(cam.Position.X + 1000, cam.Position.Y);
            WINpos = new Vector2(cam.Position.X - 1400, cam.Position.Y);
            Console.WriteLine("{0}{1}", NERpos.X, NERpos.Y);
        }

        public void unload()
        {
            WIN.Dispose();
            NER.Dispose();
            backGround.Dispose();
        }

        public void update(GameTime gameTime, Game1 game, ContentManager content, Camera2D cam)
        {
            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (WINpos.X <= cam.Position.X - 350 && NERpos.X > cam.Position.X)
            {
                inAnimation = true;
                WINpos = new Vector2(WINpos.X + 10, WINpos.Y);
                NERpos = new Vector2(NERpos.X - 10, NERpos.Y);
            }
            else inAnimation = false;

            if (Input.IsPressed(Keys.Enter) && inAnimation == false)
            {
                CharactersHandler.Players.Clear();
                Collisions.reset();
                Game1.firstEntry = false;
                MenuCharacterChoose.resetArrays(content);
                MenuCharacterChoose.resetPlayerInserted();
                CharactersHandler.JogadorActivo = 0;
                CharactersHandler.initialPlayer = 0;
                game.gameState = Game1.GameState.Menu;
            }
        }

        public void draw(SpriteBatch spriteBatch, Camera2D cam)
        {
            spriteBatch.Draw(WIN, WINpos, Color.White);
            spriteBatch.Draw(NER, NERpos, Color.White);
            spriteBatch.Draw(backGround, new Rectangle((int)cam.Position.X - 720, (int)cam.Position.Y - 500, 1500, 1000), Color.Black * 0.5f);
            spriteBatch.DrawString(spriteFont, CharactersHandler.getWinner().returnName(), new Vector2(cam.Position.X - 25, cam.Position.Y - 50), Color.Red);
            if (!inAnimation){
                spriteBatch.DrawString(spriteFont, Options[0], new Vector2(cam.Position.X - 75, cam.Position.Y), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
        }
    }
}
