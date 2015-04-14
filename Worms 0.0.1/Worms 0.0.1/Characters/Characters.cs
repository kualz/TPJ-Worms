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
        private float intervalo = 0.08f, timer;
        public SpriteEffects flip;
        private int currentFrame = 0;
        private Point mousePos;
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
            textura = content.Load<Texture2D>("character");
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
            

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(textura, new Vector2((int)CharacterPos.X, (int)CharacterPos.Y), new Rectangle(currentFrame, 0, 50, 72), Color.White, 0f, new Vector2(25, 36), 1f, flip, 0f);
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
