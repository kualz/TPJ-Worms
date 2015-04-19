using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class CharactersHandler : Characters
    {
        static private List<Characters> Barracks = new List<Characters>();
        static public List<Characters> Players = new List<Characters>();
        static public int JogadorActivo = 0;

        static public void InitList(ContentManager content)
        {
            Barracks.Add(new Characters("Kualz"));
            Barracks.Add(new Characters("Phaktumn"));
            Barracks.Add(new Characters("Klipper"));
            Barracks.Add(new Characters("Zjeh"));
            Barracks.Add(new Characters("Saber"));
        }

        static public Characters getCharacter(int Character)
        {
            return Barracks[Character];
        }

        static public void AddPlayer(Characters cha)
        {
            Players.Add(cha);
        }

        static public Characters getActiveCharacter()
        {
            foreach (Characters cha in Players){
                if (cha.isActive()){ return cha; } 
            }
            return null;
        }

        static public Rectangle getActiveCharacterRectangle()
        {
            foreach (Characters cha in Players)
            {
                if (cha.isActive())
                {
                    return cha.getCharRec();
                }
            }
            return new Rectangle();
        }

        static public Weapons getActiveWeapon()
        {
            foreach (Characters cha in Players)
            {
                if (cha.isActive()) { return cha.GetActiveWeapon(); }
            }
            return null;
        }

        static public void ChangeActive()
        {
            Players[JogadorActivo].SetCharacterInPlay();
            JogadorActivo++;
            if (JogadorActivo >= Players.Count) JogadorActivo = 0;
            Players[JogadorActivo].SetCharacterInPlay();
        }

        static public void updatePlayers(GameTime gameTime)
        {
            foreach (Characters cha in Players){
                cha.Update(gameTime);
                    if (cha.isActive()){
                        cha.GetActiveWeapon().Update(gameTime,cha);
                }
            }
        }
    }
}
