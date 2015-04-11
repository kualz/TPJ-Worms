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

            MouseState mState = Mouse.GetState();

            Vector2 nextPosition = PositionRelativeToCharacter;
            nextPosition = new Vector2(PositionRelativeToCharacter.X -  mousePos.X, PositionRelativeToCharacter.Y - mousePos.Y);

            //rever
            //x y rato
            mousePos = mState.Position;

            double x = Math.Abs(mousePos.X - PositionRelativeToCharacter.X);
            double y = Math.Abs(PositionRelativeToCharacter.Y - mousePos.Y);

            //double hypo = Math.Pow(mousePos.X, 2) + Math.Pow(y, 2);
            //double result = Math.Sqrt(hypo);

            rotation = (float)(Math.Atan(y/x) * 180 / Math.PI);
            //rever
            rec = new Rectangle((int)PositionRelativeToCharacter.X, (int)PositionRelativeToCharacter.Y, 10, 15);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.textura, this.PositionRelativeToCharacter, rec , Color.White, this.rotation, new Vector2(PositionRelativeToCharacter.X / 2, PositionRelativeToCharacter.Y), 1f, SpriteEffects.None, 0f);
        }
    }
}
