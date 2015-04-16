//REVER CODIGO DA SHOTGUN
//REVER CODIGO DA SHOTGUN
//REVER CODIGO DA SHOTGUN

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1.Weapons_and_projectiles
{
    class Bullet
    {
        public float range;
        public float speed;
        private Vector2 sourcePosition;
        private Vector2 direction;
        private Vector2 velocity;
        private Texture2D texturax;
        private Texture2D texturasRocket;
        private Point mousePos;
        private float rotation;
        private List<string> names = new List<string>();
        private List<Bullet> bulletsOnScreen = new List<Bullet>();
        private Vector2 shotPosition;
        private Rectangle bulletRec;
        public enum AmmoType
        {
            cal32,
            rocket,
            nade
        }
        public AmmoType ammoType;

        public Bullet() { }

        public Bullet(Vector2 sourcePosition, 
                      float rotation, 
                      AmmoType ammo, 
                      int range, 
                      float speed, 
                      Vector2 shotPosition)
        {
            this.shotPosition = shotPosition;
            this.range = range;
            this.sourcePosition = sourcePosition;
            this.rotation = rotation;
            this.direction = new Vector2((float)Math.Cos(rotation),
                                         (float)Math.Sin(rotation));
            this.ammoType = ammo;
            this.speed = speed;
        }

        public void load(ContentManager content)
        {
            names.Add("teste_Projetil1");
            names.Add("teste_Projetil2");
            texturax = content.Load<Texture2D>(names[0]);
            texturasRocket = content.Load<Texture2D>(names[1]);
        }

        public void addAmmoToStack(Bullet shooted)
        {
            bulletsOnScreen.Add(shooted);
        }

        public void UpdateDeletebullet(){
            for (int i = 0; i < bulletsOnScreen.Count; i++){
                if ((bulletsOnScreen[i].sourcePosition - bulletsOnScreen[i].shotPosition).Length() > bulletsOnScreen[i].range){
                    bulletsOnScreen.Remove(bulletsOnScreen[i]);
                }
            }
        }

        public void update(GameTime gameTime, AmmoType activeWeaponAmmoType){
            foreach (Bullet bullet in bulletsOnScreen){
                if (bullet.ammoType == AmmoType.cal32)
                {
                    bullet.sourcePosition = bullet.sourcePosition + bullet.direction * bullet.speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                    bulletRec = new Rectangle((int)bullet.sourcePosition.X, (int)bullet.sourcePosition.Y, 15, 15);
                }
                else if(bullet.ammoType == AmmoType.rocket)
                {
                    bullet.sourcePosition = bullet.sourcePosition + bullet.direction * bullet.speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                    bulletRec = new Rectangle((int)bullet.sourcePosition.X, (int)bullet.sourcePosition.Y, 15, 15);
                }
                else if (bullet.ammoType == AmmoType.nade) { }
            }
        }

        public void draw(SpriteBatch spriteBatch, AmmoType activeWeaponAmmoType){
            foreach (Bullet bullet in bulletsOnScreen){
                if (activeWeaponAmmoType == AmmoType.cal32)
                    spriteBatch.Draw(texturax, new Vector2(bullet.sourcePosition.X + 3, bullet.sourcePosition.Y + 7), null, Color.White, bullet.rotation, new Vector2((float)2.5, (float)2.5), 1f, SpriteEffects.None, 0f);
                else if (bullet.ammoType == AmmoType.rocket)
                    spriteBatch.Draw(texturasRocket, new Vector2(bullet.sourcePosition.X, bullet.sourcePosition.Y + 7), null, Color.White, bullet.rotation, new Vector2((float)5, (float)3.5), 1f, SpriteEffects.None, 0f);
                else if (bullet.ammoType == AmmoType.nade) { }
            }
        }

        public float getFireRate(AmmoType ammo){
            if (ammo == AmmoType.cal32)
                return 0.3f;
            if (ammo == AmmoType.rocket)
                return 0.6f;
            if (ammo == AmmoType.nade)
                return 0.9f;
            else return 0;
        }
    }
}
