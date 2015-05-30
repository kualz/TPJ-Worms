using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using War_Square.WeaponsAndProjectiles;
using Microsoft.Xna.Framework.Graphics;
using War_Square.Menus;

namespace War_Square.characters
{
    class CharactersHandler : Characters
    {
        static public List<Characters> Barracks = new List<Characters>();
        static public List<Characters> Players = new List<Characters>();
        static public int JogadorActivo = 1;
        static public int initialPlayer = 1;


        static public void InitList()
        {
            Barracks.Add(new Characters("Kualz"));
            Barracks.Add(new Characters("Phaktumn"));
            Barracks.Add(new Characters("Klipper"));
            Barracks.Add(new Characters("Zjeh"));
            Barracks.Add(new Characters("Saber"));
            Barracks.Add(new Characters("GhostCharacter"));
        }

        static public Characters getCharacter(int Character){
            return Barracks[Character];
        }

        static public void AddPlayer(Characters cha)
        {
            Players.Add(cha);
            Console.WriteLine("Player Inserido " + cha.returnName());
        }

        static public Characters getPlayerIN_GAME(int player)
        {
            if (Players[player].returnName() == "GhostCharacter")
                return Players[player + 1];
            else return Players[player];
        }

        static public Characters getActiveCharacter()
        {
            foreach (Characters cha in Players){
                if (cha.isActive()){ return cha; } 
            }
            return null;
        }

        static public Characters getNextPlayer()
        {
            int aux = 0;
            foreach (Characters ch in Players){
                if (ch.isActive() && ch.returnName() != "GhostCharacter"){
                    if (aux + 1 >= Players.Count) return getPlayerIN_GAME(0);
                    else return Players[aux + 1];
                }
                aux++;
                if (aux >= Players.Count) aux = 0;
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
            foreach (Characters cha in Players){
                if (cha.isActive()) { return cha.GetActiveWeapon(); }
            }
            return null;
        }


        /// <summary>
        /// this one makes the game!! really really good! you won bitch
        /// </summary>
        /// <returns></returns>
        static public bool isWinner(){
            int aux = 0;
            foreach (Characters Char in Players){
                if (Char.getHp() <= 0 && Char.returnName() != "GhostCharacter"){
                    aux++;
                }
            }
            if (aux == Game1.charactersInPlay - 1){
                return true;
            }
            else return false;
        }

        static public Characters getWinner()
        {
            foreach (Characters character  in Players){
                if (character.getHp() >= 0 && character.returnName() != "GhostCharacter"){
                    return character;
                }
            }
            return null;
        }

        static public void ChangeActive()
        {
            if (Game1.firstEntry == false) initialPlayer = 0;

            Players[JogadorActivo].SetCharacterInPlay();

            JogadorActivo++;
            //atencao apos todos os resets o jogador inicial nao 'e o 1 mas sim o 0;
            if (JogadorActivo >= Players.Count) JogadorActivo = initialPlayer;
            while (Players[JogadorActivo].getHp() <= 0)
            {
                JogadorActivo++;
                if (JogadorActivo >= Players.Count){
                    JogadorActivo = initialPlayer;
                    break;
                }
            }
            Players[JogadorActivo].SetCharacterInPlay();
        }

        static public void updatePlayers(GameTime gameTime)
        {
            foreach (Characters cha in Players){
                if (cha.returnName() != "GhostCharacter"){
                    cha.Update(gameTime);
                    if (cha.isActive() && cha.getHp() > 0){
                        cha.GetActiveWeapon().Update(gameTime, cha, cha.flip);
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
                if (cha.returnName() == "GhostCharacter"){
                    Players.Remove(cha);
                }
            }
        }

        /// <summary>
        /// basicamente da reset para poderes iniciar um novo jogo
        /// da reset ao necessario aqui!!!
        /// </summary>
        static public void resetPlayersList(bool isFirst)
        {
            Barracks.Clear();
            InitList();
            Game1.charactersInPlay = 0;
            if(isFirst == false)
                JogadorActivo = 0;
        }
    }
}
