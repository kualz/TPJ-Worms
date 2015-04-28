#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Worms_0._0._1.Weapons_and_projectiles;
#endregion

namespace Worms_0._0._1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Characters Player1, Player2;
        Weapons weapon;
        Crosshair MIRA;
        Vector2 mousevector;
        Map TesteMapa;
        private Camera2D Camera;
        public int CameraFocusAux = 0;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.IsFullScreen = false;
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
            Camera.Scale = 0.7f;
            TesteMapa = new Map();
            TesteMapa.Load(Content);
            TesteMapa.InitRectMap();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MIRA = new Crosshair();
            MIRA.Load(Content);
            CharactersHandler.InitList(Content);
            Player1 = CharactersHandler.getCharacter(0);
            Player1.SetCharacterInPlay();
            Player2 = CharactersHandler.getCharacter(1);
            Player1.SetCharacterPosition(new Vector2(600, 350));
            Player1.Load(Content);
            Collisions.characterCollisions.Add(Player1);
            Player2.SetCharacterPosition(new Vector2(700, 350));
            Player2.Load(Content);
            Collisions.characterCollisions.Add(Player2);
            CharactersHandler.AddPlayer(Player1);
            CharactersHandler.AddPlayer(Player2);
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            TesteMapa.update(gameTime);
            Input.Update();
            MIRA.update();
            MouseState mState = Mouse.GetState();
            Camera.Focus = MIRA;
            mousevector = new Vector2(mState.X, mState.Y);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Input.IsPressed(Keys.K) )
            {
                //CameraFocusAux++;
                //if (CameraFocusAux > CharactersHandler.Players.Count-1) CameraFocusAux = 0;
                //Camera.Focus = CharactersHandler.Players[CameraFocusAux];
                CharactersHandler.ChangeActive();

            }
            CharactersHandler.updatePlayers(gameTime);
            //Player1.Update(gameTime);  
            //Player2.Update(gameTime);
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);
            //int aux = 0;
            //if (aux == 0)
            //{
            //    TesteMapa.Draw(spriteBatch);
            //    aux = 1;
            //}
            //else
            TesteMapa.secondDraw(spriteBatch);

            MIRA.draw(spriteBatch, mousevector);
            Player1.Draw(spriteBatch);
            Player2.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

 
    }
}
