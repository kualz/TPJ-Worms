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
        public Vector2 sourcePosition;
        private Vector2 direction;
        private Vector2 velocity;
        private Point mousePos;
        private Mouse mouse;
        private float rotation;
        private Rectangle bulletRec;
        static public Vector2 rec;
        private float deltaX;
        private float ratio;
        private float K = 1;
        private int distanciaPercorrida = 0;
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
                      Point MousePos)
        {
            this.range = range;
            this.sourcePosition = sourcePosition;
            this.rotation = rotation;
            this.direction = new Vector2((float)Math.Cos(rotation),(float)Math.Sin(rotation));
            this.ammoType = ammo;
            this.speed = speed;
            mousePos = MousePos;
        }

        public void update(GameTime gameTime, Weapons weapon)
        {
            deltaX = Math.Abs(mousePos.X - sourcePosition.X);
            ratio = 1 / deltaX;
            distanciaPercorrida++;

            if (ammoType == AmmoType.cal32)
            {
                sourcePosition = sourcePosition + direction * speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                bulletRec = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, 15, 15);
            }
            else if (ammoType == AmmoType.rocket)
            {
                velocity.X += 0.20f * K;
                if (velocity.X > 5f) velocity.X = 5f;
                velocity.Y = ratio * 5f;
                if (distanciaPercorrida > deltaX / 2) sourcePosition.Y += velocity.Y;
                if (distanciaPercorrida < deltaX / 2) sourcePosition.Y -= velocity.Y;
                if (mousePos.X > sourcePosition.X) sourcePosition.X += velocity.X;
                if (mousePos.X < sourcePosition.X) sourcePosition.X -= velocity.X;
                
                bulletRec = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, 15, 15);
            }
            else if (ammoType == AmmoType.nade) 
            { 
            
            }
            if (CheckCollisionsProjectile(bulletRec) != new Rectangle(0,0,0,0))
            {
                weapon.Sexplosion = true;
                Collisions.tilesCollisions.Remove(CheckCollisionsProjectile(bulletRec));
                for (int i = weapon.bulletsOnScreen.Count - 1; i >= 0; i--)
                {
                    if (weapon.bulletsOnScreen[i] == this)
                        weapon.bulletsOnScreen.Remove(this);
                }
            }      
        }

        public Rectangle CheckCollisionsProjectile(Rectangle rect)
        {
            //List<Rectangle> collidingWith = new List<Rectangle>();
            foreach (Rectangle rectangle in Collisions.tilesCollisions)
            {
                if (rect.Intersects(rectangle) && rect != rectangle)
                {
                    rec = new Vector2(rectangle.X, rectangle.Y);
                    return rectangle;
                }
            }
            return new Rectangle(0,0,0,0);
        }

        public Rectangle getRectangle()
        {
            return this.bulletRec;
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

        public void DontNeverDuwides()
        {

        }
    }
}
