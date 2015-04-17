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
        private float rotation;
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
                      float speed)
        {
            this.range = range;
            this.sourcePosition = sourcePosition;
            this.rotation = rotation;
            this.direction = new Vector2((float)Math.Cos(rotation),
                                         (float)Math.Sin(rotation));
            this.ammoType = ammo;
            this.speed = speed;
        }

        public void update(GameTime gameTime)
        {
            if (ammoType == AmmoType.cal32)
            {
                sourcePosition = sourcePosition + direction * speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                bulletRec = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, 15, 15);
            }
            else if (ammoType == AmmoType.rocket)
            {
                sourcePosition = sourcePosition + direction * speed * ((float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f);
                bulletRec = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, 15, 15);
            }
            else if (ammoType == AmmoType.nade) { }

            if (CheckCollisionsProjectile().Count != 0)
            {
                Game1.TesteMapa.DestroySquare(sourcePosition);
            }
            
        }

        public List<Rectangle> CheckCollisionsProjectile()
        {
            List<Rectangle> collidingWith = new List<Rectangle>();

            Rectangle rect = new Rectangle((int)Math.Round(sourcePosition.X), (int)Math.Round(sourcePosition.Y), 15, 15);

            foreach (Rectangle rectangle in Collisions.tilesCollisions)
            {
                if (rect.Intersects(rectangle) && rect != rectangle)
                {
                    collidingWith.Add(rectangle);
                }
            }
            return collidingWith;
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
