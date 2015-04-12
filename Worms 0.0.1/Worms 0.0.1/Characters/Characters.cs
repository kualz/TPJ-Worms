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
    class Characters
    {
        private Texture2D textura;

        protected bool SpecialWeapon, CharacterInPlay;
        protected string CharacterName;
        protected Vector2 CharacterPos;
        protected float speed;
        public CharacterState WormState;
        public enum CharacterState
        {
            GoingRight,
            GoingLeft,
            Airborne,
            OnTheGround
        };

        public Characters()
        { }

        public Characters(string name)
        {
            CharacterName = name;
            SpecialWeapon = false;
            CharacterInPlay = false;
            speed = 100f;
            WormState = CharacterState.OnTheGround;
        }

        public void Load(ContentManager content)
        {
            textura = content.Load<Texture2D>("WeaponRifle");
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 nextPosition = CharacterPos;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                WormState = CharacterState.GoingLeft;
                nextPosition = new Vector2(CharacterPos.X - speed * deltaTime, CharacterPos.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                WormState = CharacterState.GoingRight;
                nextPosition = new Vector2(CharacterPos.X + speed * deltaTime, CharacterPos.Y);
            }         
            if (Keyboard.GetState().IsKeyDown(Keys.W)) WormState = CharacterState.Airborne;
            if (WormState == CharacterState.Airborne)
            {
                nextPosition = new Vector2(CharacterPos.X - speed * deltaTime, -0.5f * (CharacterPos.X - speed * deltaTime) * (CharacterPos.X - speed * deltaTime) + (CharacterPos.X - speed * deltaTime));
            }
            //colocar colisoes com o chao e alterar o estado para ontheground.
            CharacterPos = nextPosition;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(textura , new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, 15, 30), Color.White);
        }

        public Vector2 CharacterPosition()
        {
            return CharacterPos;
        }
        public void SetCharacterPosition(Vector2 pos)
        {
            CharacterPos = pos;
        }
        public void UnlockSpecialWeapon()
        {
            SpecialWeapon = true;
        }
    }
}
