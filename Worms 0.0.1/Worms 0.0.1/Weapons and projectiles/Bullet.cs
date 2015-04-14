//REVER CODIGO DA SHOTGUN
//REVER CODIGO DA SHOTGUN
//REVER CODIGO DA SHOTGUN

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
        public float speed;
        private Vector2 sourcePosition;
        private Vector2 direction;
        private Texture2D[] texturax;
        private Texture2D[] texturasShotgun;
        private float rotation;
        private List<string> names = new List<string>();
        private List<Bullet> bulletsOnScreen = new List<Bullet>();
        private Vector2 shotPosition;
        public enum AmmoType
        {
            cell,
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
            names.Add("teste_Projetil3");
            names.Add("teste_Projetil4");
            //MUDAR TEXTURAS
            names.Add("Cell1");
            names.Add("Cell2");
            names.Add("Cell3");
            names.Add("Cell4");
            //MUDAR TEXTURAS

            texturax = new Texture2D[4];
            texturax[0] = content.Load<Texture2D>(names[0]);
            texturax[1] = content.Load<Texture2D>(names[1]);
            texturax[2] = content.Load<Texture2D>(names[2]);
            texturax[3] = content.Load<Texture2D>(names[3]);

            texturasShotgun = new Texture2D[4];
            texturasShotgun[0] = content.Load<Texture2D>(names[4]);
            texturasShotgun[1] = content.Load<Texture2D>(names[5]);
            texturasShotgun[2] = content.Load<Texture2D>(names[6]);
            texturasShotgun[3] = content.Load<Texture2D>(names[7]);
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
                if (bullet.ammoType == AmmoType.cal32){
                    bullet.sourcePosition = bullet.sourcePosition + bullet.direction * bullet.speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                }
                if (bullet.ammoType == AmmoType.cell){
                    bullet.sourcePosition = bullet.sourcePosition + (bullet.direction + new Vector2(0.75f)) * bullet.speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                    bullet.sourcePosition = bullet.sourcePosition + (bullet.direction) * bullet.speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                    bullet.sourcePosition = bullet.sourcePosition + (bullet.direction - new Vector2(0.75f)) * bullet.speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                }
            }
        }

        public void draw(SpriteBatch spriteBatch, AmmoType activeWeaponAmmoType, int x){
            foreach (Bullet bullet in bulletsOnScreen){
                if (activeWeaponAmmoType == AmmoType.cal32)
                    spriteBatch.Draw(texturax[0], new Vector2(bullet.sourcePosition.X + 3, bullet.sourcePosition.Y + 7), null, Color.White, bullet.rotation, new Vector2((float)2.5, (float)2.5), 1f, SpriteEffects.None, 0f);
                if (bullet.ammoType == AmmoType.cell){
                    spriteBatch.Draw(texturasShotgun[x], new Vector2(bullet.sourcePosition.X + 3, bullet.sourcePosition.Y + 7), null, Color.White, bullet.rotation, new Vector2((float)2.5, (float)2.5), 1f, SpriteEffects.None, 0f);
                }
            }
        }

        public float getFireRate(AmmoType ammo){
            if (ammo == AmmoType.cal32)
                return 0.1f;
            else if (ammo == AmmoType.cell)
                return 0.7f;
            else return 0;
        }
    }
}
