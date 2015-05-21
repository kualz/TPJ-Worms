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
        private Color color;
        private Texture2D HpBar;

        /// <summary>
        /// o que pode acontecer aqui neste construtor 'e que podemos adicionar os assets carregalos!
        /// e depois fazer o draw das imagens no ecran! e temos hud mai completo!
        /// </summary>
        public hud()
        { }

        public void load(ContentManager content)
        {
            font = content.Load<SpriteFont>("MyFont");
            HpBar = content.Load<Texture2D>("1");
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
            foreach (Characters CHARACTER in CharactersHandler.Players){
                if (CHARACTER.returnName() != "GhostCharacter"){

                    batch.Draw(HpBar, new Rectangle((int)CHARACTER.CharacterPos.X, (int)CHARACTER.CharacterPos.Y - 15, (50 - ((CHARACTER.getMaximumHp() - CHARACTER.getHp()) / 2)), 5), setHpColor(CHARACTER));
                }
            }
            batch.DrawString(font, assets[0] + Char.returnName(), new Vector2(cam.Position.X - 700, cam.Position.Y- 500), Color.White);
            batch.DrawString(font, assets[1] + Char.getHp(), new Vector2(cam.Position.X - 480, cam.Position.Y - 500), Color.White);
            batch.DrawString(font, assets[2] + Char.GetActiveWeapon().getName(), new Vector2(cam.Position.X - 700, cam.Position.Y - 475), Color.White);
            batch.DrawString(font, assets[3] + roundTime, new Vector2(cam.Position.X - 700, cam.Position.Y - 450), color);
        }

        public void ResetlRoundTime(){
            roundTime = 7;
        }

        private Color setHpColor(Characters Char)
        {
            if (Char.getHp() > 50) return Color.Green;
            if (Char.getHp() <= 50 && Char.getHp() > 20) return Color.Yellow;
            if (Char.getHp() <= 20) return Color.Red;
            else return Color.White;
        }
    }
}
