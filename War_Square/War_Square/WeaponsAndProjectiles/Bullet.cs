﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using War_Square.characters;

namespace War_Square.WeaponsAndProjectiles
{
    class Bullet
    {
        public float range, gravity = 9.8f, rotation, speed, timerExplosion, intervalo = 0.05f, intervaloRifle = 0.1f, explosionScale = 0.05f;
        public Vector2 sourcePosition, initialpos;
        private Vector2 direction, velocity;
        private Rectangle bulletRec;
        static public Vector2 rec;
        private Texture2D[] explosion;
        private int currentFrame1 = 0, damage;
        private float deltatime = 0;
        private bool RocketExplosion = false, RifleExplosion = false, characterhit = false;
        static public int DMGdAtRETA;
        public enum AmmoType
        {
            cal32,
            rocket,
            hadouken
        }
        public AmmoType ammoType;

        //public Bullet() { }

        public Bullet(Vector2 sourcePosition,
                      float rotation,
                      AmmoType ammo,
                      int range,
                      float speed, Texture2D[] explosion, int damage)
        {
            this.range = range;
            this.sourcePosition = sourcePosition;
            this.initialpos = sourcePosition;
            this.rotation = rotation;
            this.direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            this.ammoType = ammo;
            this.speed = speed;
            velocity = new Vector2(100 * (float)Math.Cos(rotation), -100 * (float)Math.Sin(rotation));
            this.explosion = explosion;
            this.damage = damage;
        }

        public void update(GameTime gameTime, Weapons weapon, Map map)
        {
            deltatime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timerExplosion += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timerExplosion > intervaloRifle && RifleExplosion)
            {
                currentFrame1++;
                if (currentFrame1 == 1)
                {
                    map.DestroySquare(new Vector2(bulletRec.X, bulletRec.Y));
                }
                if (currentFrame1 >= (9))
                {
                    currentFrame1 = 0;
                    Collisions.bulletsOnScreen.Remove(this);
                    RifleExplosion = false;
                }
                timerExplosion = 0;
            }


            if (timerExplosion > intervalo && RocketExplosion)
            {
                currentFrame1++;
                if (currentFrame1 == 4)
                {
                    if (characterhit == false)
                    {
                        ExplosionTileRemove(CheckCollisionsProjectile(bulletRec), map);
                    }
                }
                if (currentFrame1 >= (9))
                {
                    currentFrame1 = 0;
                    Collisions.bulletsOnScreen.Remove(this);
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
            else if (ammoType == AmmoType.hadouken && CheckCollisionsProjectile(bulletRec) == new Rectangle(0, 0, 0, 0) && RocketExplosion == false)
            {
                sourcePosition = sourcePosition + direction * speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                bulletRec = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, 15, 15);
            }
            DMGdAtRETA = 0;
            if (((CheckCollisionsProjectile(bulletRec) != new Rectangle(0, 0, 0, 0) || CheckCollisionsCharacters(bulletRec) != new Rectangle(0, 0, 0, 0)) || !IsInsideMap(sourcePosition)) && ammoType == AmmoType.cal32)
            {
                explosionScale = 0.05f;
                RifleExplosion = true;
            }
            if (CheckCollisionsProjectile(bulletRec) != new Rectangle(0, 0, 0, 0) && (ammoType == AmmoType.rocket || ammoType == AmmoType.hadouken))
            {
                explosionScale = 0.2f;
                RocketExplosion = true;
                characterhit = false;
            }
            if ((CheckCollisionsCharacters(bulletRec) != new Rectangle(0, 0, 0, 0) || !IsInsideMap(sourcePosition)) && (ammoType == AmmoType.rocket || ammoType == AmmoType.hadouken))
            {
                explosionScale = 0.2f;
                RocketExplosion = true;
                characterhit = true;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (RocketExplosion)
                spriteBatch.Draw(explosion[currentFrame1], new Vector2(bulletRec.X - 30, bulletRec.Y - 30), null, Color.White, 0f, new Vector2((float)5, (float)5), explosionScale, SpriteEffects.None, 0f);
            if (RifleExplosion)
                spriteBatch.Draw(explosion[currentFrame1], new Vector2(bulletRec.X, bulletRec.Y), null, Color.White, 0f, new Vector2((float)5, (float)5), explosionScale, SpriteEffects.None, 0f);
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
        public Rectangle CheckCollisionsProjectilefloor(Rectangle rect)
        {
            //List<Rectangle> collidingWith = new List<Rectangle>();
            Rectangle aux = new Rectangle(rect.X, rect.Y + 3, 15, 15);
            foreach (Rectangle rectangle in Collisions.tilesCollisions)
            {
                if (aux.Intersects(rectangle) && aux != rectangle)
                {
                    rec = new Vector2(rectangle.X, rectangle.Y);
                    return rectangle;
                }
            }
            return new Rectangle(0, 0, 0, 0);
        }

        public Rectangle CheckCollisionsCharacters(Rectangle rect)
        {
            foreach (Characters Character in Collisions.characterCollisions)
            {
                if (rect.Intersects(Character.getCharRec()) && rect != Character.getCharRec() && !Character.isActive())
                {
                    if (!Collisions.bulletsTagged.Contains(this))
                    {
                        Character.changeHp(-this.damage);
                        Collisions.bulletsTagged.Add(this);
                    }
                    rec = new Vector2(Character.getCharRec().X, Character.getCharRec().Y);
                    return Character.getCharRec();
                }
            }
            return new Rectangle(0, 0, 0, 0);
        }

        public void ExplosionTileRemove(Rectangle rect, Map map)
        {
            //List<Vector2> explosionrange = new List<Vector2>();
            //Vector2 comparison;
            map.DestroySquare(new Vector2(rect.X, rect.Y));
            map.DestroySquare(new Vector2(rect.X - 20, rect.Y));
            map.DestroySquare(new Vector2(rect.X - 40, rect.Y));
            map.DestroySquare(new Vector2(rect.X + 20, rect.Y));
            map.DestroySquare(new Vector2(rect.X + 40, rect.Y));
            map.DestroySquare(new Vector2(rect.X, rect.Y + 20));
            map.DestroySquare(new Vector2(rect.X, rect.Y - 20));
            map.DestroySquare(new Vector2(rect.X, rect.Y + 40));
            map.DestroySquare(new Vector2(rect.X, rect.Y - 40));
            map.DestroySquare(new Vector2(rect.X + 20, rect.Y + 20));
            map.DestroySquare(new Vector2(rect.X - 20, rect.Y - 20));
            map.DestroySquare(new Vector2(rect.X - 20, rect.Y + 20));
            map.DestroySquare(new Vector2(rect.X + 20, rect.Y - 20));

            //explosionrange.Add(aux1);
            //explosionrange.Add(aux2);
            //explosionrange.Add(aux3);
            //explosionrange.Add(aux4);
            //explosionrange.Add(aux5);
            //explosionrange.Add(aux6);
            //explosionrange.Add(aux7);
            //explosionrange.Add(aux8);
            //explosionrange.Add(aux9);
            //explosionrange.Add(aux10);
            //explosionrange.Add(aux11);
            //explosionrange.Add(aux12);

            //for (int i = Collisions.tilesCollisions.Count - 1; i >= 0; i--)
            //{
            //    comparison = new Vector2(Collisions.tilesCollisions[i].X, Collisions.tilesCollisions[i].Y);
            //    foreach (Vector2 vector in explosionrange)
            //    {
            //        if (comparison == vector)
            //        {
            //            //Map.alive[(int)vector.X, (int)vector.Y] = false;
            //            //Collisions.tilesCollisions.RemoveAt(i);
            //        }   
            //    }
            //}
        }
        public Rectangle getRectangle()
        {
            return this.bulletRec;
        }

        public int getDamage()
        {
            return this.damage;
        }

        static public float getFireRate(AmmoType ammo)
        {
            if (ammo == AmmoType.cal32)
                return 0.3f;
            if (ammo == AmmoType.rocket)
                return 0.6f;
            if (ammo == AmmoType.hadouken)
                return 0.1f;
            else return 0;
        }

        public void DontNeverDuwides(float scale)
        {
            this.explosionScale = scale;
        }

        public bool IsInsideMap(Vector2 position)
        {
            int MapSizeMaxX = 2900, MapSizeMinX = -350, MapSizeMaxY = 950, MapSizeMinY = -155;

            if (position.X > MapSizeMaxX - 20 || position.X < MapSizeMinX + 20 || position.Y > MapSizeMaxY - 20 || position.Y < MapSizeMinY) return false;
            else return true;
        }
    }
}
