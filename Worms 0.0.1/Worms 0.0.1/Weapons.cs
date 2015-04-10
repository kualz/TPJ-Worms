using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class Weapons
    {
        private enum WeaponType
        {
            Rocket,
            MachineGun,
            GrenadeLauncher,
            ShotGun
        }
        private WeaponType WeaponTypes;
        private int Magazine;
        private float range;
        private Vector2 PositionRelativeToCharacter;
        //MUDAR ISTO RELATIVAMENTE AS TEXTURAS!
        private int TextureWidth = 20, TextureWheight = 40;
        //==================================================

        private Weapons(Characters Char, WeaponType weaponType, int magazine, float range)
        {
            this.PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X + (TextureWidth - 2), Char.CharacterPosition().Y - (this.TextureWheight/2));
            this.WeaponTypes = weaponType;
            this.Magazine = magazine;
            this.range = range;
        }


    }
}
