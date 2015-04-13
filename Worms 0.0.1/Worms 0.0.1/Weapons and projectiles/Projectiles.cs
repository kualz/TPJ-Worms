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
        private List<string> texturas1 = new List<string>();
        private Vector2 Direction;

        public enum AmmoType
        {
            cal22,
            chell,
            nade,
            rocket
        }
        protected AmmoType ammotype;

        public Projectiles()
        { }

        public Projectiles(AmmoType ammoType, float rotation, string texturaArma)
        {
            this.position = base.PositionRelativeToCharacter;
            this.speed = 100f;
            this.ammotype = ammoType;
            this.Direction = new Vector2((float)Math.Sin(rotation), 
                                         (float)Math.Cos(rotation));
            texturas1.Add(texturaArma);
        }

        public void load(ContentManager content){
            texturas = new Texture2D[4];
            texturas[0] = content.Load<Texture2D>(texturas1[0]);
            texturas[1] = content.Load<Texture2D>(texturas1[1]);
            texturas[2] = content.Load<Texture2D>(texturas1[2]);
            texturas[3] = content.Load<Texture2D>(texturas1[3]);
        }
        
        public void Update(GameTime gameTime, Projectiles updateProjectile){

        }

        public void draw(SpriteBatch spriteBatch)
        {

        }

        public AmmoType getAmmoType(){
            return this.ammotype;
        }
    }
}
