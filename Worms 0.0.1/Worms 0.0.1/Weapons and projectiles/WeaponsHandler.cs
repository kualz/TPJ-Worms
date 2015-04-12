using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class WeaponsHandler : Weapons
    {
        static public List<Weapons> PoliceStationBasement = new List<Weapons>();
        static private List<string> NamesAndStuff = new List<string>();
        private static int Rndp;

        static public void InitList(Characters CharacterActive, ContentManager content)
        {
            NamesAndStuff.Add("Weapon_name_not_Found");
            NamesAndStuff.Add("Killerino");
            NamesAndStuff.Add("Kiss My Ass");
            NamesAndStuff.Add("LOL BOOM");
            NamesAndStuff.Add("KABOOM");


            PoliceStationBasement.Add(new Weapons(NamesAndStuff[1], 1, CharacterActive, WeaponType.MachineGun, Projectiles.AmmoType.cal22));
            PoliceStationBasement.Add(new Weapons(NamesAndStuff[2], 5, CharacterActive, WeaponType.ShotGun, Projectiles.AmmoType.chell));
            PoliceStationBasement.Add(new Weapons(NamesAndStuff[3], 3, CharacterActive, WeaponType.Rocket, Projectiles.AmmoType.rocket));
            PoliceStationBasement.Add(new Weapons(NamesAndStuff[4], 4, CharacterActive, WeaponType.GrenadeLauncher, Projectiles.AmmoType.nade));
            PoliceStationBasement[0].setWeaponState();
        }

        static public Weapons RandomDrop(){
            Random Rnd = new Random();
            Rndp = Rnd.Next(0, 3);
            return PoliceStationBasement[Rndp];
        }

        static public Weapons _GetWeapon(int weapon){
            return PoliceStationBasement[weapon];
        }

        static public Weapons getActiveWeapon(){
            for (int i = 0; i < PoliceStationBasement.Count(); i++){
                if (PoliceStationBasement[i].getWeaponState() == true)
                    return PoliceStationBasement[i];
            }
            return null;
        }

        static public Weapons GetWeapon(int Weapon)
        {
            PoliceStationBasement[previousWeapon].setWeaponState();
            PoliceStationBasement[Weapon].setWeaponState();
            return PoliceStationBasement[Weapon];
        }
    }
}
