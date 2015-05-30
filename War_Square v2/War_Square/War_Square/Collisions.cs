using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using War_Square.characters;
using War_Square.WeaponsAndProjectiles;

namespace War_Square
{
    class Collisions
    {
        public static List<Rectangle> tilesCollisions { get; protected set; }
        public static List<Rectangle> bulletCollisions { get; protected set; }
        public static List<Characters> characterCollisions { get; protected set; }
        public static List<Bullet> bulletsOnScreen = new List<Bullet>();
        public static List<Bullet> bulletsTagged = new List<Bullet>();

        static Collisions()
        {
            tilesCollisions = new List<Rectangle>();
            bulletCollisions = new List<Rectangle>();
            characterCollisions = new List<Characters>();
        }

        public static void reset()
        {
            tilesCollisions.Clear();
            bulletCollisions.Clear();
            characterCollisions.Clear();
        }
    }
}
