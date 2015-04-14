using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class WeaponsHandler : Weapons
    {
        static public List<Weapons> KualzWeapons = new List<Weapons>();
        static public List<Weapons> PhaktumnWeapons = new List<Weapons>();
        static public List<Weapons> KlliperWeapons = new List<Weapons>();
        static private List<string> NamesAndStuff = new List<string>();
        private static int Rndp;

        static public void InitList(Characters CharacterActive, ContentManager content)
        {
            NamesAndStuff.Add("Weapon_name_not_Found");
            NamesAndStuff.Add("Killerino");
            NamesAndStuff.Add("LOL BOOM");
            NamesAndStuff.Add("KABOOM");

            if (CharacterActive.returnName() == "Kualz")
            {
                KualzWeapons.Add(new Weapons(NamesAndStuff[1], CharacterActive, WeaponType.MachineGun));
                KualzWeapons.Add(new Weapons(NamesAndStuff[2], CharacterActive, WeaponType.Rocket));
                KualzWeapons.Add(new Weapons(NamesAndStuff[3], CharacterActive, WeaponType.GrenadeLauncher));
                KualzWeapons[0].setWeaponState();
            }
            if (CharacterActive.returnName() == "Phakrumn")
            {
                PhaktumnWeapons.Add(new Weapons(NamesAndStuff[1], CharacterActive, WeaponType.MachineGun));
                PhaktumnWeapons.Add(new Weapons(NamesAndStuff[2], CharacterActive, WeaponType.Rocket));
                PhaktumnWeapons.Add(new Weapons(NamesAndStuff[3], CharacterActive, WeaponType.GrenadeLauncher));
                PhaktumnWeapons[0].setWeaponState();
            }
            if (CharacterActive.returnName() == "Klipper")
            {
                KlliperWeapons.Add(new Weapons(NamesAndStuff[1], CharacterActive, WeaponType.MachineGun));
                KlliperWeapons.Add(new Weapons(NamesAndStuff[2], CharacterActive, WeaponType.Rocket));
                KlliperWeapons.Add(new Weapons(NamesAndStuff[3], CharacterActive, WeaponType.GrenadeLauncher));
                KlliperWeapons[0].setWeaponState();
            }
        }

        static public Weapons RandomDrop(Characters CharacterActive)
        {
            Random Rnd = new Random();
            Rndp = Rnd.Next(0, 3);
            if (CharacterActive.returnName() == "Kualz")
                return KualzWeapons[Rndp];
            if (CharacterActive.returnName() == "Phakrumn")
                return PhaktumnWeapons[Rndp];
            if (CharacterActive.returnName() == "Klipper")
                return KlliperWeapons[Rndp];
            else return null;
        }

        static public Weapons _GetWeapon(Characters CharacterActive, int weapon){
            if (CharacterActive.returnName() == "Kualz")
                return KualzWeapons[weapon];
            if (CharacterActive.returnName() == "Phakrumn")
                return PhaktumnWeapons[weapon];
            if (CharacterActive.returnName() == "Klipper")
                return KlliperWeapons[weapon];
            else return null;
        }

        static public Weapons getActiveWeapon(Characters CharacterActive)
        {
            if (CharacterActive.returnName() == "Kualz"){
                for (int i = 0; i < KualzWeapons.Count(); i++){
                    if (KualzWeapons[i].getWeaponState() == true)
                        return KualzWeapons[i];
                }
                return null;
            }
            if (CharacterActive.returnName() == "Phakrumn"){
                for (int i = 0; i < PhaktumnWeapons.Count(); i++){
                    if (PhaktumnWeapons[i].getWeaponState() == true)
                        return PhaktumnWeapons[i];
                }
                return null;
            }
            if (CharacterActive.returnName() == "Klipper"){
                for (int i = 0; i < KlliperWeapons.Count(); i++){
                    if (KlliperWeapons[i].getWeaponState() == true)
                        return KlliperWeapons[i];
                }
                return null;
            }
            else return null;
        }

        static public Weapons GetWeapon(Characters CharacterActive, int Weapon)
        {
            if (CharacterActive.returnName() == "Kualz"){
                KualzWeapons[previousWeapon].setWeaponState();
                KualzWeapons[Weapon].setWeaponState();
                return KualzWeapons[Weapon];
            }
            if (CharacterActive.returnName() == "Phakrumn"){
                PhaktumnWeapons[previousWeapon].setWeaponState();
                PhaktumnWeapons[Weapon].setWeaponState();
                return PhaktumnWeapons[Weapon];
            }
            if (CharacterActive.returnName() == "Klipper")
            {
                KlliperWeapons[previousWeapon].setWeaponState();
                KlliperWeapons[Weapon].setWeaponState();
                return KlliperWeapons[Weapon];
            }
            else return null;
        }
    }
}
