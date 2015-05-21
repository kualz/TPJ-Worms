using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.Sounds
{
    class SoundManager
    {
        private Dictionary<string, Sound> SoundList;
        private Dictionary<string, Music> MusicList; 


        /// <summary>
        /// sao geradas as listas para inserir as musicas!
        /// </summary>
        public SoundManager()
        {
            SoundList = new Dictionary<string, Sound>();
            MusicList = new Dictionary<string, Music>();
        }

        protected void addMusic(Music music){
            MusicList.Add(music.musicName, music);
        }

        protected void addSound(Sound sound){
            SoundList.Add(sound.soundName, sound);
        }

        protected Music getMusic(string musicName){
            if (MusicList.ContainsKey(musicName))
                return MusicList[musicName];
            else return null;
        }

        protected Sound getSound(string soundName)
        {
            if (SoundList.ContainsKey(soundName))
                return SoundList[soundName];
            else return null;
        }

        public void playSound(string soundName)
        {
            if (SoundList.ContainsKey(soundName))
                SoundList[soundName].play();
        }

        public void playMusic(string musicName)
        {
            if (MusicList.ContainsKey(musicName))
                MusicList[musicName].play();
        }
    }
}
