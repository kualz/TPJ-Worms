using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Worms_0._0._1.Weapons_and_projectiles;

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
        protected static int previousWeapon = 0;
        protected Vector2 PositionRelativeToCharacter;
        protected float rotation;
        protected Texture2D textura;
        private SpriteFont font;
        private Rectangle rec;
        protected bool activeState;
        protected int SerialNumber, weaponCodeChoosen = 0;
        protected int TextureWidth = 10, TextureWheight = 15;
        private List<Bullet> createdAmmo = new List<Bullet>();
        private Bullet nova = new Bullet();

        public Weapons() { }

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
            nova.load(content);
            textura = content.Load<Texture2D>(asset);
            font = content.Load<SpriteFont>("MyFont");
        }

        public void Update(GameTime gameTime, Characters Char)
        {
            Input.Update();
            MouseState mState = Mouse.GetState();
            PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X + 5, Char.CharacterPosition().Y);

            if(mState.LeftButton == ButtonState.Pressed)
            {
                nova.addAmmoToStack(new Bullet(this.PositionRelativeToCharacter, rotation, Bullet.AmmoType.cal32, 200));
            }
            if (Input.IsPressed(Keys.D1)){
                weaponCodeChoosen = 0;
                WeaponsHandler.GetWeapon(weaponCodeChoosen);
                previousWeapon = 0;
            }
            else if (Input.IsPressed(Keys.D2)){
                weaponCodeChoosen = 1;
                WeaponsHandler.GetWeapon(weaponCodeChoosen);
                previousWeapon = 1;
            }
            Point mousePos = mState.Position;
            float y = (float)mousePos.Y - PositionRelativeToCharacter.Y;
            float x = (float)PositionRelativeToCharacter.X - mousePos.X;
            float rot = (float)Math.Atan2(x,y);
            rot += (float)Math.PI/2f;
            rotation = rot;
            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
            nova.update(gameTime, Bullet.AmmoType.cal32);
            nova.Updatedeletebullet(PositionRelativeToCharacter);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.textura, PositionRelativeToCharacter, null , Color.White, this.rotation, new Vector2(5, 0), 1f, SpriteEffects.None, 0f);          
            spriteBatch.DrawString(font, "Weapon Name: " + WeaponsHandler._GetWeapon(weaponCodeChoosen).getName(), new Vector2(500f, 500f), Color.White);
            spriteBatch.DrawString(font, "Weapon Type: " + WeaponsHandler._GetWeapon(weaponCodeChoosen).getWeaponType().ToString(), new Vector2(500f, 525f), Color.White);

            spriteBatch.DrawString(font, "test\nPress 1 - first weapon" , new Vector2(200f, 550f), Color.White);
            spriteBatch.DrawString(font, "Press 2 - second weapon", new Vector2(200f, 600f), Color.White);
            nova.draw(spriteBatch, Bullet.AmmoType.cal32);
        }

        public string getName(){
            return this.Name;
        }

        public bool getWeaponState(){
            return this.activeState;
        }

        public void setWeaponState(){
            if (this.activeState == true)
                this.activeState = false;
            else this.activeState = true;
        }

        public WeaponType getWeaponType(){
            return this.WeaponTypes;
        }

        public int getSerialNumber(){
            return this.SerialNumber;
        }

        public float getRotation(){
            return rotation;
        }
    }
}
