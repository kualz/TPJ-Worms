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


        static public void load(ContentManager content, Game1 game)
        {
            menuInGame.load(content);
            optionsMenu.Load(content);
            menuPrincipal.load(content);
        }

        static public void Update(GameTime gameTime, Game1 game)
        {
            if (game.gameState == Game1.GameState.running)
                menuInGame.update(gameTime);
            if (game.gameState == Game1.GameState.Options)
                optionsMenu.Update(gameTime, game);
            if (game.gameState == Game1.GameState.Menu)
                menuPrincipal.update(gameTime, game);
        }

        static public void draw(SpriteBatch spriteBatch, Game1 game)
        {
            if (game.gameState == Game1.GameState.Menu)
                menuPrincipal.Draw(spriteBatch);
        }
    }
}
