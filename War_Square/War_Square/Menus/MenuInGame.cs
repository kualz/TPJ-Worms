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
    class MenuInGame
    {
        private Texture2D textura, selected;
        private SpriteFont font;
        private int escolha = 0;
        private List<string> options = new List<string>();
        private Characters GhostCharacter;


        public void load(ContentManager content)
        {
            textura = content.Load<Texture2D>("1");
            font = content.Load<SpriteFont>("MyFont");
            options.Add("Continuar");
            options.Add("Menu");
            options.Add("Sair");
            selected = content.Load<Texture2D>("selected");
        }

        public void update(GameTime gametime, Game1 game, ContentManager content)
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
                        {
                            game.gameState = Game1.GameState.Menu;
                            CharactersHandler.Players.Clear();
                            MenuCharacterChoose.resetArrays(content);
                            Collisions.reset();
                            game.getMap().InitRectMap();
                            Game1.firstEntry = false;
                        }
                        break;
                    case 2:
                        game.Exit();
                        break;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch, Characters Char, Camera2D cam)
        {
            spriteBatch.Draw(textura, new Rectangle((int)cam.Position.X - 720, (int)cam.Position.Y - 500, 1500, 1000), Color.Black* 0.3f);
            for (int i = 0; i < options.Count; i++)
            {
                if (i != escolha) spriteBatch.DrawString(font, options[i], new Vector2(cam.Position.X - 300, 100 + i * 40), Color.Silver, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
                else{
                    spriteBatch.Draw(selected, new Vector2(cam.Position.X - 325, 100 + i * 40), Color.White);
                    spriteBatch.DrawString(font, options[i], new Vector2(cam.Position.X - 300, 100 + i * 40), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}