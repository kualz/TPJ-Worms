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
        private Texture2D cross;
        private SpriteFont font;
        private Rectangle rec;
        protected bool activeState;
        protected int SerialNumber, weaponCodeChoosen = 0;
        protected int TextureWidth = 10, TextureWheight = 15;
        private List<Bullet> createdAmmo = new List<Bullet>();
        private Bullet nova = new Bullet();
        private Point mousePos;
        private int currentFrame = 0;
        private float fireRateTime = 0, timer, intervalo = 0.08f;

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
            cross =content.Load<Texture2D>("cross");
            textura = content.Load<Texture2D>(asset);
            font = content.Load<SpriteFont>("MyFont");
        }

        public void Update(GameTime gameTime, Characters Char)
        {
            Input.Update();
            MouseState mState = Mouse.GetState();
            PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X + 5, Char.CharacterPosition().Y);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += deltaTime;

            //if (timer > intervalo)
            //{
            //    currentFrame++;
            //    if(currentFrame >= 3){
            //        currentFrame = 0;
            //    }
            //    timer = 0;
            //}


            if(mState.LeftButton == ButtonState.Pressed)
            {
                if (fireRateTime >= nova.getFireRate(Bullet.AmmoType.cal32) && WeaponsHandler.getActiveWeapon().getWeaponType() == WeaponType.MachineGun)
                {
                    nova.addAmmoToStack(new Bullet(this.PositionRelativeToCharacter, rotation, Bullet.AmmoType.cal32, 400, 1350, this.PositionRelativeToCharacter));
                    fireRateTime = 0;
                }
                else fireRateTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            mousePos = mState.Position;
            float y = (float)mousePos.Y - PositionRelativeToCharacter.Y;
            float x = (float)PositionRelativeToCharacter.X - mousePos.X;
            float rot = (float)Math.Atan2(x,y);
            rot += (float)Math.PI/2f;
            rotation = rot;
            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
            nova.update(gameTime, Bullet.AmmoType.cal32);
            nova.UpdateDeletebullet();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cross, new Vector2(mousePos.X, mousePos.Y), Color.White);
            spriteBatch.Draw(this.textura, new Vector2(PositionRelativeToCharacter.X + 3, PositionRelativeToCharacter.Y + 7), null , Color.White, this.rotation, new Vector2((float)2.5,(float)2.5), 1f, SpriteEffects.None, 0f);          
            spriteBatch.DrawString(font, "Weapon Name: " + WeaponsHandler._GetWeapon(weaponCodeChoosen).getName(), new Vector2(500f, 500f), Color.White);
            spriteBatch.DrawString(font, "Weapon Type: " + WeaponsHandler._GetWeapon(weaponCodeChoosen).getWeaponType().ToString(), new Vector2(500f, 525f), Color.White);

            spriteBatch.DrawString(font, "test\nPress 1 - first weapon" , new Vector2(200f, 550f), Color.White);
            spriteBatch.DrawString(font, "Press 2 - second weapon", new Vector2(200f, 600f), Color.White);
            nova.draw(spriteBatch, Bullet.AmmoType.cal32, currentFrame);
            //spriteBatch.Draw(this.textura, new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 5, 5), Color.Red);
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
