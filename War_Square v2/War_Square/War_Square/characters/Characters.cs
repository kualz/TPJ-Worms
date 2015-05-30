using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using War_Square.WeaponsAndProjectiles;
using War_Square;

namespace War_Square.characters
{
    class Characters : IFocusable
    {
        private Texture2D textura, Hitbox;
        protected bool SpecialWeapon, CharacterInPlay, hasjumped, isAlive = true, HasSpecial = true;
        protected string CharacterName;
        public Vector2 CharacterPos, pos, velocity, nextpos, DeadPos;
        private float intervalo = 0.08f, timer;
        public CharacterState WormState;
        public SpriteEffects flip = SpriteEffects.FlipHorizontally;
        private int currentFrame = 0, Hp, PLAYER_MAXIMUM_HP = 100;
        private SpriteFont font;
        public List<Weapons> Arsenal = new List<Weapons>();
        public Weapons ActiveWeapon;
        private Rectangle rec;
        private int AuxDead = 0;
        public static bool weaponUsed = false;
        public enum CharacterState
        {
            GoingRight,
            GoingLeft,
            Airborne,
            OnTheGround
        };
        private int weaponCodeChosen = 0, previousWeapon = 0;

        public Characters() { }

        public Characters(string name)
        {
            CharacterName = name;
            SpecialWeapon = false;
            CharacterInPlay = false;
            WormState = CharacterState.OnTheGround;
            hasjumped = false;
            CreatArsenal();
            ActiveWeapon = Arsenal[0];
            Hp = PLAYER_MAXIMUM_HP;
        }

        public void Load(ContentManager content)
        {
            textura = content.Load<Texture2D>("character");
            font = content.Load<SpriteFont>("MyFont");
            Hitbox = content.Load<Texture2D>("DeadCross");          
            Arsenal[0].Load(content, "gunn");
            Arsenal[1].Load(content, "gunn");
            Arsenal[2].Load(content, "gunn");          
            getAndActivateWeapon(0);
        }

        public void Update(GameTime gameTime)
        {
            if (isAlive)
            {
                Vector2 Gravityaux = new Vector2(CharacterPos.X, CharacterPos.Y + 2f);
                float deltaTime1 = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (isActive()) Arsenal[weaponCodeChosen].Update(gameTime, this, flip);
                if (isActive() && hud.roundTime >= 0)
                {
                    float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    timer += deltaTime;
                    MouseState mState = Mouse.GetState();
                    Vector2 nextPos = CharacterPos;

                    if (timer >= intervalo)
                    {
                        currentFrame = currentFrame + 58;
                        if (currentFrame >= 290)
                        {
                            currentFrame = 0;
                        }
                        timer = 0;
                    }

                    /// <summary>
                    /// aqui o input para trocar de arma a funcionar...
                    /// </summary>
                    if (Keyboard.GetState().IsKeyDown(Keys.D1) && weaponUsed == false)
                    {
                        weaponCodeChosen = 0;
                        getAndActivateWeapon(weaponCodeChosen);
                        previousWeapon = 0;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D2) && weaponUsed == false)
                    {
                        weaponCodeChosen = 1;
                        getAndActivateWeapon(weaponCodeChosen);
                        previousWeapon = 1;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D3) && HasSpecial && weaponUsed == false)
                    {
                        weaponCodeChosen = 2;
                        getAndActivateWeapon(weaponCodeChosen);
                        previousWeapon = 2;
                    }
                    /// <summary>
                    /// aqui tbem e simples basicamente ele pega na arma que esta ativa e faz o update dela com as suas informacoes
                    /// se fores ao codigo da weapon aquilo ja nem usa a weapons handler
                    /// simplesmente carrega direto as variaveis
                    /// </summary>
                    if (weaponCodeChosen == 2 && Input.IsPressed(Keys.Space))
                    {
                        HasSpecial = false;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.W) && hasjumped == false)
                    {
                        CharacterPos.Y -= 5f;
                        velocity.Y = -3f;
                        hasjumped = true;

                    }
                    if ((hasjumped == true) && (CheckCollisionsTile(Gravityaux).Count == 0))
                    {
                        float i = 1;
                        velocity.Y += 0.2f * i;
                        if (velocity.Y > 1f) velocity.Y = 1f;
                    }

                    nextpos = new Vector2(CharacterPos.X, CharacterPos.Y - 5f);

                    if (CheckCollisionsTile(nextpos).Count != 0) velocity.Y = 1f;

                    if (CheckCollisionsTile(Gravityaux).Count == 0)
                    {
                        hasjumped = true;
                    }
                    if (CheckCollisionsTile(Gravityaux).Count != 0)
                    {
                        hasjumped = false;
                    }
                    if (hasjumped == false)
                        velocity.Y = 0f;

                    CharacterPos += velocity;

                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        nextpos = new Vector2(CharacterPos.X - 2f, CharacterPos.Y);
                        flip = SpriteEffects.None;
                        if (CheckCollisionsTile(nextpos).Count == 0)
                            CharacterPos = nextpos;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        nextpos = new Vector2(CharacterPos.X + 2f, CharacterPos.Y);
                        flip = SpriteEffects.FlipHorizontally;
                        if (CheckCollisionsTile(nextpos).Count == 0)
                            CharacterPos = nextpos;
                    }

                    if (CheckCollisionsTile(Gravityaux).Count == 0)
                    {
                        hasjumped = true;
                    }
                    if (CheckCollisionsTile(Gravityaux).Count != 0)
                    {
                        hasjumped = false;
                    }
                    else velocity.X = 0f;
                    CharacterPos += velocity;
                    rec = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, 40, 70);
                }
                else
                {
                    weaponCodeChosen = 0;
                    getAndActivateWeapon(weaponCodeChosen);
                    previousWeapon = 0;

                    if (CheckCollisionsTile(Gravityaux).Count != 0) hasjumped = false;
                    else hasjumped = true;

                    if (hasjumped == true)
                    {
                        float i = 1;
                        velocity.Y += 0.2f * i;
                        if (velocity.Y > 2f) velocity.Y = 2f;
                        if (CheckCollisionsTile(Gravityaux).Count != 0)
                            hasjumped = false;
                        CharacterPos += velocity;
                    }
                    rec = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, 40, 70);
                }
                DeadPos = CharacterPos;
            }
            if (!IsInsideMap(CharacterPos))
            {
                Hp = -1;
            }
            if (this.Hp <= 0){
                this.isAlive = false;
                Collisions.characterCollisions.Remove(this);
            }
            if (this.isAlive == false)
            {
                if (hasjumped == false && AuxDead == 0)
                {
                    DeadPos.Y -= 10f;
                    velocity.Y = -3f;
                    hasjumped = true;
                    AuxDead = 1;
                }
                if (hasjumped == true)
                {
                    velocity.Y += 0.2f;
                    DeadPos += velocity;
                }               
                rec = new Rectangle((int)DeadPos.X, (int)DeadPos.Y, 40, 70);
                Vector2 Gravityaux = new Vector2(DeadPos.X, DeadPos.Y-35f);
                if (CheckCollisionsTile(Gravityaux).Count != 0) hasjumped = false;
            }

        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (this.isAlive)
            {
                Vector2 Gravityaux = new Vector2(CharacterPos.X, CharacterPos.Y + 4f);
                spritebatch.Draw(textura, new Vector2((int)CharacterPos.X, (int)CharacterPos.Y), new Rectangle(currentFrame, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, flip, 0f);
                if (this.isActive())
                    spritebatch.DrawString(font, "0/" + magzzz.getmagAt(GetActiveWeaponCODE()).getMag(), new Vector2(this.CharacterPos.X - 45, this.CharacterPos.Y), Color.Red);
                spritebatch.DrawString(font, "" + CharacterName, new Vector2((int)CharacterPos.X, (int)CharacterPos.Y - 40), Color.White);
                //spritebatch.Draw(Hitbox, new Rectangle((int)Math.Round(Gravityaux.X) + 12, (int)Math.Round(Gravityaux.Y), 25, 63), Color.Wheat);
                /// <summary>
                /// tipo aqui so tens a weapon selecionada a fazer draw...nao sei se queres optimizar isto!!!
                /// </summary>
                if (this.isActive())
                    Arsenal[weaponCodeChosen].Draw(spritebatch, this, flip);
            }
            else spritebatch.Draw(Hitbox, DeadPos,null, Color.White,0, Vector2.Zero,0.05f,SpriteEffects.None,0);
        }

        public Vector2 CharacterPosition()
        {
            return CharacterPos;
        }
        public void SetCharacterPosition(Vector2 pos)
        {
            CharacterPos = pos;
        }
        public void SetCharacterInPlay()
        {
            if (CharacterInPlay == true) CharacterInPlay = false;
            else CharacterInPlay = true;
        }
        public bool isActive()
        {
            return CharacterInPlay;
        }
        public string returnName()
        {
            return this.CharacterName;
        }
        public bool isJumping()
        {
            return hasjumped;
        }
        public Rectangle getCharRec()
        {
            return rec;
        }
        public int getMaximumHp()
        {
            return PLAYER_MAXIMUM_HP;
        }
        public int getHp()
        { return Hp; }
        /// <summary>
        /// usar value negativo para retirar e positivo para somar 
        /// </summary>
        /// <param name="value"></param>
        public void changeHp(int value)
        { Hp += value; }


        /// <summary>
        /// ok aqui basicamente eu criei dois metodos que podiam tar numa class a parte!
        /// aquele teu que ja tinhas so completei e depois para aticar a weapon...para sabermos qual esta ativa
        /// </summary>
        public void CreatArsenal()
        {
            Arsenal.Add(new Weapons("AR556", this, Weapons.WeaponType.MachineGun, true, magzzz.getmagAt(0).getMag()));
            Arsenal.Add(new Weapons("Bazooka", this, Weapons.WeaponType.Rocket, false, magzzz.getmagAt(1).getMag()));
            if (this.CharacterName == "Kualz") Arsenal.Add(new Weapons("AirStrike", this, Weapons.WeaponType.AirStrike, false, magzzz.getmagAt(2).getMag()));
            else if (this.CharacterName == "Phaktumn") Arsenal.Add(new Weapons("Hadouken", this, Weapons.WeaponType.Hadouken, false, magzzz.getmagAt(2).getMag()));
            else if (this.CharacterName == "Klipper") Arsenal.Add(new Weapons("HailOfArrows", this, Weapons.WeaponType.HailOfArrows, false, magzzz.getmagAt(2).getMag()));
            else if (this.CharacterName == "Zjeh") Arsenal.Add(new Weapons("Bombardement", this, Weapons.WeaponType.Bombardement, false, magzzz.getmagAt(3).getMag()));
            else if (this.CharacterName == "Saber") Arsenal.Add(new Weapons("NoblePhantom", this, Weapons.WeaponType.NoblePhantom, false, magzzz.getmagAt(3).getMag()));
            else Arsenal.Add(new Weapons("vazio", this, Weapons.WeaponType.Rocket, false, magzzz.getmagAt(1).getMag()));
        }

        public void getAndActivateWeapon(int weapon)
        {
            Arsenal[previousWeapon].setWeaponState();
            Arsenal[weaponCodeChosen].setWeaponState();
            ActiveWeapon = Arsenal[weaponCodeChosen];
        }

        public Weapons GetActiveWeapon(){
            return ActiveWeapon;
        }


        /// <summary>
        /// Este metodo tou a usar no draw do character para saber a arama que esta atica num codigo de 0 a 2
        /// </summary>
        /// <returns></returns>
        public int GetActiveWeaponCODE()
        {
            if (this.ActiveWeapon.getName() == "AR556") return 0;
            if (this.ActiveWeapon.getName() == "Bazooka") return 1;
            if (this.ActiveWeapon.getName() == "HailOfArrows" || this.ActiveWeapon.getName() == "AirStrike" || this.ActiveWeapon.getName() == "Hadouken") return 2;
            if (this.ActiveWeapon.getName() == "NoblePhantom" || this.ActiveWeapon.getName() == "Bombardement") return 3;
            else return -1;
        }

        public void UnlockSpecialWeapon(){
            SpecialWeapon = true;
        }

        public List<Rectangle> CheckCollisionsTile(Vector2 pos)
        {
            List<Rectangle> collidingWith = new List<Rectangle>();
            Rectangle rect = new Rectangle((int)Math.Round(pos.X) + 12, (int)Math.Round(pos.Y), 25, 65);

            foreach (var rectangle in Collisions.tilesCollisions)
            {
                if (rect.Intersects(rectangle) && rect != rectangle)
                {
                    collidingWith.Add(rectangle);
                }
            }
            return collidingWith;
        }

        public Rectangle CheckCollisionsCharacters(Rectangle rect)
        {
            foreach (Characters Character in Collisions.characterCollisions)
            {
                if (rect.Intersects(Character.getCharRec()) && rect != Character.getCharRec() && !Character.isActive())
                {
                    pos = new Vector2(this.rec.X, this.rec.Y);
                    return Character.getCharRec();
                }
            }
            return new Rectangle(0, 0, 0, 0);
        }

        public Vector2 Position
        {
            get { return this.CharacterPos; }
        }

        public bool IsInsideMap(Vector2 position)
        {
            int MapSizeMaxY = 900;

            if (position.Y > MapSizeMaxY - 100) return false;
            else return true;
        }
    }
}
