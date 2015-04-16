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

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
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
            Input.Update();
            MouseState mState = Mouse.GetState();
            mousevector = new Vector2(mState.X, mState.Y);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Input.IsPressed(Keys.K) && Player1.isJumping() == false && Player2.isJumping() == false) CharactersHandler.ChangeActive();
            CharactersHandler.updatePlayers(gameTime);
            //Player1.Update(gameTime);  
            //Player2.Update(gameTime);
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();
            MIRA.draw(spriteBatch, mousevector);
            Player1.Draw(spriteBatch);
            Player2.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

 
    }
}
