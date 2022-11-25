using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Controllers;
using Project.Interfaces;
using Project.Levels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    internal class GameScreen : IGameObject
    {
        private Character _character;
        private Game1 _game;
        private Level level;
        private MovementController movementController;
        public ScreenType ScreenType => ScreenType.Game;


        public GameScreen(Texture2D[] characters, Game1 game)
        {
            movementController = new MovementController();
            _game = game;
            level = new Level(game, movementController);
            _character = new Character(characters, new KeyboardController(), movementController, level.blocks, level, _game);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_game.backGround[1], new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height), Color.White);
            level.Draw(spriteBatch);
            _character.Draw(spriteBatch);
        }

        public void Update(float delta, GameTime gameTime) 
        {
            level.Update(delta, gameTime);
            _character.Update(delta, gameTime);
        }
    }
}
