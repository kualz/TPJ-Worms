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

        protected bool SpecialWeapon, CharacterInPlay, hasjumped;
        protected string CharacterName;
        protected Vector2 CharacterPos;
        protected float speed;
        public CharacterState WormState;
        public Vector2 velocity;
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
            hasjumped = false;

        }

        public void Load(ContentManager content)
        {
            textura = content.Load<Texture2D>("WeaponRifle");
        }

        public void Update(GameTime gameTime)
        {      
            CharacterPos += velocity;

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
                velocity.Y += 0.15f * i;
            }
            if (hasjumped == false)        
                velocity.Y = 0f;

            //APENAS PARA TESTE
            if (CharacterPos.Y > 350)
                hasjumped = false;
            

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
