using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class Characters
    {
        private bool SpecialWeapon, CharacterInPlay;
        private Vector2 CharacterPos;
        private float speed;
        public CharacterState WormState;
        public enum CharacterState
        {
            GoingRight,
            GoingLeft,
            Airborne,
            OnTheGround
        };

        public Characters(Vector2 pos)
        {
            SpecialWeapon = false;
            CharacterInPlay = false;
            CharacterPos = pos;
            speed = 100f;
            WormState = CharacterState.OnTheGround;
        }

        public void Load()
        {

        }
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 nextPosition = CharacterPos;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                nextPosition = new Vector2(CharacterPos.X - speed * deltaTime, CharacterPos.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                nextPosition = new Vector2(CharacterPos.X + speed * deltaTime, CharacterPos.Y);
            }         
            if (Keyboard.GetState().IsKeyDown(Keys.W)) WormState = CharacterState.Airborne;
            if (WormState == CharacterState.Airborne)
            {
                nextPosition = new Vector2(CharacterPos.X - speed * deltaTime, CharacterPos.Y);
            }

        }
        public void Draw(GameTime gameTime)
        {

        }
        public void UnlockSpecialWeapon()
        {
            SpecialWeapon = true;
        }

    }
}
