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
        protected int SerialNumber, weaponCodeChoosen = 0, helperX = 0, helperXpos = 0, helperXCharPos = 0, helperYCharPos = 0;
        protected int TextureWidth = 10, TextureWheight = 15;
        private List<Bullet> createdAmmo = new List<Bullet>();
        private Bullet ammunition = new Bullet();
        private Point mousePos;
        private int currentFrame = 0, currentFrame1 = 0;
        private float fireRateTime = 0, fireRateTime1 = 0, timer, timerExplosion, intervalo = 0.05f;
        private Random rnd;
        public List<Bullet> bulletsOnScreen = new List<Bullet>();
        private List<string> names = new List<string>();
        private Texture2D[] flashFiring;
        private Texture2D[] explosion;
        private Texture2D texturax;
        private Texture2D texturasRocket;
        private Texture2D flatSquare;
        private Vector2 auxVector;
        static public bool Sexplosion = false;
        float rot = (float)Math.PI;
        private SpriteEffects flip;

        public Weapons() { }

        public Weapons(string name, Characters Char, WeaponType weaponType, bool state)
        {
            this.Name = name; 
            this.PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X, Char.CharacterPosition().Y);
            this.WeaponTypes = weaponType;
            rotation = 0f;
            this.activeState = false;
        }

        public void Load(ContentManager content, string asset)
        {
            flashFiring = new Texture2D[6];
            flashFiring[0] = content.Load<Texture2D>("flash_d_0001");
            flashFiring[1] = content.Load<Texture2D>("flash_d_0002");
            flashFiring[2] = content.Load<Texture2D>("flash_d_0003");
            flashFiring[3] = content.Load<Texture2D>("flash_d_0004");
            flashFiring[4] = content.Load<Texture2D>("flash_d_0005");
            flashFiring[5] = content.Load<Texture2D>("flash_d_0006");

            explosion = new Texture2D[9];
            explosion[0] = content.Load<Texture2D>("fireball_hit_0001");
            explosion[1] = content.Load<Texture2D>("fireball_hit_0002");
            explosion[2] = content.Load<Texture2D>("fireball_hit_0003");
            explosion[3] = content.Load<Texture2D>("fireball_hit_0004");
            explosion[4] = content.Load<Texture2D>("fireball_hit_0005");
            explosion[5] = content.Load<Texture2D>("fireball_hit_0006");
            explosion[6] = content.Load<Texture2D>("fireball_hit_0007");
            explosion[7] = content.Load<Texture2D>("fireball_hit_0008");
            explosion[8] = content.Load<Texture2D>("fireball_hit_0009");

            flatSquare = content.Load<Texture2D>("1"); 

            names.Add("teste_Projetil1");
            names.Add("teste_Projetil2");
            texturax = content.Load<Texture2D>(names[0]);
            texturasRocket = content.Load<Texture2D>(names[1]);
            textura = content.Load<Texture2D>(asset);
            font = content.Load<SpriteFont>("MyFont");
        }

        public void Update(GameTime gameTime, Characters Char)
        {
            Input.Update();
            MouseState mState = Mouse.GetState();
            PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X, Char.CharacterPosition().Y);
            mousePos = mState.Position;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += deltaTime;
            timerExplosion += deltaTime;

            if (timer >= intervalo){
                currentFrame++;
                if (currentFrame >= (6))
                    currentFrame = 0;
                timer = 0;
            }
            if (timerExplosion >= intervalo){
                currentFrame1++;
                if (currentFrame1 >= (9))
                    currentFrame1 = 0;
                timerExplosion = 0;
            }

            if (Input.IsDown(Keys.Space))
            {
                if (fireRateTime >= ammunition.getFireRate(Bullet.AmmoType.cal32) && WeaponTypes == WeaponType.MachineGun)
                {
                    bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector, (rotation + (getRandom())), Bullet.AmmoType.cal32, 300, 800));
                    fireRateTime = 0;
                }
                if (fireRateTime >= ammunition.getFireRate(Bullet.AmmoType.rocket) && WeaponTypes == WeaponType.Rocket)
                {
                    bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector, (rotation + (getRandom())), Bullet.AmmoType.rocket, 200, 500));
                    fireRateTime = 0;
                }
                if (fireRateTime >= ammunition.getFireRate(Bullet.AmmoType.nade) && WeaponTypes == WeaponType.GrenadeLauncher)
                {
                    bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector, (rotation + (getRandom())), Bullet.AmmoType.nade, 500, 150));
                    fireRateTime = 0;
                }
                else fireRateTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            //float y = (float)mousePos.Y - (PositionRelativeToCharacter.Y + helperYCharPos);
            //float x = (float)(PositionRelativeToCharacter.X + helperXCharPos) - mousePos.X;
            //float rot = (float)Math.Atan2(x,y);

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && rotation + 0.1f < 3* (float)Math.PI / 2f)
            {
                rot += (float)Math.PI / 100f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && rotation - 0.1f > (float)Math.PI / 2f)
            {
                rot -= (float)Math.PI / 100f;
            }
            

            rotation = rot;
            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
            updateBullets(gameTime);
            updateDeleteBullets(gameTime);

          
            
        }

        public void Draw(SpriteBatch spriteBatch, Characters ActiveChar, SpriteEffects flip)
        {
            if (flip == SpriteEffects.FlipHorizontally)
            {
                //rot = 0;
                helperX = 0;
                helperXpos = 0;
                helperXCharPos = 0;
                helperYCharPos = 35;
            }

            if (flip == SpriteEffects.None)
            {
                //rot = (float)Math.PI;
                helperX = -20;
                helperXpos = 35;
                helperXCharPos = 30;
                helperYCharPos = 35;
            }

            auxVector = new Vector2(helperXCharPos, helperYCharPos);
            spriteBatch.Draw(this.textura, new Vector2(ActiveChar.CharacterPosition().X + 5 + helperXpos, ActiveChar.CharacterPosition().Y + 45), null, Color.White, this.rotation+(float)Math.PI/2, new Vector2((float)45 + helperX, (float)40), 1f, flip, 0f);
            spriteBatch.DrawString(font, "Weapon Name: " + CharactersHandler.getActiveWeapon().getName(), new Vector2(500f, 500f), Color.White);
            spriteBatch.DrawString(font, "Weapon Type: " + CharactersHandler.getActiveWeapon().getWeaponType(), new Vector2(500f, 525f), Color.White);
            spriteBatch.DrawString(font, "test\nPress 1 - first weapon" , new Vector2(200f, 550f), Color.White);
            spriteBatch.DrawString(font, "Press 2 - second weapon", new Vector2(200f, 600f), Color.White);
            spriteBatch.DrawString(font, "Press 3 - Third weapon", new Vector2(200f, 625f), Color.White);
            foreach (Bullet bullet in bulletsOnScreen)
            {
                if (bullet.ammoType == Bullet.AmmoType.cal32)
                {
                    if (fireRateTime < ammunition.getFireRate(Bullet.AmmoType.cal32))
                        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------pus este 260 a sorte e deu GG--------------------
                        spriteBatch.Draw(flashFiring[currentFrame], new Vector2(this.PositionRelativeToCharacter.X + 5 + helperXpos , this.PositionRelativeToCharacter.Y + 45), null, Color.White, rotation, new Vector2((float)0, (float)260), .15f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(texturax, new Vector2(bullet.sourcePosition.X, bullet.sourcePosition.Y + 10), null, Color.White, rotation, new Vector2((float)5, (float)2.5), 1f, SpriteEffects.None, 0f);              
                }
                else if (bullet.ammoType == Bullet.AmmoType.rocket)
                    spriteBatch.Draw(texturasRocket, new Vector2(bullet.sourcePosition.X, bullet.sourcePosition.Y + 7), null, Color.White, rotation, new Vector2((float)5, (float)3.5), 1f, SpriteEffects.None, 0f);
                else if (bullet.ammoType == Bullet.AmmoType.nade)
                { }
            }
            if (Sexplosion)
            {
                spriteBatch.Draw(explosion[currentFrame1], new Vector2(Bullet.rec.X, Bullet.rec.Y), null, Color.White, 0f, new Vector2((float)5, (float)5), 0.05f, SpriteEffects.None, 0f);
            }
            Sexplosion = false;
            //spriteBatch.Draw(flatSquare, new Vector2(PositionRelativeToCharacter.X + helperXCharPos, PositionRelativeToCharacter.Y + helperYCharPos), Color.White);
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

        public void updateBullets(GameTime gameTime)
        {
            for (int i = bulletsOnScreen.Count - 1; i >= 0; i--)
            {
                bulletsOnScreen[i].update(gameTime, this);
            }
        }

        public void updateDeleteBullets(GameTime gameTime){
            for (int i = 0; i < bulletsOnScreen.Count; i++)
            {
                if ((bulletsOnScreen[i].sourcePosition - this.PositionRelativeToCharacter).Length() > bulletsOnScreen[i].range)
                    bulletsOnScreen.Remove(bulletsOnScreen[i]);
            }
        }
    }
}
