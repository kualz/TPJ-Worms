using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using War_Square.characters;
using War_Square.WeaponsAndProjectiles;

namespace War_Square
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        //private Characters Player1, Player2;
        private Crosshair MIRA;
        static public int SelectedMap;
        private Map TesteMapa;
        private Camera2D Camera;
        public int CameraFocusAux = 1;
        private Characters GhostCharacter;
        private int cameraX = 400;
        private hud Interface = new hud();
        private bool auxMapa = false;
        private Background background = new Background();
        public enum GameState
        {
            running,
            Paused,
            gameOver,
            Menu,
            Options,
            MapChoose,
            CharacterChoose,
            OpeningCutScene
        }
        public GameState gameState = GameState.OpeningCutScene;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;
            Content.RootDirectory = "Content";
            Camera = new Camera2D(this);
            Camera.Position = (new Vector2(cameraX, 350));
            Components.Add(Camera);
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            background.Load(Content);
            Camera.Scale = 1f;
            magzzz.initializeAmmo();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("MyFont");
            CharactersHandler.InitList(Content);
            MenusHandler.load(Content, this);
            TesteMapa = new Map();
            TesteMapa.Load(Content);
            MIRA = new Crosshair();
            MIRA.Load(Content);
            GhostCharacter = CharactersHandler.getCharacter(5);
            //////mudar esta posicao para a posicao do menu!\\\\\\\\\\\\\\\\\
            GhostCharacter.SetCharacterPosition(new Vector2(cameraX, 350));
            CharactersHandler.AddPlayer(GhostCharacter);
            Camera.Focus = CharactersHandler.Players[0];
            GhostCharacter.Load(Content);
            Interface.load(Content);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            background.Update(gameTime);
            if (gameState != GameState.running)
            {
                MenusHandler.Update(gameTime, this);
            }
            else
            {
                if (auxMapa == false)
                {
                    TesteMapa.InitRectMap();
                    auxMapa = true;
                }
                Interface.update(gameTime);
                Camera.Scale = 0.7f;            
                TesteMapa.update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    gameState = GameState.Paused;

                if (Input.IsPressed(Keys.K) && Collisions.bulletsOnScreen.Count == 0){
                    CharactersHandler.ChangeActive();
                    magzzz.setAllMag();
                    Interface.ResetlRoundTime();
                    Collisions.bulletsTagged.Clear();
                }

                if (hud.roundTime <= 0 && Collisions.bulletsOnScreen.Count == 0){                 
                    CharactersHandler.ChangeActive();
                    magzzz.setAllMag();
                    Interface.ResetlRoundTime();
                    Collisions.bulletsTagged.Clear();
                }

                if (Input.IsDown(Keys.Right) && cameraX < 2210) cameraX += 10;
                if (Input.IsDown(Keys.Left) && cameraX > 370) cameraX -= 10;

                Console.WriteLine(cameraX);
                GhostCharacter.SetCharacterPosition(new Vector2(cameraX, 350));
                CharactersHandler.updatePlayers(gameTime);
                if (CharactersHandler.isWinner() == true)
                    gameState = GameState.gameOver;
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);
            
            if (gameState != GameState.running && gameState != GameState.gameOver)
            {
                MenusHandler.draw(spriteBatch, this, GhostCharacter);
            }
            else
            {
                if(gameState == GameState.gameOver){
                     MenusHandler.draw(spriteBatch, this, GhostCharacter);
                }
                background.Draw(spriteBatch);
                TesteMapa.secondDraw(spriteBatch);
                CharactersHandler.DrawPlayers(spriteBatch);
                Interface.draw(spriteBatch, Camera, CharactersHandler.getActiveCharacter());
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
