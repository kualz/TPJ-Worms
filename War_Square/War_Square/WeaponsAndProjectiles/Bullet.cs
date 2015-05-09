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
        public float range, gravity = 9.8f, rotation, speed, timerExplosion, intervalo = 20f,intervaloRifle = 1f, explosionScale = 0.05f;
        public Vector2 sourcePosition, initialpos;
        private Vector2 direction, velocity;
        private Rectangle bulletRec;
        static public Vector2 rec;
        private Texture2D[] explosion;
        private int currentFrame1 = 0;
        float deltatime;
        bool RocketExplosion = false, RifleExplosion = false;
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
                      float speed, Texture2D[] explosion)
        {
            this.range = range;
            this.sourcePosition = sourcePosition;
            this.initialpos = sourcePosition;
            this.rotation = rotation;
            this.direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            this.ammoType = ammo;
            this.speed = speed;
            velocity = new Vector2(100*(float)Math.Cos(rotation), -100*(float)Math.Sin(rotation));
            this.explosion = explosion;
        }

        public void update(GameTime gameTime, Weapons weapon)
        {
            deltatime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timerExplosion += deltatime;

            if (timerExplosion > intervaloRifle && RifleExplosion)
            {
                currentFrame1++;
                if (currentFrame1 == 4)
                {                
                    Collisions.tilesCollisions.Remove(CheckCollisionsProjectile(bulletRec));
                }
                if (currentFrame1 >= (9))
                {
                    currentFrame1 = 0;
                    weapon.bulletsOnScreen.Remove(this);
                    RifleExplosion = false;
                }
                timerExplosion = 0;
            }


            if (timerExplosion > intervalo && RocketExplosion)
            {
                currentFrame1++;
                if (currentFrame1 == 7)
                {
                    ExplosionTileRemove(CheckCollisionsProjectile(bulletRec));
                    Collisions.tilesCollisions.Remove(CheckCollisionsProjectile(bulletRec));
                }
                if (currentFrame1 >= (9))
                {
                    currentFrame1 = 0;
                    weapon.bulletsOnScreen.Remove(this);
                    RocketExplosion = false;
                }
                timerExplosion = 0;
            }

            if (ammoType == AmmoType.cal32 && CheckCollisionsProjectile(bulletRec) == new Rectangle(0, 0, 0, 0) && RifleExplosion == false)
            {
                sourcePosition = sourcePosition + direction * speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                bulletRec = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, 15, 15);
            }
            else if (ammoType == AmmoType.rocket && CheckCollisionsProjectile(bulletRec) == new Rectangle(0, 0, 0, 0) && RocketExplosion == false)
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
                explosionScale = 0.05f;
                RifleExplosion = true;           
            }
            if (CheckCollisionsProjectile(bulletRec) != new Rectangle(0, 0, 0, 0) && ammoType == AmmoType.rocket)
            {
                explosionScale = 0.2f;
                RocketExplosion = true;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (RocketExplosion)
                spriteBatch.Draw(explosion[currentFrame1], new Vector2(rec.X - 30 , rec.Y - 30 ), null, Color.White, 0f, new Vector2((float)5, (float)5), explosionScale, SpriteEffects.None, 0f);
            if (RifleExplosion)
                spriteBatch.Draw(explosion[currentFrame1], new Vector2(rec.X, rec.Y), null, Color.White, 0f, new Vector2((float)5, (float)5), explosionScale, SpriteEffects.None, 0f);
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

        public void ExplosionTileRemove(Rectangle rect)
        {
            List<Vector2> explosionrange = new List<Vector2>();
            Vector2 comparison;
            Vector2 aux1 = new Vector2(rect.X - 20, rect.Y);
            Vector2 aux2 = new Vector2(rect.X - 40, rect.Y);
            Vector2 aux3 = new Vector2(rect.X + 20, rect.Y);
            Vector2 aux4 = new Vector2(rect.X + 40, rect.Y);
            Vector2 aux5 = new Vector2(rect.X, rect.Y + 20);
            Vector2 aux6 = new Vector2(rect.X, rect.Y - 20);
            Vector2 aux7 = new Vector2(rect.X, rect.Y + 40);
            Vector2 aux8 = new Vector2(rect.X, rect.Y - 40);
            Vector2 aux9 = new Vector2(rect.X + 20, rect.Y + 20);
            Vector2 aux10 = new Vector2(rect.X - 20, rect.Y - 20);
            Vector2 aux11 = new Vector2(rect.X - 20, rect.Y + 20);
            Vector2 aux12 = new Vector2(rect.X + 20, rect.Y - 20);

            explosionrange.Add(aux1);
            explosionrange.Add(aux2);
            explosionrange.Add(aux3);
            explosionrange.Add(aux4);
            explosionrange.Add(aux5);
            explosionrange.Add(aux6);
            explosionrange.Add(aux7);
            explosionrange.Add(aux8);
            explosionrange.Add(aux9);
            explosionrange.Add(aux10);
            explosionrange.Add(aux11);
            explosionrange.Add(aux12);

            for (int i = Collisions.tilesCollisions.Count - 1; i >= 0; i--)
            {
                comparison = new Vector2(Collisions.tilesCollisions[i].X, Collisions.tilesCollisions[i].Y);
                foreach (Vector2 vector in explosionrange)
                {
                    if (comparison == vector)
                    {
                        Collisions.tilesCollisions.RemoveAt(i);
                    }   
                }
            }
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

        public void DontNeverDuwides(float scale)
        {
            this.explosionScale = scale;
        }
    }
}
