using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.Sounds
{
    class Music
    {
        private Song music;
        private string asset;
        public string musicName{ get; set;}


        public Music(string asset, string musicName)
        {
            this.asset = asset;
            this.musicName = musicName;
        }

        public void load(ContentManager content){
            music = content.Load<Song>(asset);
        }

        public void play(){
            MediaPlayer.Play(music);
        }
    }
}
