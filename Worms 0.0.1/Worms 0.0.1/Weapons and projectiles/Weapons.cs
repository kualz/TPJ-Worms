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
        }
        protected string Name;
        protected WeaponType WeaponTypes;
        protected static int previousWeapon = 0;
        protected Vector2 PositionRelativeToCharacter;
        protected float rotation;
        protected Texture2D textura;
        private Texture2D cross;
        private SpriteFont font;
        private Rectangle rec;
        protected bool activeState;
        protected int SerialNumber, weaponCodeChoosen = 0;
        protected int TextureWidth = 10, TextureWheight = 15;
        private List<Bullet> createdAmmo = new List<Bullet>();
        private Bullet ammunition = new Bullet();
        private Point mousePos;
        private int currentFrame = 0;
        private float fireRateTime = 0, timer, intervalo = 0.08f;
        private Random rnd;

        public Weapons() { }

        public Weapons(string name, Characters Char, WeaponType weaponType)
        {
            this.Name = name; 
            this.PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X, Char.CharacterPosition().Y);
            this.WeaponTypes = weaponType;
            rotation = 0f;
            this.activeState = false;
        }

        public void Load(ContentManager content, string asset)
        {
            ammunition.load(content);
            textura = content.Load<Texture2D>(asset);
            font = content.Load<SpriteFont>("MyFont");
        }

        public void Update(GameTime gameTime, Characters Char)
        {
            Input.Update();
            MouseState mState = Mouse.GetState();
            PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X + 5, Char.CharacterPosition().Y);
            mousePos = mState.Position;

            if (mState.LeftButton == ButtonState.Pressed)
            {
                if (fireRateTime >= ammunition.getFireRate(Bullet.AmmoType.cal32) && WeaponTypes == WeaponType.MachineGun)
                {
                    ammunition.addAmmoToStack(new Bullet(this.PositionRelativeToCharacter, (rotation + (getRandom())), Bullet.AmmoType.cal32, 400, 1350, this.PositionRelativeToCharacter));
                    fireRateTime = 0;
                }
                if (fireRateTime >= ammunition.getFireRate(Bullet.AmmoType.rocket) && WeaponTypes == WeaponType.Rocket)
                {
                    ammunition.addAmmoToStack(new Bullet(this.PositionRelativeToCharacter, (rotation + (getRandom())), Bullet.AmmoType.rocket, 200, 500, this.PositionRelativeToCharacter));
                    fireRateTime = 0;
                }
                else fireRateTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            float y = (float)mousePos.Y - PositionRelativeToCharacter.Y;
            float x = (float)PositionRelativeToCharacter.X - mousePos.X;
            float rot = (float)Math.Atan2(x,y);
            rot += (float)Math.PI/2f;
            rotation = rot;
            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
            ammunition.update(gameTime, Bullet.AmmoType.cal32);
            ammunition.UpdateDeletebullet();
        }

        public void Draw(SpriteBatch spriteBatch, Characters Char)
        {
            spriteBatch.Draw(this.textura, new Vector2(PositionRelativeToCharacter.X + 3, PositionRelativeToCharacter.Y + 7), null , Color.White, this.rotation, new Vector2((float)2.5,(float)2.5), 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Weapon Name: " + Name, new Vector2(500f, 500f), Color.White);
            spriteBatch.DrawString(font, "Weapon Type: " + WeaponTypes.ToString(), new Vector2(500f, 525f), Color.White);
            spriteBatch.DrawString(font, "test\nPress 1 - first weapon" , new Vector2(200f, 550f), Color.White);
            spriteBatch.DrawString(font, "Press 2 - second weapon", new Vector2(200f, 600f), Color.White);
            spriteBatch.DrawString(font, "Press 3 - Third weapon", new Vector2(200f, 625f), Color.White);
            if (WeaponTypes == WeaponType.MachineGun)
                ammunition.draw(spriteBatch, Bullet.AmmoType.cal32);
            if (WeaponTypes == WeaponType.Rocket)
                ammunition.draw(spriteBatch, Bullet.AmmoType.rocket);

            //spriteBatch.Draw(this.textura, new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 5, 5), Color.Red);
        }

        public float getRandom()
        {
            rnd = new Random(Guid.NewGuid().GetHashCode());
            float random = -0.01f;
            float random1 = 0.01f;
            float random2 = 0.03f;
            float random3 = -0.03f;
            float random4 = 0.07f;
            float random5 = -0.07f;
            float random6 = 0.05f;
            float random7 = -0.05f;
            float random8 = 0.06f;
            int random9 = rnd.Next(0, 4);
            if (random9 == 0) return random;
            else if (random9 == 1) return random1;
            else if (random9 == 2) return random2;
            else if (random9 == 3) return random3;
            else if (random9 == 3) return random4;
            else if (random9 == 3) return random5;
            else if (random9 == 3) return random6;
            else if (random9 == 3) return random7;
            else return random8;
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
