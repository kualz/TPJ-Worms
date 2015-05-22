using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.Sounds
{
    class SoundManager
    {
        static private Dictionary<string, Sound> SoundList;
        static private Dictionary<string, Music> MusicList; 


        /// <summary>
        /// sao geradas as listas para inserir as musicas!
        /// </summary>
        static public void InitSoundLists()
        {
            SoundList = new Dictionary<string, Sound>();
            MusicList = new Dictionary<string, Music>();
        }

        static public void addMusic(Music music){
            MusicList.Add(music.musicName, music);
        }

        static public void addSound(Sound sound){
            SoundList.Add(sound.soundName, sound);
        }

        static public Music getMusic(string musicName){
            if (MusicList.ContainsKey(musicName))
                return MusicList[musicName];
            else return null;
        }

        static public Sound getSound(string soundName)
        {
            if (SoundList.ContainsKey(soundName))
                return SoundList[soundName];
            else return null;
        }

        static public void playSound(string soundName)
        {
            if (SoundList.ContainsKey(soundName))
                SoundList[soundName].play();
        }

        static public void playMusic(string musicName)
        {
            if (MusicList.ContainsKey(musicName))
                MusicList[musicName].play();
        }
    }
}
