using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.WeaponsAndProjectiles
{
    class magzzz
    {
        static List<Mag> Ammo = new List<Mag>();
        static int RifleAmmo = 6, RocketAmmo = 1, NadeAmmo = 2;

        public static void initializeAmmo()
        {
            Ammo.Add(new Mag(RifleAmmo));
            Ammo.Add(new Mag(RocketAmmo));
            Ammo.Add(new Mag(NadeAmmo));
        }

        /// <summary>
        /// ammoCode 0:Rifle
        /// ammoCode 1:Rocket
        /// ammoCode 2:Nade
        /// </summary>
        /// <param name="ammoCode"></param>
        /// <returns></returns>
        public static Mag getmagAt(int ammoCode){
                return Ammo[ammoCode];
        }

        /// <summary>
        /// Choose the mag to decrement!
        /// ammoCode 0:Rifle
        /// ammoCode 1:Rocket
        /// ammoCode 2:Nade
        /// </summary>
        /// <param name="ammoCode"></param>
        static public void decMag(int ammoCode){
            Ammo[ammoCode].decAmmo();
        }

        /// <summary>
        /// sets all Mag to initial state USED WHEN ACTIVE CHARACTER IS CHANGED
        /// </summary>
        static public void setAllMag()
        {
            Ammo[0].setMag(RifleAmmo);
            Ammo[1].setMag(RocketAmmo);
            Ammo[2].setMag(NadeAmmo);
        }
    }
}
