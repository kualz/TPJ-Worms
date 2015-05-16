using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using War_Square.Menus;

namespace War_Square
{
    class MenusHandler
    {
        static public MenuPrincipal menuPrincipal = new MenuPrincipal();
        static public OptionsMenu optionsMenu = new OptionsMenu();
        static public MenuInGame menuInGame = new MenuInGame();
        static public MenuCharacterChoose characterChoose = new MenuCharacterChoose();
        static public MapChoose mapChooser = new MapChoose();


        static public void load(ContentManager content, Game1 game)
        {
            menuInGame.load(content);
            optionsMenu.Load(content);
            menuPrincipal.load(content);
            characterChoose.load(content);
            mapChooser.load(content);
        }

        static public void Update(GameTime gameTime, Game1 game)
        {
            if (game.gameState == Game1.GameState.Menu)
            {
                menuPrincipal.update(gameTime, game);
                return;
            }
            if (game.gameState == Game1.GameState.Paused)
            {
                menuInGame.update(gameTime, game);
                return;
            }
            if (game.gameState == Game1.GameState.Options)
            {
                optionsMenu.Update(gameTime, game);
                return;
            }
            if (game.gameState == Game1.GameState.CharacterChoose)
            {
                characterChoose.Update(gameTime, game);
                return;
            }
            if (game.gameState == Game1.GameState.MapChoose)
            {
                mapChooser.update(gameTime, game);
                return;
            }
        }

        static public void draw(SpriteBatch spriteBatch, Game1 game, characters.Characters Char)
        {
            if (game.gameState == Game1.GameState.Paused || game.gameState == Game1.GameState.gameOver)
            {
                menuInGame.draw(spriteBatch, Char);
                return;
            }
            if (game.gameState == Game1.GameState.Options)
            {
                optionsMenu.Draw(spriteBatch);
                return;
            }
            if (game.gameState == Game1.GameState.Menu)
            {
                menuPrincipal.Draw(spriteBatch);
                return;
            }
            if (game.gameState == Game1.GameState.CharacterChoose)
            {
                characterChoose.draw(spriteBatch);
                return;
            }
            if (game.gameState == Game1.GameState.MapChoose)
            {
                mapChooser.draw(spriteBatch);
                return;
            }
        }
    }
}
