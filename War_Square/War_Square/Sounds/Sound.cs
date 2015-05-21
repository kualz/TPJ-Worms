using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.Sounds
{
    class Sound
    {
        private SoundEffect sound;
        private string asset;
        public string soundName { get; set; }
        private float volume;

        /// <summary>
        /// Sound name = dalhe o nome que quiseres serve como key num dictionary
        /// Sound asset = escreve o nome do ficheiro
        /// </summary>
        /// <param name="soundAsset"></param>
        /// <param name="soundName"></param>
        /// <param name="volume"></param>
        public Sound(string soundAsset, string soundName, float volume)
        {
            this.soundName = soundName;
            asset = soundAsset;
            this.volume = volume;
        }

        public void Load(ContentManager content){
            content.Load<SoundEffect>(asset);
        }

        public void play(){
            this.sound.Play(volume, 0f, 0f);
        }
    }
}
