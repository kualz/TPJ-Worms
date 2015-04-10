using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class Weapons
    {
        protected enum WeaponType
        {
            Rocket,
            MachineGun,
            GrenadeLauncher,
            ShotGun
        }
        private string Name;
        protected WeaponType WeaponTypes;
        protected int Magazine;
        protected float range;
        protected Vector2 PositionRelativeToCharacter;
        protected MouseState Mouse = new MouseState();
        protected float rotation;
        private Texture2D textura;
        //MUDAR ISTO RELATIVAMENTE AS TEXTURAS!
        /// <summary>
        ///basicamente ver o tamanho das texturas dos characters!!!
        /// </summary>
        private int TextureWidth = 20, TextureWheight = 40;
        //==================================================

        public Weapons(string name, Characters Char, WeaponType weaponType, int magazine, float range)
        {
            this.Name = name; 
            this.PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X + (TextureWidth - 2), Char.CharacterPosition().Y - (this.TextureWheight/2));
            this.WeaponTypes = weaponType;
            this.Magazine = magazine;
            this.range = range;
            rotation = 0;
        }

        public void Load()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {

        }


    }
}
