using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1.Weapons_and_projectiles
{
    class Bullet
    {
        public float range;
        public float speed = 100f;
        private Vector2 sourcePosition;
        private Vector2 direction;
        private Texture2D texturax;
        private float rotation;
        static private List<string> names = new List<string>();
        private List<Bullet> bulletsOnScreen = new List<Bullet>();
        private string asset;
        public enum AmmoType
        {
            cell,
            cal32,
            rocket,
            nade
        }
        public AmmoType ammoType;

        public Bullet() { }

        public Bullet(Vector2 sourcePosition, float rotation, AmmoType ammo, int range)
        {
            this.range = range;
            this.sourcePosition = sourcePosition;
            this.rotation = rotation;
            this.direction = new Vector2((float)Math.Cos(rotation),
                                         (float)Math.Sin(rotation));
            this.ammoType = ammo;
        }

        public void load(ContentManager content)
        {
            names.Add("teste_Projetil1");
            texturax = content.Load<Texture2D>(names[0]);
        }

        public void addAmmoToStack(Bullet shooted)
        {
            bulletsOnScreen.Add(shooted);
        }

        public void Updatedeletebullet(Vector2 inicialPosition)
        {
            for (int i = 0; i < bulletsOnScreen.Count; i++)
            {
                if ((bulletsOnScreen[i].sourcePosition - inicialPosition).Length() > bulletsOnScreen[i].range)
                    bulletsOnScreen.Remove(bulletsOnScreen[i]);
            }
                
        }

        public void update(GameTime gameTime, AmmoType activeWeaponAmmoType)
        {
            foreach (Bullet bullet in bulletsOnScreen){
                if (bullet.ammoType == AmmoType.cal32)
                    bullet.sourcePosition = bullet.sourcePosition + bullet.direction * bullet.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;   
            }
        }

        public void draw(SpriteBatch spriteBatch, AmmoType activeWeaponAmmoType)
        {
            foreach (Bullet bullet in bulletsOnScreen){
                if (activeWeaponAmmoType == AmmoType.cal32)
                    spriteBatch.Draw(texturax, bullet.sourcePosition, null, Color.White, bullet.rotation, new Vector2(5, 5), 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
