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
        protected bool SpecialWeapon, CharacterInPlay, hasjumped;
        protected string CharacterName;
        protected Vector2 CharacterPos;
        protected float speed;
        public CharacterState WormState;
        public Vector2 velocity, nextpos;
        private float intervalo = 0.08f, timer, timer1;
        public SpriteEffects flip;
        private int currentFrame = 0;
        private Point mousePos;
        private SpriteFont font;
        public List<Weapons> Arsenal = new List<Weapons>();
        public Weapons ActiveWeapon;
        private Rectangle rec;
        public enum CharacterState
        {
            GoingRight,
            GoingLeft,
            Airborne,
            OnTheGround
        };
        private int weaponCodeChosen = 0, previousWeapon = 0;

        public Characters()
        { }

        public Characters(string name)
        {
            CharacterName = name;
            SpecialWeapon = false;
            CharacterInPlay = false;
            speed = 100f;
            WormState = CharacterState.OnTheGround;
            hasjumped = false;
            CreatArsenal();
            ActiveWeapon = Arsenal[0];
        }

        public void Load(ContentManager content)
        {
            textura = content.Load<Texture2D>("character");
            font = content.Load<SpriteFont>("MyFont");
            Hitbox = content.Load<Texture2D>("1");
            /// <summary>
            /// esta parte aqui nao sei mesmo sera a melhor forma assim?
            /// :o
            /// </summary>
            Arsenal[0].Load(content, "gunn");
            Arsenal[1].Load(content, "gunn");
            Arsenal[2].Load(content, "gunn");
            /// <summary>
            /// aqui so te diz que a arma inicial 'e a AR556 mas se quiseres podes mudar
            /// </summary>
            getAndActivateWeapon(0);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 Gravityaux = new Vector2(CharacterPos.X, CharacterPos.Y + 2f);
            if (isActive())
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
                /// o problema e quando tens dois jogadores...as informacoes do segundo sobrepoem as do 1' 
                /// mas isso e simples de se ver
                /// </summary>
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                {
                    weaponCodeChosen = 0;
                    getAndActivateWeapon(weaponCodeChosen);
                    previousWeapon = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    weaponCodeChosen = 1;
                    getAndActivateWeapon(weaponCodeChosen);
                    previousWeapon = 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D3))
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
                Arsenal[weaponCodeChosen].Update(gameTime, this);

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
               

                //rodar personagem
                mousePos = mState.Position;
                float x = (float)CharacterPos.X - mousePos.X;
                if (mousePos.X > CharacterPos.X) flip = SpriteEffects.FlipHorizontally;
                else flip = SpriteEffects.None;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    nextpos = new Vector2(CharacterPos.X - 2f, CharacterPos.Y);
                    if (CheckCollisionsTile(nextpos).Count == 0)
                        CharacterPos = nextpos;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    nextpos = new Vector2(CharacterPos.X + 2f, CharacterPos.Y);
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
                if (CheckCollisionsTile(Gravityaux).Count != 0)
                {
                    hasjumped = false;
                }
                else
                {
                    hasjumped = true;
                }
                if (hasjumped == true)
                {
                    float i = 1;
                    velocity.Y += 0.2f * i;
                    if (velocity.Y > 2f) velocity.Y = 2f;
                    if (CheckCollisionsTile(Gravityaux).Count != 0)
                    {
                        hasjumped = false;
                    }
                    CharacterPos += velocity;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            
            Vector2 Gravityaux = new Vector2(CharacterPos.X, CharacterPos.Y + 4f);
            spritebatch.Draw(textura, new Vector2((int)CharacterPos.X, (int)CharacterPos.Y), new Rectangle(currentFrame, 0, 50, 70), Color.White, 0f, Vector2.Zero, 1f, flip, 0f);
            if(this.isActive())
                spritebatch.DrawString(font, "Character Active: " + this.CharacterName, new Vector2(500f, 475f), Color.White); 
            spritebatch.DrawString(font, "" + CharacterName, new Vector2((int)CharacterPos.X, (int)CharacterPos.Y - 40), Color.White);
            //spritebatch.Draw(Hitbox, new Rectangle((int)Math.Round(Gravityaux.X) + 12, (int)Math.Round(Gravityaux.Y), 25, 63), Color.Wheat);
            /// <summary>
            /// tipo aqui so tens a weapon selecionada a fazer draw...nao sei se queres optimizar isto!!!
            /// </summary>
            // if(this.isActive())
            Arsenal[weaponCodeChosen].Draw(spritebatch, this);
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


        /// <summary>
        /// ok aqui basicamente eu criei dois metodos que podiam tar numa class a parte!
        /// aquele teu que ja tinhas so completei e depois para aticar a weapon...para sabermos qual esta ativa
        /// </summary>
        public void CreatArsenal()
        {
            Arsenal.Add(new Weapons("AR556", this, Weapons.WeaponType.MachineGun, true));
            Arsenal.Add(new Weapons("Bazooka", this, Weapons.WeaponType.Rocket, false));
            Arsenal.Add(new Weapons("nade Launcher", this, Weapons.WeaponType.GrenadeLauncher, false));
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

        public Vector2 Position
        {
            get { return this.CharacterPos; }
        }
    }
}
