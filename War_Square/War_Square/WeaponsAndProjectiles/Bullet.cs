using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace War_Square.WeaponsAndProjectiles
{
    class Bullet
    {
        public float range, gravity = 9.8f, rotation, speed;
        public Vector2 sourcePosition, initialpos;
        private Vector2 direction, velocity;
        private Rectangle bulletRec;
        static public Vector2 rec;
        private int distanciaPercorrida = 0;
        float deltatime;
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
                      float speed)
        {
            this.range = range;
            this.sourcePosition = sourcePosition;
            this.initialpos = sourcePosition;
            this.rotation = rotation;
            this.direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            this.ammoType = ammo;
            this.speed = speed;

            velocity = new Vector2(100*(float)Math.Cos(rotation), -100*(float)Math.Sin(rotation));
            
        }

        public void update(GameTime gameTime, Weapons weapon)
        {
            deltatime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            distanciaPercorrida++;

            if (ammoType == AmmoType.cal32)
            {
                sourcePosition = sourcePosition + direction * speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                bulletRec = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, 15, 15);
            }
            else if (ammoType == AmmoType.rocket)
            {

                sourcePosition.X = initialpos.X + velocity.X * deltatime;
                sourcePosition.Y = initialpos.Y - velocity.Y * deltatime + 0.5f * gravity * (float)(Math.Pow(deltatime, 2));

                bulletRec = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, 15, 15);
            }
            else if (ammoType == AmmoType.nade){
                //nades update method!
            }
            if (CheckCollisionsProjectile(bulletRec) != new Rectangle(0, 0, 0, 0) && ammoType == AmmoType.cal32)
            {
                weapon.Sexplosion = true;
                Collisions.tilesCollisions.Remove(CheckCollisionsProjectile(bulletRec));
                for (int i = weapon.bulletsOnScreen.Count - 1; i >= 0; i--)
                {
                    if (weapon.bulletsOnScreen[i] == this)
                        weapon.bulletsOnScreen.Remove(this);
                }
            }
            if (CheckCollisionsProjectile(bulletRec) != new Rectangle(0, 0, 0, 0) && ammoType == AmmoType.rocket)
            {
                weapon.Sexplosion = true;
                Collisions.tilesCollisions.Remove(CheckCollisionsProjectile(bulletRec));
                weapon.bulletsOnScreen.Remove(this);
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
            return new Rectangle(0, 0, 0, 0);
        }

        public Rectangle getRectangle()
        {
            return this.bulletRec;
        }

        public float getFireRate(AmmoType ammo)
        {
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
