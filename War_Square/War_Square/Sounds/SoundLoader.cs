using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.Sounds
{
    class SoundLoader : SoundManager
    {
        public SoundLoader() : base(){ }

        private void addNewSound(ContentManager content, string asset, string name, float volume)
        {
            base.addSound(new Sound(asset, name, 1f));
            base.getSound(name).Load(content);
        }

        private void addNewMusic(ContentManager content, string asset, string name)
        {
            base.addMusic(new Music(asset, name));
            base.getMusic(name).load(content);
        }


        public void load(ContentManager content)
        {
            //addNewMusic(content, "musicAssetTest", "musicNametest");
            //addNewSound(content, "SoundAssetTest", "soundNameTest", 1f);

            //addNewMusic(content, "musicAssetTest1", "musicNametest1");
            //addNewSound(content, "SoundAssetTest1", "soundNameTest1", 1f);

        }
    }
}
