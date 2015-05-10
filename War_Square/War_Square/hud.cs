using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using War_Square.characters;
using War_Square.WeaponsAndProjectiles;

namespace War_Square
{
    class hud
    {
        private SpriteFont font;
        private List<string> assets = new List<string>();
        static public float roundTime = 7;
        private float deltaTime, speed = 50;
        private Color color;

        /// <summary>
        /// o que pode acontecer aqui neste construtor 'e que podemos adicionar os assets carregalos!
        /// e depois fazer o draw das imagens no ecran! e temos hud mai completo!
        /// </summary>
        public hud()
        { }

        public void load(ContentManager content)
        {
            font = content.Load<SpriteFont>("MyFont");
            assets.Add("PLayer: ");
            assets.Add("Hit Points: ");
            assets.Add("Gun: ");
            assets.Add("Round Time: ");
        }

        public void update(GameTime gameTime)
        {
            roundTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (roundTime <= 5f)
                color = Color.OrangeRed;
            if (roundTime <= 2.5f)
                color = Color.Red;
            else color = Color.White;
        }

        public void draw(SpriteBatch batch, Camera2D cam, Characters Char)
        {
            batch.DrawString(font, assets[0] + Char.returnName(), new Vector2(cam.Position.X - 700, cam.Position.Y- 500), Color.White);
            batch.DrawString(font, assets[1] + Char.getHp(), new Vector2(cam.Position.X - 550, cam.Position.Y - 500), Color.White);
            batch.DrawString(font, assets[2] + Char.GetActiveWeapon().getName(), new Vector2(cam.Position.X - 700, cam.Position.Y - 475), Color.White);
            batch.DrawString(font, assets[3] + roundTime, new Vector2(cam.Position.X - 700, cam.Position.Y - 450), color);
        }

        public void ResetlRoundTime(){
            roundTime = 7;
        }
    }
}
