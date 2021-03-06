﻿using Microsoft.Xna.Framework;
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
using War_Square.Sounds;
using Microsoft.Xna.Framework.Media;

namespace War_Square.Menus
{
    class OptionsMenu
    {
        private List<string> Options = new List<string>();
        private int selectedOption;
        private SpriteFont spriteFont;
        private float deltaTime, totalTime, opacity;
        private Texture2D texture, backGround, selected, nameSprite;

        public OptionsMenu()
        { }

        public void Load(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("MyFont");
            texture = content.Load<Texture2D>("1");
            backGround = content.Load<Texture2D>("MenuTest");
            //escolher niveis
            //e stuff do som se for necessario!
            //e mais coisas ta tudo no txt! ate se carregava de la XD
            Options.Add("Volume");
            Options.Add("Back");
            selected = content.Load<Texture2D>("Selected");
            nameSprite = content.Load<Texture2D>("WarSquare");
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (deltaTime < 0.5f)
            {
                opacity -= 0.05f;
                if (opacity < 0.3f)
                    opacity = 0.3f;
            }
            if (deltaTime > 0.5f && deltaTime < 0.75f)
            {
                opacity += 0.05f;
                if (opacity > 1f)
                    opacity = 1f;
            }
            if (deltaTime >= 1f) deltaTime = 0f;

            if (Input.IsPressed(Keys.Down))
            {
                SoundManager.playSound("menuswitch");
                selectedOption++;
                if (selectedOption >= Options.Count)
                    selectedOption = 0;
            }
            if (Input.IsPressed(Keys.Up))
            {
                SoundManager.playSound("menuswitch");
                selectedOption--;
                if (selectedOption < 0)
                    selectedOption = Options.Count - 1;
            }
            if (selectedOption == 0 && Input.IsPressed(Keys.Left))
            {
                MediaPlayer.Volume -= 0.1f;
                if (MediaPlayer.Volume <= 0f)
                    MediaPlayer.Volume = 0;
            }
            if (selectedOption == 0 && Input.IsPressed(Keys.Right))
            {
                MediaPlayer.Volume += 0.1f;
                if (MediaPlayer.Volume >= 1f)
                    MediaPlayer.Volume = 1f;
            }

            if (Input.IsPressed(Keys.Enter))
            {
                SoundManager.playSound("enterselect");
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
            spriteBatch.Draw(backGround, new Vector2(-350, 0), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            for (int i = 0; i < Options.Count; i++)
            {
                if (selectedOption != i)
                    spriteBatch.DrawString(spriteFont, Options[i], new Vector2(100, 100 + i * 40), Color.DarkGray);
                else
                {
                    if (selectedOption == 0)
                    {
                        spriteBatch.Draw(texture, new Rectangle(450, 100, (int)(MediaPlayer.Volume * 150), 25), Color.White);
                        spriteBatch.DrawString(spriteFont, "-", new Vector2(70, 100), Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                        spriteBatch.DrawString(spriteFont, "+", new Vector2(325, 100), Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                    }
                    spriteBatch.Draw(selected, new Rectangle(80, 90 + i * 40, 250, 45), null, Color.White * opacity, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(spriteFont, Options[i], new Vector2(100, 100 + i * 40), Color.White);
                }
            }
        }
    }
}

