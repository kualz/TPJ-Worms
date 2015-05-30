using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.Sounds
{
    class SoundLoader 
    {
        public SoundLoader(){ }

        public void addNewSound(ContentManager content, string asset, string name, float volume)
        {
            SoundManager.addSound(new Sound(asset, name, 1f));
            SoundManager.getSound(name).Load(content);
        }

        public void addNewMusic(ContentManager content, string asset, string name)
        {
            SoundManager.addMusic(new Music(asset, name));
            SoundManager.getMusic(name).load(content);
        }


        public void load(ContentManager content)
        {
            //addNewMusic(content, "musicAssetTest", "musicNametest");
            //addNewSound(content, "SoundAssetTest", "soundNameTest", 1f);

            addNewMusic(content, "Equalizer", "ingameMusic");
            addNewMusic(content, "Spine", "menuMusic");
            addNewSound(content, "Fart1", "peidoteste", 1f);
            addNewSound(content, "StartSelectzippy", "enterselect", 1f);
            addNewSound(content, "menuswitch", "menuswitch", 1f);
            //addNewMusic(content, "musicAssetTest1", "musicNametest1");
            //addNewSound(content, "SoundAssetTest1", "soundNameTest1", 1f);

        }
    }
}
