using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Worms_0._0._1
{
    class Weapons
    {
        public enum WeaponType
        {
            Rocket,
            MachineGun,
            GrenadeLauncher,
            ShotGun
        }
        protected string Name;
        public WeaponType WeaponTypes;
        protected int Magazine;
        protected float range;
        protected Vector2 PositionRelativeToCharacter;
        protected MouseState mouse;
        protected float rotation;
        protected Texture2D textura;
        private Rectangle rec;
        //MUDAR ISTO RELATIVAMENTE AS TEXTURAS!
        /// <summary>
        ///basicamente ver o tamanho das texturas dos characters!!!
        /// </summary>
        protected int TextureWidth = 10, TextureWheight = 15;
        //==================================================

        public Weapons(string name, Characters Char)
        {
            this.Name = name; 
            this.PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X, Char.CharacterPosition().Y);
            rotation = 0f;
        }

        public void Load(ContentManager content, string asset)
        {
            textura = content.Load<Texture2D>(asset);
        }

        public void Update(GameTime gameTime, Characters Char)
        {

            Vector2 nextPosition = PositionRelativeToCharacter;
            nextPosition = new Vector2(PositionRelativeToCharacter.X, PositionRelativeToCharacter.Y);

            //rever
            MouseState mState = Mouse.GetState();
            //x y rato
            Point mousePos = mState.Position;

            float y = (float)Math.Abs(mousePos.Y - PositionRelativeToCharacter.Y);
            float x = (float)Math.Abs(mousePos.X - PositionRelativeToCharacter.X);
            float rot = (float)Math.Atan2(x, y);
            rot += (float)Math.PI/1f;
            rotation = rot;

            //rever
            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.textura, this.PositionRelativeToCharacter, rec , Color.White, this.rotation, new Vector2(PositionRelativeToCharacter.X / 2, PositionRelativeToCharacter.Y), 1f, SpriteEffects.None, 0f);
        }
    }
}
