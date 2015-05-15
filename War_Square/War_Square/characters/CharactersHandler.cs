using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using War_Square.WeaponsAndProjectiles;
using Microsoft.Xna.Framework.Graphics;

namespace War_Square.characters
{
    class CharactersHandler : Characters
    {
        static private List<Characters> Barracks = new List<Characters>();
        static public List<Characters> Players = new List<Characters>();
        static public int JogadorActivo = 1;

        static public void InitList(ContentManager content)
        {
            Barracks.Add(new Characters("Kualz"));
            Barracks.Add(new Characters("Phaktumn"));
            Barracks.Add(new Characters("Klipper"));
            Barracks.Add(new Characters("Zjeh"));
            Barracks.Add(new Characters("Saber"));
            Barracks.Add(new Characters("GhostCharacter"));
        }

        static public Characters getCharacter(int Character)
        {
            return Barracks[Character];
        }

        static public void AddPlayer(Characters cha)
        {
            Players.Add(cha);
            Console.WriteLine("Player Inserido " + cha.returnName());
        }

        static public Characters getPlayerIN_GAME(int player)
        {
            return Players[player];
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
            if (JogadorActivo >= Players.Count ) JogadorActivo = 1;
            Players[JogadorActivo].SetCharacterInPlay();
        }

        static public void updatePlayers(GameTime gameTime)
        {
            foreach (Characters cha in Players){
                if (cha.returnName() != "GhostCharacter"){
                    cha.Update(gameTime);
                    if (cha.isActive() && cha.getHp() > 0){
                        cha.GetActiveWeapon().Update(gameTime, cha);
                    }
                }
            }
        }

        static public void DrawPlayers(SpriteBatch spritebatch)
        {
            foreach (Characters cha in Players)
            {
                if (cha.returnName() != "GhostCharacter")
                    cha.Draw(spritebatch);
            }
        }

        static public void RemoveGhostCharacter()
        {
            foreach (Characters cha in Players)
            {
                if (cha.returnName() == "GhostCharacter")
                {
                    Players.Remove(cha);
                }
            }
        }


    }
}
