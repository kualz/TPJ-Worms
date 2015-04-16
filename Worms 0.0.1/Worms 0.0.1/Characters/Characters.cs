using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class Characters : IFocusable
    {
        private Texture2D textura;
        protected bool SpecialWeapon, CharacterInPlay, hasjumped;
        protected string CharacterName;
        protected Vector2 CharacterPos;
        protected float speed;
        public CharacterState WormState;
        public Vector2 velocity;
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
            /// <summary>
            /// esta parte aqui nao sei mesmo sera a melhor forma assim?
            /// :o
            /// </summary>
            Arsenal[0].Load(content, "WeaponRifle");
            Arsenal[1].Load(content, "WeaponRifle");
            Arsenal[2].Load(content, "WeaponRifle");    
            /// <summary>
            /// aqui so te diz que a arma inicial 'e a AR556 mas se quiseres podes mudar
            /// </summary>
            getAndActivateWeapon(0);
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += deltaTime;
            MouseState mState = Mouse.GetState();

            if (timer >= intervalo)
            {
                currentFrame = currentFrame + 50;
                if (currentFrame >= 250)
                {
                    currentFrame = 0;
                }
                timer = 0;
            }
            CharacterPos += velocity;


            /// <summary>
            /// aqui o input para trocar de arma a funcionar...
            /// o problema e quando tens dois jogadores...as informacoes do segundo sobrepoem as do 1' 
            /// mas isso e simples de se ver
            /// </summary>
            if (Input.IsPressed(Keys.D1))
            {
                weaponCodeChosen = 0;
                getAndActivateWeapon(weaponCodeChosen);
                previousWeapon = 0;
            }
            else if (Input.IsPressed(Keys.D2))
            {
                weaponCodeChosen = 1;
                getAndActivateWeapon(weaponCodeChosen);
                previousWeapon = 1;
            }
            else if (Input.IsPressed(Keys.D3))
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



            if (Keyboard.GetState().IsKeyDown(Keys.A)) velocity.X = -3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) velocity.X = 3f;
            else velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && hasjumped == false)
            {
                CharacterPos.Y -= 10f;
                velocity.Y = -5f;
                hasjumped = true;
            }
            if (hasjumped == true)
            {
                float i = 1;
                velocity.Y += 0.20f * i;
            }
            if (hasjumped == false)        
                velocity.Y = 0f;

            //APENAS PARA TESTE
            if (CharacterPos.Y > 350)
                hasjumped = false;

            mousePos = mState.Position;
            float x = (float)CharacterPos.X - mousePos.X;

            if (mousePos.X > CharacterPos.X) flip = SpriteEffects.FlipHorizontally  ;
            else flip = SpriteEffects.None;
            rec = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, 50, 72);
        }
        public void Draw(SpriteBatch spritebatch)
        {   
            spritebatch.Draw(textura, new Vector2((int)CharacterPos.X, (int)CharacterPos.Y), new Rectangle(currentFrame, 0, 50, 72), Color.White, 0f, new Vector2(25, 36), 1f, flip, 0f);
            spritebatch.DrawString(font, "" + CharacterName, new Vector2((int)CharacterPos.X - 48, (int)CharacterPos.Y - 70), Color.White);
            /// <summary>
            /// tipo aqui so tens a weapon selecionada a fazer draw...nao sei se queres optimizar isto!!!
            /// </summary>
            if(this.isActive())
                Arsenal[weaponCodeChosen].Draw(spritebatch, CharactersHandler.getActiveCharacter());
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



        public Vector2 Position
        {
            get { throw new NotImplementedException(); }
        }

        Vector2 IFocusable.Position
        {
            get { throw new NotImplementedException(); }
        }
    }
}
