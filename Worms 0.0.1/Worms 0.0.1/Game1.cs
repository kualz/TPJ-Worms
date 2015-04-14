#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace Worms_0._0._1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Characters Player1, Player2;
        Weapons weapon;

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
            CharactersHandler.InitList(Content);
            Player1 = CharactersHandler.getCharacter(1);
            Player1.SetCharacterInPlay();
            //Player2 = CharactersHandler.getCharacter(2);
            Player1.SetCharacterPosition(new Vector2(600, 350));
            Player1.Load(Content);
            WeaponsHandler.InitList(Player1, Content);
           //Player2.SetCharacterPosition(new Vector2(700, 350));
           //Player2.Load(Content);
           // WeaponsHandler.InitList(Player2, Content);
            weapon = WeaponsHandler.GetWeapon(Player1, 0);
            weapon.Load(Content, "WeaponRifle");
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
          
                Player1.Update(gameTime);
                weapon.Update(gameTime, Player1);
            
            //Player2.Update(gameTime);
            //weapon.Update(gameTime, Player2);

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();
            Player1.Draw(spriteBatch);
            //Player2.Draw(spriteBatch);
            weapon.Draw(spriteBatch, Player1);
            spriteBatch.End();


            base.Draw(gameTime);
        }

 
    }
}
