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
using Microsoft.Xna.Framework.Audio;


namespace War_Square.Menus
{

    class MenuPrincipal
    {
        public List<string> Options = new List<string>();
        private int selectedOption = 0;
        private SpriteFont spriteFont;
        private Texture2D texture, backGround, selected, unselected, nameSprite;
        private float opacity, deltaTime, totalTime;

        public MenuPrincipal()
        { }

        public void load(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("MyFont");
            Options.Add("Start New Game");
            Options.Add("Options");
            Options.Add("Exit");
            texture = content.Load<Texture2D>("1");
            backGround = content.Load<Texture2D>("MenuTest");
            selected = content.Load<Texture2D>("Selected");
            unselected = content.Load<Texture2D>("unselected");
            nameSprite = content.Load<Texture2D>("WarSquareteste3");
        }

        public void update(GameTime gameTime, Game1 game)
        {
            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (deltaTime < 0.5f){
                opacity -= 0.04f;
                if (opacity < 0.1f)
                    opacity = 0.1f;
            }
            if (deltaTime > 0.5f && deltaTime < 1f){
                opacity += 0.035f;
            }
            if (deltaTime > 1f) deltaTime = 0f;

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
            if (Input.IsPressed(Keys.Enter))
            {
                switch (selectedOption)
                {
                        //'e importante nao dar reset aqui logo da lista de players
                        //in game
                        //caso isso aconteca aqui basicamente ao correr o draw do menu inicial da erro
                        //o reset dos players escolhidos acontece quando das input do enter
                        //para passar do menu principal para o menu de escolheres as personagens
                    case 0:
                        {
                            game.gameState = Game1.GameState.CharacterChoose;
                            CharactersHandler.resetPlayersList(Game1.firstEntry);
                        
                        }
                        break;
                    case 1:
                        game.gameState = Game1.GameState.Options;
                        break;
                    case 2: game.Exit();
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Vector2(-350, 0), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(nameSprite, new Vector2(90, 225), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(texture, new Rectangle(-125, 80, 425, 150), Color.DarkSlateGray);
            spriteBatch.DrawString(spriteFont, "Version 0.9 Phaktumn Kualz Klipper", new Vector2(500, 650), Color.White);
            for (int i = 0; i < Options.Count; i++)
            {
                if (selectedOption != i)
                {
                    //spriteBatch.Draw(unselected, new Rectangle(90, 97 + i * 40, 200, 30), Color.White);
                    spriteBatch.DrawString(spriteFont, Options[i], new Vector2(500, 300 + i * 40), Color.DarkGray);
                }
                else
                {
                    spriteBatch.Draw(selected, new Rectangle(480, 290 + i * 40, 250, 45),null , Color.White * opacity, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(spriteFont, Options[i], new Vector2(500, 300 + i * 40), Color.White, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
