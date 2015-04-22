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
    class MenuCharacterChoose
    {
        private List<String> options = new List<string>();
        private SpriteFont font;
        private Texture2D textura;
        private Texture2D squareSelected;
        private float timer = 0;
        private int chooseOption = 0, characterChoosen = 0;
        private Characters Player1, Player2, Player3, Player4, Player5;

        public MenuCharacterChoose() { }

        public void load(ContentManager content)
        {
            Player1 = CharactersHandler.getCharacter(0);
            Player2 = CharactersHandler.getCharacter(1);
            Player3 = CharactersHandler.getCharacter(2);
            Player4 = CharactersHandler.getCharacter(3);
            Player5 = CharactersHandler.getCharacter(4);
            Player1.Load(content);
            Player2.Load(content);
            Player3.Load(content);
            Player4.Load(content);
            Player5.Load(content);
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
            timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            if(Input.IsPressed(Keys.Right))
            {
                chooseOption++;
                if (chooseOption >= options.Count)
                    chooseOption = chooseOption - options.Count;
            }
            if(Input.IsPressed(Keys.Left))
            {
                chooseOption--;
                if (chooseOption < 0)
                    chooseOption = options.Count - 1;
            }
            if (timer > 2f)
            {
                if (Input.IsPressed(Keys.Enter))
                {
                    switch (chooseOption)
                    {
                        case 0:
                            {
                                if (characterChoosen == 0)
                                {
                                    Player1.SetCharacterInPlay();
                                    Player1.SetCharacterPosition(new Vector2(600, 350));
                                    Collisions.characterCollisions.Add(Player1);
                                    CharactersHandler.AddPlayer(Player1);
                                }
                                else
                                {
                                    Player1.SetCharacterPosition(new Vector2(700, 350));
                                    Collisions.characterCollisions.Add(Player1);
                                    CharactersHandler.AddPlayer(Player1);
                                }
                                characterChoosen++;
                            }
                            break;
                        case 1:
                            {
                                if (characterChoosen == 0)
                                {
                                    Player2.SetCharacterInPlay();
                                    Player2.SetCharacterPosition(new Vector2(600, 350));
                                    Collisions.characterCollisions.Add(Player2);
                                    CharactersHandler.AddPlayer(Player2);
                                }
                                else
                                {
                                    Player2.SetCharacterPosition(new Vector2(700, 350));
                                    Collisions.characterCollisions.Add(Player2);
                                    CharactersHandler.AddPlayer(Player2);
                                }
                                characterChoosen++;
                            }
                            break;
                        case 2:
                            {
                                if (characterChoosen == 0)
                                {
                                    Player3.SetCharacterInPlay();
                                    Player3.SetCharacterPosition(new Vector2(600, 350));
                                    Collisions.characterCollisions.Add(Player3);
                                    CharactersHandler.AddPlayer(Player3);
                                }
                                else
                                {
                                    Player3.SetCharacterPosition(new Vector2(700, 350));
                                    Collisions.characterCollisions.Add(Player3);
                                    CharactersHandler.AddPlayer(Player3);
                                }
                                    characterChoosen++;
                            }
                            break;
                        case 3:
                            {
                                if (characterChoosen == 0)
                                {
                                    Player4.SetCharacterInPlay();
                                    Player4.SetCharacterPosition(new Vector2(600, 350));
                                    Collisions.characterCollisions.Add(Player4);
                                    CharactersHandler.AddPlayer(Player4);
                                }
                                else
                                {
                                    Player4.SetCharacterPosition(new Vector2(700, 350));
                                    Collisions.characterCollisions.Add(Player4);
                                    CharactersHandler.AddPlayer(Player4);
                                }
                                characterChoosen++;
                            }
                            break;
                        case 4:
                            {
                                if (characterChoosen == 0)
                                {
                                    Player5.SetCharacterInPlay();
                                    Player5.SetCharacterPosition(new Vector2(600, 350));
                                    Collisions.characterCollisions.Add(Player5);
                                    CharactersHandler.AddPlayer(Player5);
                                }
                                else
                                {
                                    Player5.SetCharacterPosition(new Vector2(700, 350));
                                    Collisions.characterCollisions.Add(Player5);
                                    CharactersHandler.AddPlayer(Player5);
                                }
                                characterChoosen++;
                            }
                            break;
                    }
                    if (characterChoosen == 2)
                    {
                        game.gameState = Game1.GameState.running;
                    }
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, options[1], new Vector2(100, 25), Color.White);
            spriteBatch.DrawString(font, options[0], new Vector2(190, 25), Color.White);
            spriteBatch.DrawString(font, options[2], new Vector2(300, 25), Color.White);
            spriteBatch.DrawString(font, options[4], new Vector2(400, 25), Color.White);
            spriteBatch.DrawString(font, options[3], new Vector2(500, 25), Color.White);
            spriteBatch.DrawString(font, "Escolhidos: " + characterChoosen,new Vector2(700, 50), Color.White);
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
