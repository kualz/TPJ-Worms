using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using War_Square.characters;
using War_Square.WeaponsAndProjectiles;
using War_Square.Sounds;


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
        static public int SelectedMap, charactersInPlay = 0;
        private Map TesteMapa;
        private Camera2D Camera;
        public int CameraFocusAux = 1;
        private Characters GhostCharacter;
        public int cameraX {get; set;}
        private hud Interface = new hud();
        private bool auxMapa = false;
        private Background background = new Background();
        static public bool firstEntry = true;
        public enum GameState
        {
            running,
            Paused,
            gameOver,
            Menu,
            Options,
            MapChoose,
            CharacterChoose,
            OpeningCutScene,
            CharacterChangeScene,
            Win
        }
        public GameState gameState = GameState.OpeningCutScene;
        private SoundLoader globalSounds = new SoundLoader();
        
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
            SoundManager.InitSoundLists();
            background.Load(Content);
            cameraX = 400;
            Camera.Scale = 1f;
            globalSounds.load(Content);
            magzzz.initializeAmmo();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("MyFont");
            CharactersHandler.InitList();
            MenusHandler.load(Content, this, Camera);
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

            //desenha os menus que nao sao ingame
            //se nao esta in game ou nao existe vencedor 
            if (gameState != GameState.running && gameState != GameState.Win && gameState != GameState.CharacterChangeScene)
            {
                MenusHandler.Update(gameTime, this, Content, Camera, Interface);
                if (gameState != GameState.Paused){
                    cameraX = 400;
                    Camera.Position = new Vector2(cameraX, 350);
                    Camera.Scale = 1f;
                }
            }

            //se tiver in game!!!
            else
            {
                if (gameState == GameState.Win || gameState == GameState.CharacterChangeScene){
                    MenusHandler.Update(gameTime, this, Content, Camera, Interface);
                }
                else
                {
                    SoundManager.playMusic("ingameMusic");   
                    if (auxMapa == false){
                        TesteMapa.InitRectMap();
                        auxMapa = true;
                    }
                    Interface.update(gameTime);
                    Camera.Scale = 0.7f;
                    TesteMapa.update(gameTime);

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && gameState == GameState.running)
                        gameState = GameState.Paused;

                    if (Input.IsPressed(Keys.K) && Collisions.bulletsOnScreen.Count == 0){
                        gameState = GameState.CharacterChangeScene;
                    }

                    if (hud.roundTime <= 0 && Collisions.bulletsOnScreen.Count == 0)
                    {
                        gameState = GameState.CharacterChangeScene;
                        if (CharactersHandler.isWinner()){                          
                            gameState = GameState.Win;
                            MenusHandler.winMenu.loadInGame(Camera);
                        }
                    }
                    if (CharactersHandler.isWinner())
                    {
                        cameraX = (int)CharactersHandler.getWinner().CharacterPosition().X;
                    }

                    if (Input.IsDown(Keys.Right) && cameraX < 2210 && gameState == GameState.running && !CharactersHandler.isWinner()) cameraX += 10;
                    if (Input.IsDown(Keys.Left) && cameraX > 370 && gameState == GameState.running && !CharactersHandler.isWinner()) cameraX -= 10;


                    GhostCharacter.SetCharacterPosition(new Vector2(cameraX, 350));
                    CharactersHandler.updatePlayers(gameTime);
                }
            }
            
            
            //Console.WriteLine("X:{0}   Y:{1}", CharactersHandler.getActiveCharacterRectangle().X, CharactersHandler.getActiveCharacterRectangle().Y);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);

            if (gameState != GameState.running && gameState != GameState.Paused && gameState != GameState.Win && gameState != GameState.CharacterChangeScene){
                MenusHandler.draw(spriteBatch, this, GhostCharacter, Camera);
            }
            else
            {
                background.Draw(spriteBatch);
                TesteMapa.secondDraw(spriteBatch);
                CharactersHandler.DrawPlayers(spriteBatch);
                Interface.draw(spriteBatch, Camera, CharactersHandler.getActiveCharacter());
                if (gameState == GameState.Paused || gameState == GameState.Win || gameState == GameState.CharacterChangeScene){
                    MenusHandler.draw(spriteBatch, this, CharactersHandler.getActiveCharacter(), Camera);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        internal Map getMap(){
            return this.TesteMapa;
        }
    }
}
