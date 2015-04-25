using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Vector2 mousevector;
        private Map TesteMapa;
        private float roundTime = 20;
        private Camera2D Camera;
        public int CameraFocusAux = 1;
        private Characters GhostCharacter;
        public enum GameState
        {
            running,
            Paused,
            gameOver,
            Menu,
            Options,
            MapChoose,
            CharacterChoose
        }
        public GameState gameState = GameState.Menu;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;
            Content.RootDirectory = "Content";
            Camera = new Camera2D(this);
            Components.Add(Camera);
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            Camera.Scale = 1f;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("MyFont");
            CharactersHandler.InitList(Content);
            MenusHandler.load(Content, this);
            TesteMapa = new Map();
            TesteMapa.Load(Content);
            TesteMapa.InitRectMap();
            MIRA = new Crosshair();
            MIRA.Load(Content);
            GhostCharacter = CharactersHandler.getCharacter(5);
            //////mudar esta posicao para a posicao do menu!\\\\\\\\\\\\\\\\\
            GhostCharacter.SetCharacterPosition(new Vector2(400, 350));
            CharactersHandler.AddPlayer(GhostCharacter);
            Camera.Focus = CharactersHandler.Players[0];
            GhostCharacter.Load(Content);
            //Player1 = CharactersHandler.getCharacter(0);
            //Player1.SetCharacterInPlay();
            //Player2 = CharactersHandler.getCharacter(1);
            //Player1.SetCharacterPosition(new Vector2(600, 350));
            //Player1.Load(Content);
            //Collisions.characterCollisions.Add(Player1);
            //Player2.SetCharacterPosition(new Vector2(700, 350));
            //Player2.Load(Content);
            //Collisions.characterCollisions.Add(Player2);
            //CharactersHandler.AddPlayer(Player1);
            //CharactersHandler.AddPlayer(Player2);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            if (gameState != GameState.running)
            {
                MenusHandler.Update(gameTime, this);
            }
            else
            {
                Camera.Scale = 0.7f;
                Camera.Focus = CharactersHandler.Players[CameraFocusAux];
                roundTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                TesteMapa.update(gameTime);
                MouseState mState = Mouse.GetState();
                mousevector = new Vector2(mState.X, mState.Y);
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    gameState = GameState.Paused;
                }
                if (Input.IsPressed(Keys.K) && CharactersHandler.getPlayerIN_GAME(0).isJumping() == false && CharactersHandler.getPlayerIN_GAME(1).isJumping() == false)
                {
                    CameraFocusAux++;
                    if (CameraFocusAux > CharactersHandler.Players.Count - 1) CameraFocusAux = 1;
                    Camera.Focus = CharactersHandler.Players[CameraFocusAux];
                    CharactersHandler.ChangeActive();
                }
                if (roundTime <= 0)
                {
                    CameraFocusAux++;
                    if (CameraFocusAux > CharactersHandler.Players.Count - 1) CameraFocusAux = 1;
                    Camera.Focus = CharactersHandler.Players[CameraFocusAux];
                    CharactersHandler.ChangeActive();
                    roundTime = 20;
                }
                CharactersHandler.updatePlayers(gameTime);
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);
            if (gameState != GameState.running)
                MenusHandler.draw(spriteBatch, this);
            else
            {
                TesteMapa.secondDraw(spriteBatch);
                spriteBatch.DrawString(spriteFont, "Time: " + roundTime, new Vector2(50, 50), Color.White);
                MIRA.draw(spriteBatch, mousevector);
                CharactersHandler.DrawPlayers(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
