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
using War_Square.Sounds;

namespace War_Square.WeaponsAndProjectiles
{
    class Weapons
    {
        public enum WeaponType
        {
            Rocket,
            MachineGun,
            Hadouken,
            AirStrike,
            HailOfArrows,
            NoblePhantom,
            Bombardement
        }
        protected string Name;
        protected WeaponType WeaponTypes;
        protected static int previousWeapon = 0;
        protected Vector2 PositionRelativeToCharacter;
        protected float rotation, deltatimeNoble = 0;
        protected Texture2D textura;
        private SpriteFont font;
        private Rectangle rec;
        protected bool activeState, justflippedright = true, justflippedleft = false, NoblePhantomAux = false, BombardementAux = false, mira = false;
        protected int weaponCodeChoosen = 0, helperX = 0, helperXpos = 0, helperXCharPos = 0, helperYCharPos = 0;
        protected int TextureWidth = 10, TextureWheight = 15;
        private List<Bullet> createdAmmo = new List<Bullet>();
        private Point mousePos;
        private int currentFrame = 0;
        private float fireRateTime = 0.3f, timer, timerExplosion, intervalo = 0.05f;
        private Random rnd;
        private List<string> names = new List<string>();
        private Texture2D[] flashFiring;
        private Texture2D texturax, texturasRocket;
        private Texture2D flatSquare;
        private Vector2 auxVector;
        float rot = (float)Math.PI;
        public SpriteEffects auxflip, lasteffect = SpriteEffects.None;
        private Texture2D[] explosion;

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

            flatSquare = content.Load<Texture2D>("1");

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

            names.Add("teste_Projetil1");
            names.Add("teste_Projetil2");
            texturax = content.Load<Texture2D>(names[0]);
            texturasRocket = content.Load<Texture2D>("airdrop");
            textura = content.Load<Texture2D>(asset);
            font = content.Load<SpriteFont>("MyFont");
        }

        public void Update(GameTime gameTime, Characters Char, SpriteEffects flip)
        {
            MouseState mState = Mouse.GetState();
            PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X, Char.CharacterPosition().Y);
            mousePos = mState.Position;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            deltatimeNoble += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += deltaTime;
            timerExplosion += deltaTime;

            if (timer >= intervalo)
            {
                currentFrame++;
                if (currentFrame >= (6))
                    currentFrame = 0;
                timer = 0;
            }

           


            if (Input.IsPressed(Keys.Space) && !Characters.weaponUsed) Characters.weaponUsed = true;
            if (Input.IsPressed(Keys.Space) && WeaponTypes == WeaponType.NoblePhantom) NoblePhantomAux = true;
            if (Input.IsPressed(Keys.Space) && WeaponTypes == WeaponType.Bombardement) BombardementAux = true;

            if (deltatimeNoble >= 0.6f && WeaponTypes == WeaponType.NoblePhantom && NoblePhantomAux == true)
            {
                Vector2 aux1 = new Vector2(PositionRelativeToCharacter.X, PositionRelativeToCharacter.Y - 10);
                Vector2 aux2 = new Vector2(PositionRelativeToCharacter.X, PositionRelativeToCharacter.Y - 0);
                Vector2 aux3 = new Vector2(PositionRelativeToCharacter.X, PositionRelativeToCharacter.Y + 10);
                SoundManager.playSound("FX003");
                if (magzzz.getmagAt(3).getMag() > 0)
                {
                    Collisions.bulletsOnScreen.Add(new Bullet(aux1 + auxVector, (rotation + (getRandom())), Bullet.AmmoType.cal32, 2000, 50, explosion, 7));
                    Collisions.bulletsOnScreen.Add(new Bullet(aux2 + auxVector, (rotation + (getRandom())), Bullet.AmmoType.cal32, 2000, 50, explosion, 7));
                    Collisions.bulletsOnScreen.Add(new Bullet(aux3 + auxVector, (rotation + (getRandom())), Bullet.AmmoType.cal32, 2000, 50, explosion, 7));

                    magzzz.decMag(3);
                }
                if (magzzz.getmagAt(3).getMag() == 0) NoblePhantomAux = false;
                deltatimeNoble = 0;
            }

            if (deltatimeNoble >= 0.6f && WeaponTypes == WeaponType.Bombardement && BombardementAux == true)
            {
                SoundManager.playSound("FX025");
                if (flip == SpriteEffects.FlipHorizontally)
                {
                    Vector2 aux1 = new Vector2(PositionRelativeToCharacter.X + 100, -135);
                    Vector2 aux2 = new Vector2(PositionRelativeToCharacter.X + 130, -130);
                    Vector2 aux3 = new Vector2(PositionRelativeToCharacter.X + 155, -125);
                    Vector2 aux4 = new Vector2(PositionRelativeToCharacter.X + 175, -120);
                    Vector2 aux5 = new Vector2(PositionRelativeToCharacter.X + 200, -125);
                    Vector2 aux6 = new Vector2(PositionRelativeToCharacter.X + 225, -130);
                    if (magzzz.getmagAt(3).getMag() > 0)
                    {
                        Collisions.bulletsOnScreen.Add(new Bullet(aux1, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux2, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux3, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux4, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux5, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux6, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                    }
                }
                if (flip == SpriteEffects.None)
                {
                    Vector2 aux1 = new Vector2(PositionRelativeToCharacter.X - 100, -130);
                    Vector2 aux2 = new Vector2(PositionRelativeToCharacter.X - 130, -125);
                    Vector2 aux3 = new Vector2(PositionRelativeToCharacter.X - 155, -120);
                    Vector2 aux4 = new Vector2(PositionRelativeToCharacter.X - 175, -125);
                    Vector2 aux5 = new Vector2(PositionRelativeToCharacter.X - 200, -130);
                    Vector2 aux6 = new Vector2(PositionRelativeToCharacter.X - 225, -135);
                    if (magzzz.getmagAt(3).getMag() > 0)
                    {
                        Collisions.bulletsOnScreen.Add(new Bullet(aux1, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux2, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux3, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux4, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux5, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                        Collisions.bulletsOnScreen.Add(new Bullet(aux6, (float)Math.PI / 2, Bullet.AmmoType.cal32, 2000, 150, explosion, 5));
                    }
                }
                magzzz.decMag(3);
                if (magzzz.getmagAt(3).getMag() == 0) BombardementAux = false;
                deltatimeNoble = 0;
            }

            if (Input.IsDown(Keys.Space))
            {
                if (fireRateTime >= Bullet.getFireRate(Bullet.AmmoType.cal32) && WeaponTypes == WeaponType.MachineGun)
                {
                    if (magzzz.getmagAt(0).getMag() > 0)
                    {
                        SoundManager.playSound("FX71");
                        Collisions.bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector, (rotation + (getRandom())), Bullet.AmmoType.cal32, 300, 800, explosion, 7));
                    }
                    magzzz.decMag(0);
                    fireRateTime = 0;
                }
                if (fireRateTime >= Bullet.getFireRate(Bullet.AmmoType.rocket) && WeaponTypes == WeaponType.Rocket)
                {
                    if (magzzz.getmagAt(1).getMag() > 0)
                    {
                        SoundManager.playSound("FX110");
                        Collisions.bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector, (rotation + (getRandom())), Bullet.AmmoType.rocket, 2000, 500, explosion, 30));
                    }
                    magzzz.decMag(1);
                    fireRateTime = 0;
                }

                if (fireRateTime >= Bullet.getFireRate(Bullet.AmmoType.hadouken) && WeaponTypes == WeaponType.Hadouken)
                {
                    if (magzzz.getmagAt(2).getMag() > 0)
                    {
                        SoundManager.playSound("FX002");
                        Vector2 lelvector = new Vector2(10, 0);
                        if (flip == SpriteEffects.FlipHorizontally)
                            Collisions.bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector + lelvector, (rotation + (getRandom())), Bullet.AmmoType.hadouken, 2000, 50, explosion, 40));
                        else Collisions.bulletsOnScreen.Add(new Bullet(this.PositionRelativeToCharacter + auxVector - lelvector, (rotation + (getRandom())), Bullet.AmmoType.hadouken, 2000, 50, explosion, 40));
                    }
                    magzzz.decMag(2);
                    fireRateTime = 0;
                }
                if (fireRateTime >= Bullet.getFireRate(Bullet.AmmoType.rocket) && WeaponTypes == WeaponType.AirStrike)
                {
                    SoundManager.playSound("FX088");
                    if (flip == SpriteEffects.FlipHorizontally)
                    {
                        Vector2 aux1 = new Vector2(PositionRelativeToCharacter.X + 100, -150);
                        Vector2 aux2 = new Vector2(PositionRelativeToCharacter.X + 190, -150);
                        Vector2 aux3 = new Vector2(PositionRelativeToCharacter.X + 280, -150);
                        if (magzzz.getmagAt(1).getMag() > 0)
                        {
                            Collisions.bulletsOnScreen.Add(new Bullet(aux1, (float)Math.PI / 4, Bullet.AmmoType.rocket, 2000, 500, explosion, 30));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux2, (float)Math.PI / 4, Bullet.AmmoType.rocket, 2000, 500, explosion, 30));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux3, (float)Math.PI / 4, Bullet.AmmoType.rocket, 2000, 500, explosion, 30));
                        }
                        magzzz.decMag(1);
                        fireRateTime = 0;
                    }
                    if (flip == SpriteEffects.None)
                    {
                        Vector2 aux1 = new Vector2(PositionRelativeToCharacter.X - 100, -150);
                        Vector2 aux2 = new Vector2(PositionRelativeToCharacter.X - 190, -150);
                        Vector2 aux3 = new Vector2(PositionRelativeToCharacter.X - 280, -150);
                        if (magzzz.getmagAt(1).getMag() > 0)
                        {
                            Collisions.bulletsOnScreen.Add(new Bullet(aux1, 3 * (float)Math.PI / 4, Bullet.AmmoType.rocket, 2000, 500, explosion, 30));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux2, 3 * (float)Math.PI / 4, Bullet.AmmoType.rocket, 2000, 500, explosion, 30));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux3, 3 * (float)Math.PI / 4, Bullet.AmmoType.rocket, 2000, 500, explosion, 30));
                        }
                        magzzz.decMag(1);
                        fireRateTime = 0;
                    }
                }
                if (fireRateTime >= Bullet.getFireRate(Bullet.AmmoType.rocket) && WeaponTypes == WeaponType.HailOfArrows)
                {
                    SoundManager.playSound("FX033");
                    if (flip == SpriteEffects.FlipHorizontally)
                    {

                        Vector2 aux2 = new Vector2(PositionRelativeToCharacter.X + 130, -130);
                        Vector2 aux3 = new Vector2(PositionRelativeToCharacter.X + 155, -125);
                        Vector2 aux4 = new Vector2(PositionRelativeToCharacter.X + 175, -150);
                        Vector2 aux6 = new Vector2(PositionRelativeToCharacter.X + 225, -125);
                        Vector2 aux7 = new Vector2(PositionRelativeToCharacter.X + 245, -132);
                        Vector2 aux8 = new Vector2(PositionRelativeToCharacter.X + 270, -124);
                        Vector2 aux9 = new Vector2(PositionRelativeToCharacter.X + 300, -138);


                        if (magzzz.getmagAt(1).getMag() > 0)
                        {
                            
                            Collisions.bulletsOnScreen.Add(new Bullet(aux2, (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux3, (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux4, (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux6, (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux7, (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux8, (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux9, (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                        }
                        magzzz.decMag(1);
                        fireRateTime = 0;
                    }
                    if (flip == SpriteEffects.None)
                    {

                        Vector2 aux2 = new Vector2(PositionRelativeToCharacter.X - 130, -130);
                        Vector2 aux3 = new Vector2(PositionRelativeToCharacter.X - 155, -125);
                        Vector2 aux4 = new Vector2(PositionRelativeToCharacter.X - 175, -150);

                        Vector2 aux6 = new Vector2(PositionRelativeToCharacter.X - 225, -125);
                        Vector2 aux7 = new Vector2(PositionRelativeToCharacter.X - 245, -132);
                        Vector2 aux8 = new Vector2(PositionRelativeToCharacter.X - 270, -124);
                        Vector2 aux9 = new Vector2(PositionRelativeToCharacter.X - 300, -138);

                        if (magzzz.getmagAt(1).getMag() > 0)
                        {
                            
                            Collisions.bulletsOnScreen.Add(new Bullet(aux2, 3 * (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux3, 3 * (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux4, 3 * (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux6, 3 * (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux7, 3 * (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux8, 3 * (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));
                            Collisions.bulletsOnScreen.Add(new Bullet(aux9, 3 * (float)Math.PI / 4, Bullet.AmmoType.cal32, 2000, 100, explosion, 10));


                        }
                        magzzz.decMag(1);
                        fireRateTime = 0;
                    }
                }
                else fireRateTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && auxflip == SpriteEffects.FlipHorizontally && rot - 0.1f > -(float)Math.PI / 2)
            {
                rot -= (float)Math.PI / 100f;
                justflippedleft = false;
                justflippedright = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && auxflip == SpriteEffects.FlipHorizontally && rot + 0.1f < (float)Math.PI / 2)
            {
                rot += (float)Math.PI / 100f;
                justflippedleft = false;
                justflippedright = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && auxflip == SpriteEffects.None && rot + 0.1f < 3 * (float)Math.PI / 2)
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

            if (auxflip == SpriteEffects.None && (rot < (float)Math.PI / 2 || rot > 3 * (float)Math.PI / 2)) rot = (float)Math.PI;

            if (rot >= 2 * (float)Math.PI) rot = 0;

            if (WeaponTypes == WeaponType.Bombardement || WeaponTypes == WeaponType.HailOfArrows || WeaponTypes == WeaponType.AirStrike) mira = true;
            else mira = false;
            rotation = rot;
            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
            updateBullets(gameTime);
            updateDeleteBullets(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch, Characters ActiveChar, SpriteEffects flip, Texture2D textura)
        {
            auxVector = new Vector2(helperXCharPos, helperYCharPos);

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
            spriteBatch.Draw(textura, new Vector2(ActiveChar.CharacterPosition().X + helperXCharPos + 10, ActiveChar.CharacterPosition().Y + 45), null, Color.White, this.rotation + (float)Math.PI / 2, new Vector2((float)45 + helperX, (float)40), 1f, flip, 0f);

            auxflip = flip;
            foreach (Bullet bullet in Collisions.bulletsOnScreen)
            {
                if (bullet.ammoType == Bullet.AmmoType.cal32)
                {
                    if ((fireRateTime < Bullet.getFireRate(Bullet.AmmoType.cal32)) && WeaponTypes == WeaponType.MachineGun)
                        spriteBatch.Draw(flashFiring[currentFrame], new Vector2(this.PositionRelativeToCharacter.X + 5 + helperXpos, this.PositionRelativeToCharacter.Y + 45), null, Color.White, rotation, new Vector2((float)0, (float)260), .15f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(texturasRocket, new Vector2(bullet.sourcePosition.X, bullet.sourcePosition.Y + 10), null, Color.White, rotation, new Vector2((float)5, (float)2.5), 0.2f, SpriteEffects.None, 0f);
                }
                if (bullet.ammoType == Bullet.AmmoType.rocket)
                    spriteBatch.Draw(texturasRocket, new Vector2(bullet.sourcePosition.X, bullet.sourcePosition.Y + 7), null, Color.White, rotation, new Vector2((float)5, (float)3.5), 0.8f, SpriteEffects.None, 0f);

                if (bullet.ammoType == Bullet.AmmoType.hadouken)
                    spriteBatch.Draw(texturasRocket, new Vector2(bullet.sourcePosition.X, bullet.sourcePosition.Y + 10), null, Color.White, rotation, new Vector2((float)5, (float)2.5), 0.8f, SpriteEffects.None, 0f);

               
                bullet.draw(spriteBatch);
            }

            if (mira)
            {
                if (flip == SpriteEffects.FlipHorizontally)
                {
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X + 100), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X + 120), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X + 140), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X + 160), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X + 180), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X + 200), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X + 220), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X + 240), -100, 10, 5), Color.Red);
                }
                if (flip == SpriteEffects.None)
                {
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X - 100), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X - 120), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X - 140), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X - 160), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X - 180), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X - 200), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X - 220), -100, 10, 5), Color.Red);
                    spriteBatch.Draw(flatSquare, new Rectangle((int)(PositionRelativeToCharacter.X - 240), -100, 10, 5), Color.Red);
                }

            }
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
            for (int i = Collisions.bulletsOnScreen.Count - 1; i >= 0; i--)
            {
                Collisions.bulletsOnScreen[i].update(gameTime, this);
            }
        }

        public void updateDeleteBullets(GameTime gameTime)
        {
            for (int i = 0; i < Collisions.bulletsOnScreen.Count; i++)
            {
                if ((Collisions.bulletsOnScreen[i].sourcePosition - this.PositionRelativeToCharacter).Length() > Collisions.bulletsOnScreen[i].range)
                    Collisions.bulletsOnScreen.Remove(Collisions.bulletsOnScreen[i]);
            }
        }
    }
}
