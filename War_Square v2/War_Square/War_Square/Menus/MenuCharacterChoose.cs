using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using War_Square.characters;
using War_Square.Sounds;

namespace War_Square.Menus
{
    class MenuCharacterChoose
    {
        private List<String> options = new List<string>();
        private List<Vector2> MapPositions = new List<Vector2>();
        private SpriteFont font;
        private Texture2D textura, squareSelected, backGround, caixaDeTexto, outerGlow, textura2, textura3, textura4, textura5;
        private float timer = 0, deltaTime, opacity;
        private int playerCount = 2;
        static private int characterChoosen = 0, chooseOption = 0;
        static private Characters[] Player;
        static private bool[] lelitos = new bool[5], VectorsUsed = new bool[5];
        static private Color[] corolelitos = new Color[5];


        public MenuCharacterChoose() { }

        static private void LoadPlayers(ContentManager content)
        {
            Player = new Characters[5];
            Player[1 - 1] = CharactersHandler.getCharacter(0);
            Player[2 - 1] = CharactersHandler.getCharacter(1);
            Player[3 - 1] = CharactersHandler.getCharacter(2);
            Player[4 - 1] = CharactersHandler.getCharacter(3);
            Player[5 - 1] = CharactersHandler.getCharacter(4);
            Player[1-1].Load(content);
            Player[2-1].Load(content);
            Player[3-1].Load(content);
            Player[4-1].Load(content);
            Player[5-1].Load(content);
        }

        public void load(ContentManager content)
        {
            for (int i = 0; i < 5; i++) corolelitos[i] = Color.White;
            font = content.Load<SpriteFont>("MyFont");
            LoadPlayers(content);
            MapPositions.Add(new Vector2(300, -100));
            MapPositions.Add(new Vector2(700, -100));
            MapPositions.Add(new Vector2(1300, -100));
            MapPositions.Add(new Vector2(1800, -100));
            MapPositions.Add(new Vector2(2200, -100));  
            options.Add("Phaktumn");
            options.Add("Kualz");
            options.Add("Klipper");
            options.Add("Saber");
            options.Add("Zjeh");
            //imagens dos characters!
            caixaDeTexto = content.Load<Texture2D>("caixadeTexto");
            squareSelected = content.Load<Texture2D>("CharacterSelected");
            textura = content.Load<Texture2D>("character"); //zjeh
            textura2 = content.Load<Texture2D>("character2"); // kualz
            textura3 = content.Load<Texture2D>("character3"); // phaktumn
            textura4 = content.Load<Texture2D>("character4"); // klipper
            textura5 = content.Load<Texture2D>("character11"); // saber
            backGround = content.Load<Texture2D>("MenuTest");
            outerGlow = content.Load<Texture2D>("CharacterSelectedOuterGlow");
        }

        public void Update(GameTime gametime, Game1 game, ContentManager content)
        {
            deltaTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (deltaTime < 0.5f)
            {
                opacity -= 0.04f;
                if (opacity < 0.1f)
                    opacity = 0.1f;
            }
            if (deltaTime > 0.5f && deltaTime < 1f)
            {
                opacity += 0.035f;
            }
            if (deltaTime > 1f) deltaTime = 0f;
            timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            if(Input.IsPressed(Keys.Right))
            {
                SoundManager.playSound("menuswitch");
                chooseOption++;
                if (chooseOption >= options.Count)
                    chooseOption = chooseOption - options.Count;
            }
            if(Input.IsPressed(Keys.Left))
            {
                SoundManager.playSound("menuswitch");
                chooseOption--;
                if (chooseOption < 0)
                    chooseOption = options.Count - 1;
            }
            if (Input.IsPressed(Keys.Up))
            {
                SoundManager.playSound("menuswitch");
                playerCount++;
                if (playerCount > 5)
                    playerCount = 5;
            }
            if (Input.IsPressed(Keys.Down))
            {
                SoundManager.playSound("menuswitch");
                playerCount--;
                if (playerCount < 2)
                    playerCount = 2;
            }
            if (Input.IsPressed(Keys.Back))
            {
                SoundManager.playSound("menuswitch");
                resetArrays(content);
                resetPlayerInserted();
                CharactersHandler.JogadorActivo = 0;
                CharactersHandler.initialPlayer = 0;
                game.gameState = Game1.GameState.Menu;
            }
            if (timer > 2f)
            {
                if (Input.IsPressed(Keys.Enter))
                {
                    SoundManager.playSound("enterselect");
                    switch (chooseOption)
                    {
                        case 0:
                            {
                                if (characterChoosen == 0)
                                {
                                    insertNewPlayer(Player[chooseOption], true);
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    insertNewPlayer(Player[chooseOption], false);
                                }
                            }
                            break;
                        case 1:
                            {
                                if (characterChoosen == 0)
                                {
                                    insertNewPlayer(Player[chooseOption], true);
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    insertNewPlayer(Player[chooseOption], false);
                                }
                            }
                            break;
                        case 2:
                            {
                                if (characterChoosen == 0)
                                {
                                    insertNewPlayer(Player[chooseOption], true);
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    insertNewPlayer(Player[chooseOption], false);
                                }
                            }
                            break;
                        case 3:
                            {

                                if (characterChoosen == 0)
                                {
                                    insertNewPlayer(Player[chooseOption], true);
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    insertNewPlayer(Player[chooseOption], false);
                                }
                            }
                            break;
                        case 4:
                            {

                                if (characterChoosen == 0)
                                {
                                    insertNewPlayer(Player[chooseOption], true);
                                }
                                else if (!lelitos[chooseOption])
                                {
                                    insertNewPlayer(Player[chooseOption], false);
                                }
                            }
                            break;
                    }
                    if (characterChoosen >= playerCount && characterChoosen >= 2){
                        resetPosicoesMapa();
                        Console.WriteLine(CharactersHandler.Players.Count);
                        game.gameState = Game1.GameState.MapChoose;
                    }
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Vector2(-350, 0), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(caixaDeTexto, new Rectangle(0, 145, 550, 100), Color.White);
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
            spriteBatch.DrawString(font, "Version 0.9 Phaktumn Kualz Klipper", new Vector2(500, 650), Color.White);
            for (int i = 0; i < options.Count; i++)
            {
                if (i != chooseOption)
                    spriteBatch.Draw(textura, new Vector2(90 + i * 100 - 75, 55), new Rectangle(0, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                else
                {
                    spriteBatch.Draw(squareSelected, new Rectangle(90 + i * 100 - 75, 55, 85, 75), Color.White);
                    spriteBatch.Draw(outerGlow, new Rectangle(90 + i * 100 - 75, 55, 85, 75), Color.White * opacity);
                    spriteBatch.Draw(textura, new Vector2(90 + i * 100 - 75, 49), new Rectangle(0, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
        }

        static public void resetArrays(ContentManager content)
        {
            for (int i = 0; i < 5; i++){
                lelitos[i] = false;
                corolelitos[i] = Color.White;
            }
            characterChoosen = 0;
            chooseOption = 0;
            LoadPlayers(content);
        }

        static  public void resetPlayerInserted()
        {
            CharactersHandler.Players.Clear();
        }

        private void insertNewPlayer(Characters player, bool isFirst)
        {
            if (isFirst) player.SetCharacterInPlay();
            player.SetCharacterPosition(getPosition());
            Collisions.characterCollisions.Add(player);
            CharactersHandler.AddPlayer(player);
            lelitos[chooseOption] = true;
            corolelitos[chooseOption] = Color.Red;
            Game1.charactersInPlay++;
            characterChoosen++;
        }

        public Vector2 getPosition()
        {
            Random rnd = new Random();
            while (true)
            {
                int aux = rnd.Next(0, 5);
                if (VectorsUsed[aux] == false)
                {
                    VectorsUsed[aux] = true;
                    return MapPositions[aux];
                }
            }
        }

        public void resetPosicoesMapa()
        {
            for (int i = 0; i < 5; i++)
            {
                VectorsUsed[i] = false;
            }
        
        }

    }
}
