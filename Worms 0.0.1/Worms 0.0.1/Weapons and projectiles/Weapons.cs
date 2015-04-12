using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Worms_0._0._1
{
    class Weapons
    {
        public enum WeaponType
        {
            Rocket,
            MachineGun,
            GrenadeLauncher,
            ShotGun
        }
        protected string Name;
        protected WeaponType WeaponTypes;
        protected int Magazine;
        protected float range;
        protected Vector2 PositionRelativeToCharacter;
        protected float rotation;

        protected Texture2D textura;
        private SpriteFont font;

        private Rectangle rec;
        protected bool activeState;
        protected int SerialNumber, weaponCodeChoosen = 0;
        //MUDAR ISTO RELATIVAMENTE AS TEXTURAS!
        /// <summary>
        ///basicamente ver o tamanho das texturas dos characters!!!
        /// </summary>
        protected int TextureWidth = 10, TextureWheight = 15;
        //==================================================

        public Weapons()
        {

        }

        public Weapons(string name, int serialNumber, Characters Char, WeaponType weaponType)
        {
            this.SerialNumber = serialNumber;
            this.Name = name; 
            this.PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X, Char.CharacterPosition().Y);
            this.WeaponTypes = weaponType;
            rotation = 0f;
            this.activeState = false;
        }

        public void Load(ContentManager content, string asset)
        {
            textura = content.Load<Texture2D>(asset);
            font = content.Load<SpriteFont>("MyFont");
        }

        public void Update(GameTime gameTime, Characters Char)
        {
            PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X, Char.CharacterPosition().Y); ;

            if (Keyboard.GetState().IsKeyDown(Keys.D1)){
                weaponCodeChoosen = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D2)){
                weaponCodeChoosen = 1;
            }
               
            //rever
            MouseState mState = Mouse.GetState();

            //x y rato
            Point mousePos = mState.Position;
            float y = (float)mousePos.Y - PositionRelativeToCharacter.Y;
            float x = (float)PositionRelativeToCharacter.X - mousePos.X;
            float rot = (float)Math.Atan2(x,y);
            rot += (float)Math.PI/2f;
            rotation = rot;
            //rever

            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.textura, PositionRelativeToCharacter, null , Color.White, this.rotation, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Weapon Name: " + WeaponsHandler.GetWeapon(weaponCodeChoosen).getName(), new Vector2(500f, 500f), Color.White);
            spriteBatch.DrawString(font, "Weapon Type: " + WeaponsHandler.GetWeapon(weaponCodeChoosen).getWeaponType().ToString(), new Vector2(500f, 525f), Color.White);

            spriteBatch.DrawString(font, "test\nPress 1 - first weapon" , new Vector2(200f, 550f), Color.White);
            spriteBatch.DrawString(font, "Press 2 - second weapon", new Vector2(200f, 600f), Color.White);
        }

        public string getName(){
            return this.Name;
        }

        public bool getWeaponState(){
            return this.activeState;
        }

        public void setWeaponState(){
            if (this.activeState == false)
                this.activeState = true;
            else this.activeState = false;
        }

        public WeaponType getWeaponType(){
            return this.WeaponTypes;
        }

        public int getSerialNumber(){
            return this.SerialNumber;
        }
    }
}
