using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.Menus
{
    class OpeningCutScene
    {
        private Texture2D LogoIpca, LogoGDTV, Texture;
        private SpriteFont spriteFont;
        private Vector2 DrawPosition;
        private float Opacidade = 0f, ElapsedTime, TotalTime = 0, OpacidadLetras = 0f, Scale = 1f;

        public OpeningCutScene()
        { }

        public void load(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("MyFont");
            LogoIpca = content.Load<Texture2D>("LogoIpca.png");
            LogoGDTV = content.Load<Texture2D>("LogoGDTV");
            Texture = LogoIpca;
            DrawPosition = new Vector2(250, 250);
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            TotalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ElapsedTime > 0.05f && TotalTime < 2f )
            {
                Opacidade += 0.1f;
                ElapsedTime = 0f;
            }
            if (ElapsedTime > 0.05f && TotalTime > 3f )
            {
                Opacidade -= 0.1f;
                ElapsedTime = 0f;
            }

            if (OpacidadLetras < 1) OpacidadLetras += 0.01f;

            if(TotalTime > 5f && Texture != LogoGDTV)
            {
                Texture = LogoGDTV;
                Opacidade = 0;
                TotalTime = 0;
                Scale = 0.5f;
                DrawPosition = new Vector2(100, 250);
            }
            if (TotalTime > 6f && Texture == LogoGDTV) game.gameState = Game1.GameState.Menu;

            if (Input.IsPressed(Keys.Enter) || Input.IsPressed(Keys.Escape)) game.gameState = Game1.GameState.Menu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawPosition,null, Color.White * Opacidade,0f,Vector2.Zero,Scale,SpriteEffects.None,0f);
            spriteBatch.DrawString(spriteFont, "Press Escape or Enter to skip.", new Vector2(500, 650), Color.White * OpacidadLetras);
        }
    }
}
