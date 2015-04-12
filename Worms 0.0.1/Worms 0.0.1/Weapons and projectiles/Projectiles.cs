using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Worms_0._0._1
{
    class Projectiles : Weapons
    {
        protected float speed;
        protected Vector2 position;
        private Texture2D[] texturas;
        private Vector2 Direction;
        public enum AmmoType
        {
            cal22,
            chell,
            nade,
            rocket
        }
        protected AmmoType ammotype;


        public Projectiles(AmmoType ammoType, float rotation)
        {
            this.position = base.PositionRelativeToCharacter;
            this.speed = 100f;
            this.ammotype = ammoType;
            this.Direction = new Vector2((float)Math.Sin(rotation), 
                                         (float)Math.Cos(rotation));
        }

        public void load(ContentManager content){
            texturas = new Texture2D[4];
            texturas[0] = content.Load<Texture2D>("teste_Projetil1");
            texturas[1] = content.Load<Texture2D>("teste_Projetil1");
            texturas[2] = content.Load<Texture2D>("teste_Projetil1");
            texturas[3] = content.Load<Texture2D>("teste_Projetil1");
        }
        
        public void UpdateChell(GameTime gameTime){

        }

        public void UpdateCal32(GameTime gameTime){

        }

        public void UpdateRocket(GameTime gameTime){

        }

        public void UpdateNade(GameTime gameTime){

        }



        public void drawCal32(SpriteBatch spriteBatch){
            spriteBatch.Draw(this.texturas[0], position, null, Color.White);
        }

        public void drawChell(SpriteBatch spriteBatch){

        }

        public void drawRocket(SpriteBatch spriteBatch){

        }

        public void drawNade(SpriteBatch spriteBatch){

        }

        public AmmoType getAmmoType(){
            return this.ammotype;
        }
    }
}
