using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class WeaponsHandler
    {
        static public List<Weapons> PoliceStationBasement = new List<Weapons>();
        private static int Rndp;

        static public void InitList(Characters CharacterActive, ContentManager content)
        {
            PoliceStationBasement.Add(new Weapons("Killerino", CharacterActive));
            
            
            //PoliceStationBasement.Add(new Weapons("Kiss My Ass", CharacterActive, WeaponType.ShotGun, 5, 5));
            //PoliceStationBasement.Add(new Weapons("LOL BOOM", CharacterActive, WeaponType.Rocket, 2, 30));
            //PoliceStationBasement.Add(new Weapons("KABOOM", CharacterActive, WeaponType.GrenadeLauncher, 4, 25));
        }

        static public Weapons RandomDrop()
        {
            Random Rnd = new Random();
            Rndp = Rnd.Next(0, 3);
            return PoliceStationBasement[Rndp];
        }

        static public Weapons GetWeapon(int Weapon)
        {
            return PoliceStationBasement[Weapon];
        }
    }
}
