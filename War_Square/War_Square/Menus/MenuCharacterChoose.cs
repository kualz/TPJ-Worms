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
        private int chooseOption = 0, characterChoosen = 0, playerCount = 2;
        private Characters Player1, Player2, Player3, Player4, Player5;
        private bool[] lelitos = new bool[5];
        private Color[] corolelitos = new Color[5];


        public MenuCharacterChoose() { }

        public void load(ContentManager content)
        {
            for (int i = 0; i < 5; i++) corolelitos[i] = Color.White;
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
            if (Input.IsPressed(Keys.Up))
            {
                playerCount++;
                if (playerCount > 5)
                    playerCount = 5;
            }
            if (Input.IsPressed(Keys.Down))
            {
                playerCount--;
                if (playerCount < 2)
                    playerCount = 2;
            }
            if (Input.IsPressed(Keys.Back)) game.gameState = Game1.GameState.Menu;
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
                                    Player1.SetCharacterPosition(new Vector2(600, 200));
                                    Collisions.characterCollisions.Add(Player1);
                                    CharactersHandler.AddPlayer(Player1);
                                    lelitos[chooseOption] = true;
                                    corolelitos[chooseOption] = Color.Red; 
                                    characterChoosen++;
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    Player1.SetCharacterPosition(new Vector2(700, 200));
                                    Collisions.characterCollisions.Add(Player1);
                                    CharactersHandler.AddPlayer(Player1);
                                    lelitos[chooseOption] = true;
                                    corolelitos[chooseOption] = Color.Red; 
                                    characterChoosen++;
                                }
                            }
                            break;
                        case 1:
                            {
                                if (characterChoosen == 0)
                                {
                                    Player2.SetCharacterInPlay();
                                    Player2.SetCharacterPosition(new Vector2(600, 200));
                                    Collisions.characterCollisions.Add(Player2);
                                    CharactersHandler.AddPlayer(Player2);
                                    corolelitos[chooseOption] = Color.Red; 
                                    lelitos[chooseOption] = true;
                                    characterChoosen++;
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    Player2.SetCharacterPosition(new Vector2(700, 200));
                                    Collisions.characterCollisions.Add(Player2);
                                    CharactersHandler.AddPlayer(Player2);
                                    corolelitos[chooseOption] = Color.Red; 
                                    lelitos[chooseOption] = true;
                                    characterChoosen++;
                                }
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
                                    lelitos[chooseOption] = true;
                                    corolelitos[chooseOption] = Color.Red; 
                                    characterChoosen++;
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    Player3.SetCharacterPosition(new Vector2(700, 350));
                                    Collisions.characterCollisions.Add(Player3);
                                    CharactersHandler.AddPlayer(Player3);
                                    lelitos[chooseOption] = true;
                                    corolelitos[chooseOption] = Color.Red; 
                                    characterChoosen++;
                                }
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
                                    lelitos[chooseOption] = true;
                                    corolelitos[chooseOption] = Color.Red; 
                                    characterChoosen++;
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    Player4.SetCharacterPosition(new Vector2(700, 350));
                                    Collisions.characterCollisions.Add(Player4);
                                    CharactersHandler.AddPlayer(Player4);
                                    lelitos[chooseOption] = true;
                                    corolelitos[chooseOption] = Color.Red; 
                                    characterChoosen++;
                                }
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
                                    lelitos[chooseOption] = true;
                                    corolelitos[chooseOption] = Color.Red; 
                                    characterChoosen++;
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    Player5.SetCharacterPosition(new Vector2(700, 350));
                                    Collisions.characterCollisions.Add(Player5);
                                    CharactersHandler.AddPlayer(Player5);
                                    lelitos[chooseOption] = true;
                                    corolelitos[chooseOption] = Color.Red; 
                                    characterChoosen++;
                                }
                            }
                            break;
                    }
                    if (characterChoosen >= playerCount && characterChoosen >= 2){
                        game.gameState = Game1.GameState.MapChoose;
                    }
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, options[1], new Vector2(25, 25), corolelitos[0]);
            spriteBatch.DrawString(font, options[0], new Vector2(190 - 75, 25), corolelitos[1]);
            spriteBatch.DrawString(font, options[2], new Vector2(300 - 75, 25), corolelitos[2]);
            spriteBatch.DrawString(font, options[4], new Vector2(400 - 75, 25), corolelitos[3]);
            spriteBatch.DrawString(font, options[3], new Vector2(500 - 75, 25), corolelitos[4]);
            spriteBatch.DrawString(font, "Press BackSpace to return to Menu.", new Vector2(100 - 75, 650), Color.White);
            spriteBatch.DrawString(font, "Press Up or Down to change the number of Players.", new Vector2(100 - 75, 150), Color.White);
            spriteBatch.DrawString(font, "Press Left or Right to change the selected Player.", new Vector2(100 - 75, 175), Color.White);
            spriteBatch.DrawString(font, "Press Enter to select Player.", new Vector2(100 - 75, 200), Color.White);
            spriteBatch.DrawString(font, "Escolhidos: " + characterChoosen,new Vector2(650, 50), Color.White);
            spriteBatch.DrawString(font, "Character count: " + playerCount, new Vector2(650, 100), Color.White);
            for (int i = 0; i < options.Count; i++)
            {
                if (i != chooseOption)
                    spriteBatch.Draw(textura, new Vector2(90 + i * 100 - 75, 55), new Rectangle(0, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                else
                {
                    spriteBatch.Draw(squareSelected, new Rectangle(90 + i * 100 - 75, 55, 60, 75), Color.BlueViolet);
                    spriteBatch.Draw(textura, new Vector2(90 + i * 100 - 75, 49), new Rectangle(0, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
