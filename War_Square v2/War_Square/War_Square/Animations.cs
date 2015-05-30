using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square
{
    class Animations
    {
        private Texture2D animaçao;
        private string assets;
        private Vector2 position;
        private float scale = 5f;
        private int currentFrame;
        private float timer; /*positionTimer*/
        private float intervalo = 0.05f;
        private int Frames;
        private int AnimationSpeed;
        public enum AnimationType
        {
            scaled,
            slide
        }
        private AnimationType animationType;
        //se formos usar um retangulo para passar os frames da imagem------------------------------
        //slide square para as frames                                                             |
        private int squareSlideX; //para fazer slide para o lado este numero de pixeis            |
        //private int slideY; //nem sei se vamos usar este XD                                     | 
        private int height; //tamanho dos retangulos (altura) em relaçao a figura que vamos usar! |
        private int width; //tamanho dos retangulos (largura) em relaçao a figura que vamos usar! |
        //-----------------------------------------------------------------------------------------

        /// <summary>
        /// suqareSlide: How many pixels that frames Rectangle will slide in the animationFrame image
        /// asset: image name
        /// frameNumber: number of images that the square has to slide
        /// instantialPosition: animation start posistion
        /// imageHeight: aplies to every frame square to slide
        /// imageWidth: aplies to every frame square to slide 
        /// animationType: animation type choose between slide or scale in or out
        /// animationSpeed: ´the speed that animation will run
        /// </summary>
        /// <param name="squareSlide"></param>
        /// <param name="asset"></param>
        /// <param name="frameNumber"></param>
        /// <param name="instantialPosition"></param>
        /// <param name="imageHeight"></param>
        /// <param name="imageWidth"></param>
        /// <param name="animationType"></param>
        /// <param name="AnimationSpeed"></param>
        public Animations(int squareSlide,string asset, int frameNumber, Vector2 instantialPosition, int imageHeight, int imageWidth, AnimationType animationType, int AnimationSpeed)
        {
            this.squareSlideX = squareSlide;
            this.assets = asset;
            this.Frames = frameNumber;
            this.position = instantialPosition;
            this.height = imageHeight;
            this.width = imageWidth;
            this.animationType = animationType;
            this.AnimationSpeed = AnimationSpeed;
        }

        public void load(ContentManager content)
        {
            animaçao = content.Load<Texture2D>(assets);
        }

        public void update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //se tiver frames-----------------------------------------------------
            timer += deltaTime;
            if (timer >= intervalo)
            {
                currentFrame += squareSlideX;//para usar com o retangulo o  slide se nao usa-se ++!
                //currentFrame++;
                if (currentFrame >= (Frames))
                    currentFrame = 0;
                timer = 0;
            }
            //-------------------------------------------------------------------
            //quando a animaçao tem uma mudança de scale!
            if (this.animationType == AnimationType.scaled)
            {
                this.scale -= (float)gameTime.ElapsedGameTime.TotalSeconds / 2f;
                if (this.scale <= 1){
                    this.scale = 1;
                }
            }
            //quando a animaçao tem uma mudança de posiçao!
            if (this.animationType == AnimationType.slide)
                this.position.X += position.X + AnimationSpeed * (deltaTime * 0.8f);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if(animationType == AnimationType.scaled)
            {
                //os draws tao sempre a dar erros ja tou aqui a deitar peidos pela cabeça!
                //isto do draw e tao gay acrescentas mais cenas e da erro ok! depois vemos isto!
                //spriteBatch.Draw(assets, position, new Rectangle(currentFrame, 0, new Vector2(this.width, this.heightheight), Color.White, 0f, new Vector2(0f), scale, SpriteEffects.None, 0f);
            }
            if(animationType == AnimationType.slide)
            {
                //os draws tao sempre a dar erros ja tou aqui a deitar peidos pela cabeça!
                //spriteBatch.Draw(assets, position, new Rectangle(currentFrame, 0, this.width, this.height), Color.White);
            }
        }
    }
}
