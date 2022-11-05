using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Project.Screens
{
    internal class MainMenuScreen : IScreen
    {
        private readonly Texture2D _exit;
        private readonly Texture2D _exitHover;
        private Texture2D _exitHovered;
        private readonly Texture2D _newGame;
        private Texture2D _newGameHover;
        private Texture2D _newGameHovered;
        private readonly Texture2D _loadGame;
        private Texture2D _loadGameHover;
        private Texture2D _loadGameHovered;
        private Vector2 exitVector;
        private Game1 _game;

        public MainMenuScreen(Texture2D exit, Texture2D exitHover, Texture2D newGame, Texture2D newGameHover, Texture2D loadGame, Texture2D loadGameHover, Game1 game)
        {
            _exit = exit;
            _exitHover = exitHover;
            _newGame = newGame;
            _newGameHover = newGameHover;
            _loadGame = loadGame;
            _loadGameHover = loadGameHover;
            _loadGameHovered = _loadGame;
            _newGameHovered = _newGame;
            _exitHovered = _exit;
            _game = game; 
        }

        public ScreenType ScreenType => ScreenType.MainMenu;


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(
                _newGameHovered,
                new Vector2(exitVector.X, exitVector.Y-432),
                Color.White
            );
            spriteBatch.Draw(
                _loadGameHovered,
                new Vector2(exitVector.X, exitVector.Y-216),
                Color.White
            );
            spriteBatch.Draw(
                _exitHovered,
                exitVector,
                Color.White
            );
            spriteBatch.End();
        }

        public void Update(float delta)
        {
            exitVector = new Vector2((Game1.ScreenWidth / 2) - 240, Game1.ScreenHeight - 228);
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            var exitButton = new Rectangle((int)exitVector.X, (int)exitVector.Y, this._exit.Width, this._exit.Height);
            var loadButton = new Rectangle((int)exitVector.X, (int)(exitVector.Y-216), this._loadGame.Width, this._loadGame.Height);
            var newButton = new Rectangle((int)exitVector.X, (int)(exitVector.Y-432), this._newGame.Width, this._newGame.Height);
            var isClicked = mouseState.LeftButton;

            if (exitButton.Contains(mousePoint))
            {
                _exitHovered = _exitHover;
                if (isClicked == ButtonState.Pressed)
                {
                    _game.Exit();
                }
            }
            else if (newButton.Contains(mousePoint))
            {
                _newGameHovered = _newGameHover;
                if (isClicked == ButtonState.Pressed)
                {
                    _game.isSwitch = true;
                    _game.screenFromMain = ScreenType.New;
                }
            }
            else if (loadButton.Contains(mousePoint))
            {
                _loadGameHovered = _loadGameHover;
                if (isClicked == ButtonState.Pressed)
                {
                    _game.isSwitch = true;
                    _game.screenFromMain = ScreenType.Load;
                }
            }
            else
            {
                _exitHovered = _exit;
                _loadGameHovered = _loadGame;
                _newGameHovered = _newGame;
                _game.isSwitch = false;
            }
        }
    }
}
