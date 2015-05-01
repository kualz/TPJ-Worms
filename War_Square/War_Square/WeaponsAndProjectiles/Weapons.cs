using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using War_Square.WeaponsAndProjectiles;
using War_Square.characters;

namespace War_Square.WeaponsAndProjectiles
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
        private SpriteFont font;
        private Rectangle rec;
        protected bool activeState, justflippedright = true, justflippedleft = false;
        protected int weaponCodeChoosen = 0, helperX = 0, helperXpos = 0, helperXCharPos = 0, helperYCharPos = 0;
        protected int TextureWidth = 10, TextureWheight = 15;
        private List<Bullet> createdAmmo = new List<Bullet>();
        private Bullet ammunition = new Bullet();
        private Point mousePos;
        private int currentFrame = 0, currentFrame1 = 0;
        private float fireRateTime = 0.3f, timer, timerExplosion, intervalo = 0.05f;
        private Random rnd;
        public List<Bullet> bulletsOnScreen = new List<Bullet>();
        private List<string> names = new List<string>();
        private Texture2D[] flashFiring;
        private Texture2D[] explosion;
        private Texture2D texturax;
        private Texture2D texturasRocket;
        private Texture2D flatSquare;
        private Vector2 auxVector;
        public bool Sexplosion = false;
        float rot = (float)Math.PI;
        public SpriteEffects auxflip, lasteffect = SpriteEffects.None;

        public Weapons() { }

        public Weapons(string name, Characters Char, WeaponType weaponType, bool state, int Mag)
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
            MouseState mState = Mouse.GetState();
            PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X, Char.CharacterPosition().Y);
            mousePos = mState.Position;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += deltaTime;
            timerExplosion += deltaTime;

            if (timer >= intervalo)
            {
                currentFrame++;
                if (currentFrame >= (6))
                    currentFrame = 0;
                timer = 0;
            }
            if (timerExplosion >= intervalo)
            {
                currentFrame1++;
                if (currentFrame1 >= (9))
                    currentFrame1 = 0;
                timerExplosion = 0;
            }
            if (Input.IsDown(Keys.Space))
            {
                if (fireRateTime >= ammunition.getFireRate(Bullet.AmmoType.cal32) && WeaponTypes == WeaponType.MachineGun)
                {
                    if(magzzz.getmagAt(0).getMag() > 0)
                        bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector, (rotation + (getRandom())), Bullet.AmmoType.cal32, 300, 800));
                    magzzz.decMag(0);
                    fireRateTime = 0;
                }
                if (fireRateTime >= ammunition.getFireRate(Bullet.AmmoType.rocket) && WeaponTypes == WeaponType.Rocket)
                {
                    if (magzzz.getmagAt(1).getMag() > 0)
                        bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector, (rotation + (getRandom())), Bullet.AmmoType.rocket, 20000, 500));
                    magzzz.decMag(1);
                    fireRateTime = 0;
                }
                if (fireRateTime >= ammunition.getFireRate(Bullet.AmmoType.nade) && WeaponTypes == WeaponType.GrenadeLauncher)
                {
                    if (magzzz.getmagAt(2).getMag() > 0)
                        bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector,(rotation + (getRandom())),Bullet.AmmoType.nade, 500, 150));
                    magzzz.decMag(2);
                    fireRateTime = 0;
                }
                else fireRateTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && auxflip == SpriteEffects.FlipHorizontally && rot - 0.1f > -(float)Math.PI / 2)
            {
                rot -= (float)Math.PI / 100f;
                justflippedleft = false;
                justflippedright = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && auxflip == SpriteEffects.FlipHorizontally &&  rot + 0.1f < (float)Math.PI / 2  )
            {
                rot += (float)Math.PI / 100f;
                justflippedleft = false;
                justflippedright = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && auxflip == SpriteEffects.None &&  rot + 0.1f < 3 * (float)Math.PI / 2)
            {
                rot += (float)Math.PI / 100f;
                justflippedleft = false;
                justflippedright = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && auxflip == SpriteEffects.None && rot - 0.1f > (float)Math.PI / 2)
            {
                rot -= (float)Math.PI / 100f;
                justflippedleft = false;
                justflippedright = false;
            }


            if (justflippedright) rot = 0;
            if (justflippedleft) rot = (float)Math.PI;

            if (rot >= 2 * (float)Math.PI) rot = 0;

            rotation = rot;
            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
            updateBullets(gameTime);
            updateDeleteBullets(gameTime);
           
        }

        public void Draw(SpriteBatch spriteBatch, Characters ActiveChar, SpriteEffects flip)
        {           
            auxVector = new Vector2(helperXCharPos, helperYCharPos);
            spriteBatch.Draw(this.textura, new Vector2(ActiveChar.CharacterPosition().X + helperXCharPos + 10, ActiveChar.CharacterPosition().Y + 45), null, Color.White, this.rotation + (float)Math.PI / 2, new Vector2((float)45 + helperX, (float)40), 1f, flip, 0f);

            auxflip = flip;

            if (flip == SpriteEffects.FlipHorizontally)
            {
                helperX = 0;
                helperXpos = 0;
                helperXCharPos = 0;
                helperYCharPos = 35;
            }

            if (flip == SpriteEffects.None)
            {
                helperX = -20;
                helperXpos = 35;
                helperXCharPos = 30;
                helperYCharPos = 35;
            }

            if (lasteffect == SpriteEffects.None && flip == SpriteEffects.FlipHorizontally)
            {
                justflippedleft = false;
                justflippedright = true;
            }
            if (lasteffect == SpriteEffects.FlipHorizontally && flip == SpriteEffects.None)
            {
                justflippedleft = true;
                justflippedright = false;
            }
            foreach (Bullet bullet in bulletsOnScreen)
            {
                if (bullet.ammoType == Bullet.AmmoType.cal32)
                {
                    if (fireRateTime < ammunition.getFireRate(Bullet.AmmoType.cal32))
                        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------pus este 260 a sorte e deu GG--------------------
                        spriteBatch.Draw(flashFiring[currentFrame], new Vector2(this.PositionRelativeToCharacter.X + 5 + helperXpos, this.PositionRelativeToCharacter.Y + 45), null, Color.White, rotation, new Vector2((float)0, (float)260), .15f, SpriteEffects.None, 0f);
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


            lasteffect = flip;
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

        public string getName()
        {
            return this.Name;
        }

        public bool getWeaponState()
        {
            return this.activeState;
        }

        public void setWeaponState()
        {
            if (this.activeState == true)
                this.activeState = false;
            else this.activeState = true;
        }

        public WeaponType getWeaponType()
        {
            return this.WeaponTypes;
        }

        public float getRotation()
        {
            return rotation;
        }

        public void updateBullets(GameTime gameTime)
        {
            for (int i = bulletsOnScreen.Count - 1; i >= 0; i--)
            {
                bulletsOnScreen[i].update(gameTime, this);
            }
        }

        public void updateDeleteBullets(GameTime gameTime)
        {
            for (int i = 0; i < bulletsOnScreen.Count; i++)
            {
                if ((bulletsOnScreen[i].sourcePosition - this.PositionRelativeToCharacter).Length() > bulletsOnScreen[i].range)
                    bulletsOnScreen.Remove(bulletsOnScreen[i]);
            }
        }
    }
}
