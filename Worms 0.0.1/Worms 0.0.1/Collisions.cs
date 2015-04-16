using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class Collisions
    {
        public static List<Rectangle> tilesCollisions { get; protected set; }
        public static List<Rectangle> bulletCollisions { get; protected set; }
        public static List<Rectangle> characterCollisions { get; protected set; }

        static Collisions()
        {
            tilesCollisions = new List<Rectangle>();
            bulletCollisions = new List<Rectangle>();
            characterCollisions = new List<Rectangle>();
        }

        public static void reset()
        {
            tilesCollisions.Clear();
            bulletCollisions.Clear();
            characterCollisions.Clear();
        }
    }
}
