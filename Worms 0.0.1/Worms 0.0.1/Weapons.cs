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
        private Point mousePos;
        //MUDAR ISTO RELATIVAMENTE AS TEXTURAS!
        /// <summary>
        ///basicamente ver o tamanho das texturas dos characters!!!
        /// </summary>
        protected int TextureWidth = 20, TextureWheight = 40;
        //==================================================

        public Weapons(string name, Characters Char)
        {
            this.Name = name; 
            this.PositionRelativeToCharacter = new Vector2(Char.CharacterPosition().X + (TextureWidth - 2), Char.CharacterPosition().Y - (this.TextureWheight/2));
            rotation = 0f;
        }

        public void Load(ContentManager content, string asset)
        {
            textura = content.Load<Texture2D>(asset);
        }

        public void Update(GameTime gameTime, Characters Char)
        {

            MouseState mState = Mouse.GetState();

            Vector2 nextPosition = PositionRelativeToCharacter;
            nextPosition = new Vector2(PositionRelativeToCharacter.X, PositionRelativeToCharacter.Y);

            //rever
            //x y rato
            mousePos = mState.Position;

            double x = mousePos.X - PositionRelativeToCharacter.X;
            double y = PositionRelativeToCharacter.Y - mousePos.Y;

            double hypo = Math.Pow(x,2) + Math.Pow(y,2);
            double result = Math.Sqrt(hypo);

            rotation = (float)(Math.Atan(result) * 180 / Math.PI);
            //rever
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.textura, this.PositionRelativeToCharacter, null, Color.White, this.rotation, new Vector2(PositionRelativeToCharacter.X / 2, PositionRelativeToCharacter.Y), 1f, SpriteEffects.None, 0f);
        }
    }
}
