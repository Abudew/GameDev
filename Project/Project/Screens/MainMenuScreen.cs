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
    internal class MainMenuScreen : IGameObject
    {
        private readonly Texture2D _exitButton;
        private readonly Texture2D _exitButtonHover;
        private Texture2D _exitButtonHovered;

        private readonly Texture2D _loadButton;
        private readonly Texture2D _loadButtonHover;
        private Texture2D _loadButtonHovered;

        private readonly Texture2D _newButton;
        private readonly Texture2D _newButtonHover;
        private Texture2D _newButtonHovered;

        private readonly SpriteFont _font;

        private Vector2 exitVector;
        private Game1 _game;

        public MainMenuScreen(Texture2D button, Texture2D buttonHover, SpriteFont font, Game1 game)
        {
            _exitButton = button;
            _exitButtonHover = buttonHover; 
            _exitButtonHovered = button;

            _newButton = button;
            _newButtonHover = buttonHover;
            _newButtonHovered = button;

            _loadButton = button;
            _loadButtonHover = buttonHover;
            _loadButtonHovered = button;

            _font = font;

            _game = game; 
        }

        public ScreenType ScreenType => ScreenType.MainMenu;


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _newButtonHovered,
                new Vector2(exitVector.X, exitVector.Y-400),
                Color.White
            );
            spriteBatch.DrawString(_font, "New Game", new Vector2(exitVector.X + 115, exitVector.Y - 385), Color.Black);
            spriteBatch.Draw(
                _loadButtonHovered,
                new Vector2(exitVector.X, exitVector.Y-300),
                Color.White
            );
            spriteBatch.DrawString(_font, "Load Game", new Vector2(exitVector.X + 115, exitVector.Y - 285), Color.Black);
            spriteBatch.Draw(
                _exitButtonHovered,
                exitVector,
                Color.White
            );
            spriteBatch.DrawString(_font, "Exit Game", new Vector2(exitVector.X + 115, exitVector.Y + 15), Color.Black);
        }

        public void Update(float delta, GameTime gameTime)
        {
            exitVector = new Vector2((Game1.ScreenWidth / 2) - 150, Game1.ScreenHeight-150);
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            var exitButton = new Rectangle((int)exitVector.X, (int)exitVector.Y, this._exitButton.Width, this._exitButton.Height);
            var loadButton = new Rectangle((int)exitVector.X, (int)(exitVector.Y-300), this._exitButton.Width, this._exitButton.Height);
            var newButton = new Rectangle((int)exitVector.X, (int)(exitVector.Y-400), this._exitButton.Width, this._exitButton.Height);
            var isClicked = mouseState.LeftButton;

            if (exitButton.Contains(mousePoint))
            {
                _exitButtonHovered = _exitButtonHover;
                if (isClicked == ButtonState.Pressed)
                {
                    _game.Exit();
                }
            }
            else if (newButton.Contains(mousePoint))
            {
                _newButtonHovered = _newButtonHover;
                if (isClicked == ButtonState.Pressed)
                {
                    _game.isSwitch = true;
                    _game.screenSelection = ScreenType.Game;
                }
            }
            else if (loadButton.Contains(mousePoint))
            {
                _loadButtonHovered = _loadButtonHover;
                if (isClicked == ButtonState.Pressed)
                {
                    _game.isSwitch = true;
                    _game.screenSelection = ScreenType.Load;
                }
            }
            else
            {
                _exitButtonHovered = _exitButton;
                _loadButtonHovered = _loadButton;
                _newButtonHovered = _newButton;
                _game.isSwitch = false;
            }
        }
    }
}
